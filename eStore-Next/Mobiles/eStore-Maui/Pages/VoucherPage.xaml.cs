namespace eStore_Maui.Pages;

public partial class VoucherPage : ContentPage
{

	List<Test> TestList;
	public VoucherPage()
	{
		InitializeComponent();
		init();

	}

	void init()
	{
		TestList = new List<Test>();
		TestList.Add(new Test { id=1, name="Amit", city="Dumka" });
        TestList.Add(new Test { id = 1, name = "Amit", city = "Deoghar" });
        TestList.Add(new Test { id = 2, name = "Amit Kumar", city = "Bang" });
        TestList.Add(new Test { id = 3, name = "Ajit", city = "Dumka" });
        TestList.Add(new Test { id = 4, name = "Alok", city = "Delhi" });
        TestList.Add(new Test { id = 5, name = "Amar", city = "London" });
    }
}


class Test
{
	public int id { get; set; }
	public string name { get; set; }
	public string city { get; set; }
}