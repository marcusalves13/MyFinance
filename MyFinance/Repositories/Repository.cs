using Dapper.Contrib.Extensions;
using System.Data.SqlClient;

namespace MyFinance.Repositories
{
    public class Repository<Tmodel> where Tmodel : class
    {
        private readonly SqlConnection _connection;
        public Repository(SqlConnection connection)
        {
            _connection = connection;
        }
        public long Create(Tmodel model) 
        {
            return _connection.Insert<Tmodel>(model);
        }
        public IEnumerable<Tmodel> Get() 
        {
            return _connection.GetAll<Tmodel>();
        }
        public bool Update(Tmodel model)
        {
            return _connection.Update<Tmodel>(model);
        }
        public bool Delete(long id) 
        {
            var model = _connection.Get<Tmodel>(id);
            return _connection.Delete<Tmodel>(model);
        }
    }
}
