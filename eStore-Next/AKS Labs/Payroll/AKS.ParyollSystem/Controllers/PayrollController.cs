namespace AKS.ParyollSystem.Controllers
{
    public interface IController<T> : IDisposable
    {
        public Task<bool> CreateAsync(T record);

        public Task<bool> Update(T record);

        public Task<bool> Delete(T record);

        public Task<bool> DeleteAll(List<T> records);

        public Task<bool> DeleteAll(List<int> records);

        public Task<bool> DeleteAll(List<string> records);

        public Task<bool> Delete(string id);

        public Task<bool> Delete(int id);

        public List<T> Records();

        public T Record(int id);

        public T Record(string id);

        public List<T> Record(List<string> columnNames, List<string> value);
    }
}