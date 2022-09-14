using AndroidX.CardView.Widget;

namespace eStore_Maui.Views.Custom;

public partial class RecordListingView : ContentView
{
	public RecordListingView()
	{
		InitializeComponent();
	}

    #region PropertyDefine

    //Title
    public static readonly BindableProperty TitleProperty = BindableProperty.Create(nameof(Title), typeof(string), typeof(RecordListingView), string.Empty);

    public string Title
    {
        get => (string)GetValue(RecordListingView.TitleProperty);
        set => SetValue(RecordListingView.TitleProperty, value);
    }

    //Add Button
    public static readonly BindableProperty AddButtonTextProperty = BindableProperty.Create(nameof(AddButtonText), typeof(string), typeof(RecordListingView), "Add");

    public string AddButtonText
    {
        get => (string)GetValue(RecordListingView.AddButtonTextProperty);
        set => SetValue(RecordListingView.AddButtonTextProperty, value);
    }

    //Delete Button
    public static readonly BindableProperty DeleteButtonTextProperty = BindableProperty.Create(nameof(DeleteButtonText), typeof(string), typeof(RecordListingView), "Delete");
    
    public string DeleteButtonText
    {
        get => (string)GetValue(RecordListingView.DeleteButtonTextProperty);
        set => SetValue(RecordListingView.DeleteButtonTextProperty, value);
    }
    //Delete Button
    public static readonly BindableProperty RefreshButtonTextProperty = BindableProperty.Create(nameof(RefreshButtonText), typeof(string), typeof(RecordListingView), "Refresh");

    public string RefreshButtonText
    {
        get => (string)GetValue(RecordListingView.RefreshButtonTextProperty);
        set => SetValue(RecordListingView.RefreshButtonTextProperty, value);
    }

    #endregion
}
