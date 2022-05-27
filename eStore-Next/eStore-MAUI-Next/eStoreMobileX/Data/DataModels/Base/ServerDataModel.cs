using eStoreMobileX.Core.Database;
namespace eStoreMobileX.Data.DataModels.Base
{
    public abstract class ServerDataModel<T> where T : class
    {
        public List<T> Entity { get; set; }
        protected AppDBContext _context;

        public void Get() { }
        public void GetById(int id) { }
        public void GetById(string id) { }

        public void Find(QueryParam queryParams) { }

        public void Save(T item, bool isNew = true) { }
        public void Save(List<T> items, bool isNew = true) { }

        public void Delete(int id) { }
        public void DeleteSelected(List<int> ids) { }
        public void Delete(string id) { }
        public void DeleteSelected(List<string> ids) { }

    }

}
