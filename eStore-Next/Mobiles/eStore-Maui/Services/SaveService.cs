namespace eStore_Maui.Services
{
#if ANDROID
    public partial class SaveService
    {
        public partial void SaveAndView(string filename, string contentType, MemoryStream stream);
        
    }
#endif
}