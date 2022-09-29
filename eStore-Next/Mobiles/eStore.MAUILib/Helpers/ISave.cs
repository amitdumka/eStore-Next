namespace eStore.MAUILib.Helpers
{
    public interface ISave
    {
        public Task SaveAndView(string filename, string contentType, MemoryStream stream);
    }
}