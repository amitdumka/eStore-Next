using CommunityToolkit.Maui.Alerts;
using eStore.MAUILib.Services;

namespace eStore.MAUILib.Helpers
{
    public static class Notify
    {
        public static void NotifyLong(string msg)
        {
            Toast.Make(msg, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
        }

        public static void NotifyShort(string msg)
        {
            Toast.Make(msg, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
        }

        public static void NotifyVLong(string msg)
        {
            if (!string.IsNullOrEmpty(msg))
            {
                Toast.Make(msg, CommunityToolkit.Maui.Core.ToastDuration.Long).Show();
                ASpeak.Speak(msg);
            }
        }

        public static void NotifyVShort(string msg)
        {
            Toast.Make(msg, CommunityToolkit.Maui.Core.ToastDuration.Short).Show();
            ASpeak.Speak(msg);
        }
    }
}