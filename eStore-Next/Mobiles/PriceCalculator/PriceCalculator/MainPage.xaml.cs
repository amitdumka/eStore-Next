
namespace PriceCalculator;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage()
	{
		InitializeComponent();
	}

	

	private void CounterBtn_Clicked(object sender, EventArgs e)
	{
		decimal rate = (decimal)5.0;
		decimal mrp = decimal.Parse(MRP.Text.Trim());
		if (Fabric.IsChecked)
			rate = (decimal)5.0; 
		else if (RMZ.IsChecked)
		{
			if (mrp > 999) rate = (decimal)12.0; else rate = (decimal)5.0;
		}

		decimal baserate= Math.Round((mrp*100)/(100+rate),2);
		decimal tax = mrp - baserate;
		decimal gst = Math.Round(tax / 2,2);

		Result.Text = $" MRP:\t {mrp}\n Basic Rate: \t{baserate}\n TotalTax:\t{tax} \nTax Rate: \t{rate}\n SGST:\t{gst}\t  CGST:\t{gst}";
	}
}

