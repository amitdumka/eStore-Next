using System.Collections.ObjectModel;
using AKS.Shared.Commons.Ops;
using Syncfusion.Maui.Scheduler;

namespace eStore_Maui.Pages;

public partial class DashboardPage : ContentPage
{
    public string StoreName { get; set; }
    void Init()
    {

        if (CurrentSession.IsLoggedIn)
        {
            HeaderText.Text = $"Welcome to {CurrentSession.StoreCode}";

        }
        var Meetings = new ObservableCollection<Meeting>();
        Meeting meeting = new Meeting {
            Background= Brush.Orange, From=DateTime.Now.Date.AddHours(-2),
            To=DateTime.Now.Date.AddHours(2), IsAllDay=false, EventName="Travling to Dumka"
        };
        Meeting meeting2 = new Meeting
        {
            Background = Brush.RosyBrown,
            From = DateTime.Now.Date.AddHours(-6),
            To = DateTime.Now.Date.AddHours(-3),
            IsAllDay = false,
            EventName = "No Work"
        };
        Meetings.Add(meeting);
        Meetings.Add(meeting2);
        this.sch.AppointmentsSource = Meetings;
    }
	public DashboardPage()
	{
		InitializeComponent();
        Init();
	}

    void Picker_SelectedIndexChanged(System.Object sender, System.EventArgs e)
    {
		sch.View = ((SchedulerView)Enum.GetNames(typeof(SchedulerView)).ToList().IndexOf(pck.SelectedItem.ToString()));
    }

    void OnSaveButtonClicked(System.Object sender, System.EventArgs e)
    {
        ImageSource? source = SignPad.ToImageSource();
    }
     void OnClearButtonClicked(object? sender, EventArgs e)
    {
         SignPad.Clear();
    }

    void ClearButton_Clicked(System.Object sender, System.EventArgs e)
    {
        SignPad.Clear();
    }

    void SaveButton_Clicked(System.Object sender, System.EventArgs e)
    {
        ImageSource? source = SignPad.ToImageSource();
        
    }
}
/// <summary>    
/// Represents the custom data properties.    
/// </summary>  
public class Meeting
{
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public bool IsAllDay { get; set; }
    public string EventName { get; set; }
    public TimeZoneInfo StartTimeZone { get; set; }
    public TimeZoneInfo EndTimeZone { get; set; }
    public Brush Background { get; set; }
}