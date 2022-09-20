using CommunityToolkit.Maui.Views;
using eStore_Maui.ViewModels.Entry;
namespace eStore_Maui.Pages.Payroll.Entry;
public partial class AttendanceEntryView : Popup
{
	
	private AttendanceEntryViewModel entryViewModel;

	public AttendanceEntryView(AttendanceEntryViewModel vm, bool isNew)
	{
		InitializeComponent();
		entryViewModel = vm;
		this.BindingContext = entryViewModel; 
	}
}