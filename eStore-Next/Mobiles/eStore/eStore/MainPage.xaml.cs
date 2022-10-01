using static eStore.Views.ListWidget;

namespace eStore;

public partial class MainPage : ContentPage
{
    private int count = 0;

    public List<ItemList> SourceList = new(){
        new ItemList {Title="Yearly", Description="2000" },
        new ItemList { Title = "Monthly", Description = "2000" },
        new ItemList
        {
            Title = "Qutarly",
            Description = "2000"
        },
    };

    public MainPage()
    {
        InitializeComponent();
        LWA.ItemData = SourceList;
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}