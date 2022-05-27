using eStoreMobileX.Core.Database;
namespace eStoreMobileX.Data.DataModels.Base
{
    public abstract class LocalDataModel<T> : BaseDataModel<T> where T : class
    {
        public List<T> Entity { get; set; }
        protected AppDBContext _localDb;

        protected LocalDataModel(ConType conType) : base(conType)
        {
        }

        public override async Task<bool> Delete(int id)
        {
            using (_localDb = new AppDBContext())
            {
                var element = await _localDb.FindAsync<T>(id);
                _localDb.Remove<T>(element);
                return (await _localDb.SaveChangesAsync()) > 0;
            }
        }
        public override async Task<T> GetById(int id)
        {
            using (_localDb = new AppDBContext())
            {
                return await _localDb.FindAsync<T>(id);
            }
        }
        public override async Task<bool> Delete(string id)
        {
            using (_localDb = new AppDBContext())
            {
                var element = await _localDb.FindAsync<T>(id);
                _localDb.Remove<T>(element);
                return (await _localDb.SaveChangesAsync()) > 0;
            }
        }
        public override async Task<T> GetById(string id)
        {
            using (_localDb = new AppDBContext())
            {
                return await _localDb.FindAsync<T>(id);
            }
        }
        public override bool IsExists(int id)
        {
            if (_localDb.Find<T>(id) != null) return true; else return false;
        }
        public override bool IsExists(string id)
        {
            if (_localDb.Find<T>(id) != null) return true; else return false;
        }

        public override async Task<bool> Save(T item, bool isNew = true)
        {
            using (_localDb = new AppDBContext())
            {
                await _localDb.AddAsync<T>(item);
                return (await _localDb.SaveChangesAsync()) > 0;
            }
        }
         

    }

}
