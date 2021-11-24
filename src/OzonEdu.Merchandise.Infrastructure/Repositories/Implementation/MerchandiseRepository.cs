using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchPackAggregate;
using OzonEdu.Merchandise.Domain.Contracts;
using OzonEdu.Merchandise.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.Merchandise.Infrastructure.Repositories.Implementation
{
    public class MerchandiseRepository:IMerchOrderRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;
        public IUnitOfWork UnitOfWork { get; }
        public MerchandiseRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }

        public async Task<MerchOrder> CreateAsync(MerchOrder merchOrder, CancellationToken cancellationToken = default)
        {
            string sql = @$"
                INSERT INTO merch_order ( employee_id, merch_pack_id, status_id, order_date)
                VALUES ( @EmployeeId, @MerchPackId, @StatusId, @OrderDate) RETURNING id;";

            var parameters = new
            {

                EmployeeId = merchOrder.EmployeeId.Value,
                MerchPackId = merchOrder.MerchPackId.Value,
                StatusId = merchOrder.CurrentOrderState.Id,
                OrderDate = merchOrder.OrderDate.Value
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var id = await connection.QuerySingleOrDefaultAsync<long>(commandDefinition);
            _changeTracker.Track(merchOrder);
            return MerchOrder.Create(id, merchOrder.EmployeeId, merchOrder.MerchPackId, merchOrder.CurrentOrderState, merchOrder.OrderDate);
        }
        public async Task<MerchOrder> UpdateAsync(MerchOrder merchOrder, CancellationToken cancellationToken = default)
        {
             string sql = @$"
                UPDATE merch_order
                SET employee_id = @EmployeeId, merch_pack_id = @MerchPackId, status_id = @StatusId, order_date =@OrderDate
                WHERE id = @MerchOrderId;";

            var parameters = new
            {
                EmployeeId = merchOrder.EmployeeId.Value,
                MerchPackId = merchOrder.MerchPackId.Value,
                StatusId = merchOrder.CurrentOrderState.Id,
                OrderDate = merchOrder.OrderDate.Value
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            _changeTracker.Track(merchOrder);
            return merchOrder;
        }

        public async Task<OrderState> CheckOrderState(long orderId, CancellationToken cancellationToken = default)
        {
             string sql=@$"
                        select m.status_id, o.name 
                        from merch_order m inner join order_state o on o.id = m.status_id 
                        where m.id = {orderId}";
             var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
             var result = await connection.QueryAsync<Models.OrderState>(sql);

             OrderState.TryGetOrderStateById( result.First().Id, out var val);
             return val;
        }
        
        public async Task<MerchOrder> FindById(long orderId, CancellationToken cancellationToken = default)
        {
            string sql=@$"
                        select id, employee_id, merch_id, status_id, order_date
                        from merch_order    
                        where id = {orderId}";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchOrderDto>(sql);
            var firstOfRes = result.First();
            return MerchOrder.Create(firstOfRes.OrderId, 
                new EmployeeId(firstOfRes.EmployeeId),
                new PackId(firstOfRes.MerchPackId),
                OrderState.GetOrderStateById(firstOfRes.StatusId),
                new OrderDate(firstOfRes.OrderDate));
        }

        public async Task<bool> CheckEmployeeHaveMerch(long employeeId, long merchPackId, CancellationToken cancellationToken = default)
        {
            string sql=@$"
                        select id, employee_id, merch_id, status_id
                        from merch_order   
                        where employee_id = {employeeId} and merch_id = {merchPackId} and status_id = 4";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchOrderDto>(sql);

            return result.Any();
        }
        
        public async Task<bool> CheckEmployeeHaveMerchOrders(long employeeId, long merchPackId, CancellationToken cancellationToken = default)
        {
            string sql=@$"
                        select id, employee_id, merch_id, status_id
                        from merch_order   
                        where employee_id = {employeeId} and merch_id = {merchPackId} and status_id = any(1,2,3,5,6)";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchOrderDto>(sql);

            return result.Any();
        }

        public async Task<IReadOnlyList<MerchOrder>> GetAllEmployeeCompleteOrders(long employeeId, CancellationToken cancellationToken = default)
        {
            string sql=@$"
                        select id, employee_id, merch_id, status_id
                        from merch_order   
                        where employee_id = {employeeId} and status_id = 4";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchOrderDto>(sql);
            var merchOrderList = new List<MerchOrder>();
            result.ToList().ForEach(x=>merchOrderList.Add(
                MerchOrder.Create(x.OrderId,
                    new EmployeeId(x.EmployeeId), 
                    new PackId(x.MerchPackId), 
                    OrderState.GetOrderStateById(x.StatusId),
                    new OrderDate(x.OrderDate))
                ));
        
            return merchOrderList;
        }

        public async Task<IReadOnlyList<MerchOrder>> GetAllEmployeeInProcessOrders(long employeeId, CancellationToken cancellationToken = default)
        { 
            string sql=@$"
                        select id, employee_id, merch_id, status_id, order_date
                        from merch_order   
                        where employee_id = @employeeId and status_id = any(1,2,3,5,6)";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchOrderDto>(sql);
            var merchOrderList = new List<MerchOrder>();
            result.ToList().ForEach(x=>merchOrderList.Add(
                MerchOrder.Create(x.OrderId, 
                    new EmployeeId(x.EmployeeId), 
                    new PackId(x.MerchPackId), 
                    OrderState.GetOrderStateById(x.StatusId),
                    new OrderDate(x.OrderDate))
            ));
          
            merchOrderList.ForEach(x=>_changeTracker.Track(x));
            return merchOrderList;
        }
        
        public async Task<IReadOnlyList<MerchOrder>> GetAllEmployeeOrdersInSpecialStatus(long employeeId, List<int> statusList, CancellationToken cancellationToken = default)
        {
             string sql=@$"
                        select id, employee_id, merch_id, status_id, order_date
                        from merch_order   
                        where employee_id = @employeeId and status_id = any(@StatusList);";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchOrderDto>(sql, new
            {
                StatusList = statusList
            });
            var merchOrderList = new List<MerchOrder>();
            result.ToList().ForEach(x=>merchOrderList.Add(
                MerchOrder.Create(x.OrderId,
                    new EmployeeId(x.EmployeeId),
                    new PackId(x.MerchPackId), 
                    OrderState.GetOrderStateById(x.StatusId),
                    new OrderDate(x.OrderDate))
            ));

            merchOrderList.ForEach(x=>_changeTracker.Track(x));

            return merchOrderList;
        }
    }
}