//using DevExpress.Maui.Editors;
using System.Globalization;


namespace eStore_MAUI_Next
{
    public enum BoxMode { Filled, Outlined} //TODO: remove rfom devexpress implemented
    public class BoxModeToImageSourceConverter : IValueConverter, IMarkupExtension<BoxModeToImageSourceConverter>
    {
        public ImageSource Filled { get; set; }
        public ImageSource Outlined { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is BoxMode boxMode) || targetType != typeof(ImageSource)) return null;
            switch (boxMode)
            {
                case BoxMode.Filled:
                    return Filled;
                case BoxMode.Outlined:
                    return Outlined;
                default:
                    throw new NotSupportedException();
            }

        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public BoxModeToImageSourceConverter ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
