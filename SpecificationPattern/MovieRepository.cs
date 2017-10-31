using System.Collections.Generic;
using System.Linq;

using ServiceStack.OrmLite.SqlServer;
using ServiceStack.OrmLite;
using System.Data;
using ServiceStack.OrmLite.Legacy;

namespace SpecificationPattern
{

    public class MovieRepository : Repository<Movie>
    {
    }


    public abstract class Repository<T>
    {
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Movies;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        private OrmLiteConnectionFactory dbFactory;
            
        public Repository() {
            this.dbFactory = new OrmLiteConnectionFactory(this.connectionString,SqlServerDialect.Provider);
        }


        public IReadOnlyList<T> Find(Specification<T> specification, int page = 0, int pageSize = 100)
        {
            using (IDbConnection db = dbFactory.Open())
            {
                return db.Select<T>(specification.ToExpression()).Skip<T>(page * pageSize).Take<T>(pageSize).ToList();
            }
        }
    }
}
