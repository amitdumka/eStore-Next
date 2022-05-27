using eStoreMobileX.Core.Database;
namespace eStoreMobileX.Data.DataModels.Base
{
    public abstract class LocalDataModel<T> : BaseDataModel<T> where T : class
    {
        public List<T> Entity { get; set; }
        protected AppDBContext _context;

        protected LocalDataModel(ConType conType) : base(conType)
        {
        }

        public override async Task<bool> Delete(int id)
        {
            using (_context = new AppDBContext())
            {
                var element = await _context.FindAsync<T>(id);
                _context.Remove<T>(element);
                return (await _context.SaveChangesAsync()) > 0;
            }
        }
        public override async Task<T> GetById(int id)
        {
            using (_context = new AppDBContext())
            {
                return await _context.FindAsync<T>(id);
            }
        }
        public override async Task<bool> Delete(string id)
        {
            using (_context = new AppDBContext())
            {
                var element = await _context.FindAsync<T>(id);
                _context.Remove<T>(element);
                return (await _context.SaveChangesAsync()) > 0;
            }
        }
        public override async Task<T> GetById(string id)
        {
            using (_context = new AppDBContext())
            {
                return await _context.FindAsync<T>(id);
            }
        }
        public override bool IsExists(int id)
        {
            if (_context.Find<T>(id) != null) return true; else return false;
        }
        public override bool IsExists(string id)
        {
            if (_context.Find<T>(id) != null) return true; else return false;
        }

        public override async Task<bool> Save(T item, bool isNew = true)
        {
            using (_context = new AppDBContext())
            {
                await _context.AddAsync<T>(item);
                return (await _context.SaveChangesAsync()) > 0;
            }
        }
        //public abstract Task<List<T>> FindAsync(QueryParam query);
        //public abstract Task<List<T>> GetItems(int storeid);
        //{using (_context = new AppDBContext()) return _context.Query<T>().ToList();}

    }

}
