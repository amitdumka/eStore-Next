using System.Collections;
using AKS.Shared.Payroll.Models;
using DevExpress.Maui.DataForm;
using eStore.Dev.Models;
using eStore_MauiLib.DataModels;
using eStore_MauiLib.DataModels.Payroll;

namespace eStore.Dev.Views;

public partial class TestFormPage : ContentPage
{

    //AttendanceDataModel dm;
	public TestFormPage()
	{
		InitializeComponent();
        dataForm.DataObject = new Attendance();
		//dm = new AttendanceDataModel(ConType.Hybrid);
        //dataForm.PickerSourceProvider = new ComboBoxDataProvider();
    }
}

//public class ComboBoxDataProvider : IPickerSourceProvider
//{
//    AttendanceDataModel dm;
//    public ComboBoxDataProvider()
//    {
//        dm = new AttendanceDataModel(ConType.Hybrid);
//        dm.InitContext();
//    }
//    public IEnumerable GetSource(string propertyName)
//    {
//        if (propertyName == "EmoloyeeId")
//        {
//            return CommonDataModel.GetEmployeeList(dm.GetContextLocal());
//        }
//        return null;
//    }
//}
