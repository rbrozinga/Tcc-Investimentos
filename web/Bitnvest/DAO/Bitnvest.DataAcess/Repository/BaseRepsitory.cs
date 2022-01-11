using Bitnvest.DataAcess.Context;
using System;

namespace Bitnvest.DataAcess.Repository
{
    public abstract class BaseRepsitory : IDisposable
    {
        protected DbSqlContext _db;

        public BaseRepsitory(DbSqlContext db)
        {
            _db = db;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
