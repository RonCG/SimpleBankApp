using LinqToDB;
using LinqToDB.Data;
using Microsoft.AspNetCore.Http;
using SimpleBankApp.Infrastructure.Persistance.Linq2DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBankApp.Infrastructure.Persistance.Repositories
{
    public class BaseRepository
    {
        protected HttpContext _context;
        protected DataConnection internalDb;

        public SimpleBankDb Db
        {
            get
            {
                return (SimpleBankDb)internalDb;
            }
        }

        public BaseRepository()
        {
            internalDb = new SimpleBankDb();
        }

        protected T SetCreateVars<T>(T model)
        {
            model = SetUpdateVars(model);
            ((dynamic)model).CreatedOn = DateTime.Now.ToUniversalTime();
            return model;
        }

        
        protected T SetUpdateVars<T>(T model)
        {
            ((dynamic)model).LastUpdatedOn = DateTime.Now.ToUniversalTime();
            return model;
        }

        public void Dispose()
        {
            internalDb.Dispose();
        }
    }
}
