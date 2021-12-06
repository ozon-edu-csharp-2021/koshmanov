using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Dapper;
using Npgsql;
using OpenTracing;
using OzonEdu.Merchandise.Domain.AggregationModels.EmployeeAggregate;
using OzonEdu.Merchandise.Domain.Contracts;
using OzonEdu.Merchandise.Infrastructure.Repositories.Infrastructure.Interfaces;

namespace OzonEdu.Merchandise.Infrastructure.Repositories.Implementation
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDbConnectionFactory<NpgsqlConnection> _dbConnectionFactory;
        private readonly IChangeTracker _changeTracker;
        private const int Timeout = 5;
        private readonly ITracer _tracer;
        public IUnitOfWork UnitOfWork { get; }

        public EmployeeRepository(IDbConnectionFactory<NpgsqlConnection> dbConnectionFactory,
            IChangeTracker changeTracker, ITracer tracer)
        {
            _dbConnectionFactory = dbConnectionFactory;
            _changeTracker = changeTracker;
            _tracer = tracer;
        }

        public async Task<Employee> CreateAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            using var span = _tracer.BuildSpan("EmployeeRepository.CreateAsync")
                .StartActive();
            string sql = @$"
                INSERT INTO employee ( email)
                VALUES (  @Email) RETURNING id;";

            var parameters = new
            {
                Email = employee.Email.Value
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var id = await connection.QuerySingleOrDefaultAsync<long>(commandDefinition);
            _changeTracker.Track(employee);
            return Employee.Create(id, employee.Email);
        }

        public async Task<Employee> UpdateAsync(Employee employee, CancellationToken cancellationToken = default)
        {
            using var span = _tracer.BuildSpan("EmployeeRepository.UpdateAsync")
                .StartActive();
            string sql = @$"
                UPDATE employee
                SET email = @Email
                WHERE id = @EmployeeId;";

            var parameters = new
            {
                Email = employee.Email.Value,
                EmployeeId = employee.Id
            };
            var commandDefinition = new CommandDefinition(
                sql,
                parameters: parameters,
                commandTimeout: Timeout,
                cancellationToken: cancellationToken);
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            await connection.ExecuteAsync(commandDefinition);
            _changeTracker.Track(employee);
            return employee;
        }

        public async Task<Employee> FindByIdAsync(long id, CancellationToken cancellationToken = default)
        {
            using var span = _tracer.BuildSpan("EmployeeRepository.FindByIdAsync")
                .StartActive();
            string sql=@$"
                        select id, email
                        from employee    
                        where id = @Id";
            var connection = await _dbConnectionFactory.CreateConnection(cancellationToken);
            var result = await connection.QueryAsync<Models.EmployeeDto>(sql, new
            {
                Id = id
            });
            var firstOfRes = result.First();
            return Employee.Create(firstOfRes.Id,  Email.Create(firstOfRes.Email) );
        }
    }
}