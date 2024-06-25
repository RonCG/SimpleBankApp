using LinqToDB;
using LinqToDB.Data;
using SimpleBankApp.Infrastructure.Persistance.Linq2DB;
namespace SimpleBankApp.Infrastructure.Persistance.Repositories.Common
{
    public class CommonRepository : BaseRepository, ICommonRepository
    {
        public SimpleBankDb GetSimpleBankDb()
        {
            return base.Db;
        }
        
        public async Task<int> InsertAsync<T>(T model, bool skipCreateVars = false)
        {
            SetCreateVars(model);
            return await Db.InsertAsync(model);
        }

        public async Task<int> UpdateAsync<T>(T model, bool skipCreateVars = false)
        {
            SetUpdateVars(model);
            return await Db.UpdateAsync(model);
        }

        public async Task<int> DeleteAsync<T>(T model)
        {
            return await Db.DeleteAsync(model);
        }


    }

    public interface ICommonRepository
    {
        public SimpleBankDb GetSimpleBankDb();
        public Task<int> InsertAsync<T>(T model, bool skipCreateVars = false);
        public Task<int> UpdateAsync<T>(T model, bool skipUpdateVars = false);
        public Task<int> DeleteAsync<T>(T model);
    }
}
