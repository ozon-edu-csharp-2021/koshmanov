using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OzonEdu.Merchandise.Domain.AggregationModels.MerchOrderAggregate;
using OzonEdu.Merchandise.Domain.Contracts;
using OzonEdu.Merchandise.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.Merchandise.Infrastructure.Repositories.Implementation
{
    public class MerchandiseRepository:IMerchOrderRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;

        public MerchandiseRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory, IChangeTracker changeTracker)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
        }
        public IUnitOfWork UnitOfWork { get; }
        public async Task<MerchOrder> CreateAsync(MerchOrder merchOrder, CancellationToken cancellationToken = default)
        {
            string sql = @$"
                INSERT INTO merch_order (id, employee_id, merch_pack_id, status_id)
                VALUES (@MerchOrderId, @EmployeeId, @MerchPackId, @StatusId);";

            var parameters = new
            {
                MerchOrderId = merchOrder.Id,
                EmployeeId = merchOrder.EmployeeId.Value,
                MerchPackId = merchOrder.MerchPack.Id,
                StatusId = merchOrder.CurrentOrderState.Id,
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
        public async Task<MerchOrder> UpdateAsync(MerchOrder merchOrder, CancellationToken cancellationToken = default)
        {
             string sql = @$"
                UPDATE merch_order
                SET employee_id = @EmployeeId, merch_pack_id = @MerchPackId, status_id = @StatusId
                WHERE id = @MerchOrderId;";

            var parameters = new
            {
                EmployeeId = merchOrder.EmployeeId.Value,
                MerchPackId = merchOrder.MerchPack.Id,
                StatusId = merchOrder.CurrentOrderState.Id,
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

        public async Task<OrderState> CheckOrderState(int orderId, CancellationToken cancellationToken = default)
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
        
        public async Task<MerchOrder> FindById(int orderId, CancellationToken cancellationToken = default)
        {
            string sql=@$"
                        select id, employee_id, merch_id, status_id
                        from merch_order    
                        where id = {orderId}";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchOrderDto>(sql);
            
            return MerchOrder.Create(result.First().OrderId, new EmployeeId(result.First().EmployeeId),
                MerchPack.GetPackById(result.First().MerchPackId),
                OrderState.GetOrderStateById(result.First().StatusId));
        }

        public async Task<bool> CheckEmployeeHaveMerch(int employeeId, int merchPackId, CancellationToken cancellationToken = default)
        {
            string sql=@$"
                        select id, employee_id, merch_id, status_id
                        from merch_order   
                        where employee_id = {employeeId} and merch_id = {merchPackId} and status_id = 4";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchOrderDto>(sql);

            return result.Any();
        }
        
        public async Task<bool> CheckEmployeeHaveMerchOrders(int employeeId, int merchPackId, CancellationToken cancellationToken = default)
        {
            string sql=@$"
                        select id, employee_id, merch_id, status_id
                        from merch_order   
                        where employee_id = {employeeId} and merch_id = {merchPackId} and status_id = any(1,2,3,5,6)";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchOrderDto>(sql);

            return result.Any();
        }

        public async Task<IReadOnlyList<MerchOrder>> GetAllEmployeeCompleteOrders(int employeeId, CancellationToken cancellationToken = default)
        {
            string sql=@$"
                        select id, employee_id, merch_id, status_id
                        from merch_order   
                        where employee_id = {employeeId} and status_id = 4";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchOrderDto>(sql);
            var merchOrderList = new List<MerchOrder>();
            result.ToList().ForEach(x=>merchOrderList.Add(
                MerchOrder.Create(x.OrderId, new EmployeeId(x.EmployeeId), MerchPack.GetPackById(x.MerchPackId), OrderState.GetOrderStateById(x.StatusId))
                ));
        
            return merchOrderList;
        }

        public async Task<IReadOnlyList<MerchOrder>> GetAllEmployeeInProcessOrders(int employeeId, CancellationToken cancellationToken = default)
        { 
            string sql=@$"
                        select id, employee_id, merch_id, status_id
                        from merch_order   
                        where employee_id = @employeeId and status_id = any(1,2,3,5,6)";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchOrderDto>(sql);
            var merchOrderList = new List<MerchOrder>();
            result.ToList().ForEach(x=>merchOrderList.Add(
                MerchOrder.Create(x.OrderId, new EmployeeId(x.EmployeeId), MerchPack.GetPackById(x.MerchPackId), OrderState.GetOrderStateById(x.StatusId))
            ));
          
            merchOrderList.ForEach(x=>_changeTracker.Track(x));
            return merchOrderList;
        }
        
        public async Task<IReadOnlyList<MerchOrder>> GetAllEmployeeOrdersInSpecialStatus(int employeeId, List<int> statusList, CancellationToken cancellationToken = default)
        {
             string sql=@$"
                        select id, employee_id, merch_id, status_id
                        from merch_order   
                        where employee_id = @employeeId and status_id = any(@statusList);";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.MerchOrderDto>(sql, new
            {
                statusList = statusList
            });
            var merchOrderList = new List<MerchOrder>();
            result.ToList().ForEach(x=>merchOrderList.Add(
                MerchOrder.Create(x.OrderId, new EmployeeId(x.EmployeeId), MerchPack.GetPackById(x.MerchPackId), OrderState.GetOrderStateById(x.StatusId))
            ));

            merchOrderList.ForEach(x=>_changeTracker.Track(x));

            return merchOrderList;
        }
    }
}