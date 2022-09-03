using System.Collections.ObjectModel;
using Syncfusion.Maui.DataGrid;

namespace eStore_Maui.Pages;

public partial class VoucherPage : ContentPage
{

    public ObservableCollection<Test>  TestList;
    TestVM viewModel2 = new TestVM();
    
	public VoucherPage()
	{
		InitializeComponent();
		init();
       // dataGrid.ItemsSource = viewModel2.TestList;

    }

	void init()
	{
		TestList = new ObservableCollection<Test>();
		TestList.Add(new Test { id=1, name="Amit", city="Dumka" });
        TestList.Add(new Test { id = 1, name = "Amit", city = "Deoghar" });
        TestList.Add(new Test { id = 2, name = "Amit Kumar", city = "Bang" });
        TestList.Add(new Test { id = 3, name = "Ajit", city = "Dumka" });
        TestList.Add(new Test { id = 4, name = "Alok", city = "Delhi" });
        TestList.Add(new Test { id = 5, name = "Amar", city = "London" });
    }
}

public class TestVM
{
    public ObservableCollection<Test> TestList { get; set; }
    public TestVM()
    {
        TestList = new ObservableCollection<Test>();
        TestList.Add(new Test { id = 1, name = "Amit", city = "Dumka" });
        TestList.Add(new Test { id = 1, name = "Amit", city = "Deoghar" });
        TestList.Add(new Test { id = 2, name = "Amit Kumar", city = "Bang" });
        TestList.Add(new Test { id = 3, name = "Ajit", city = "Dumka" });
        TestList.Add(new Test { id = 4, name = "Alok", city = "Delhi" });
        TestList.Add(new Test { id = 5, name = "Amar", city = "London" });
    }

}
public class Test
{
	public int id { get; set; }
	public string name { get; set; }
	public string city { get; set; }
}