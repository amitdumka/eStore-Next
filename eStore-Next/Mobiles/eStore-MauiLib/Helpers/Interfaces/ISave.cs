namespace eStore_MauiLib.Helpers.Interfaces
{
    public interface ISave
    {
        public Task SaveAndView(string filename, string contentType, MemoryStream stream);
    }
}