//using System;
//using Microsoft.Maui.Controls;
//using static Java.Util.Jar.Attributes;
//using static System.Net.Mime.MediaTypeNames;
//using System.Text.Json;
//using System.Diagnostics;
//using Azure;
//using Android.Net;
//using Java.Security;
//using Android.App;
//using Microsoft.Maui.Controls.PlatformConfiguration;

//namespace eStore_Maui.Test
//{
//    public class TestClass
//    {
//        public async Task<ImageSource> TakeScreenshotAsync()
//        {
//            if (Screenshot.Default.IsCaptureSupported)
//            {
//                IScreenshotResult screen = await Screenshot.Default.CaptureAsync();

//                Stream stream = await screen.OpenReadAsync();

//                return ImageSource.FromStream(() => stream);
//            }

//            return null;
//        }
//        private void VibrateStartButton_Clicked(object sender, EventArgs e)
//        {
//            int secondsToVibrate = Random.Shared.Next(1, 7);
//            TimeSpan vibrationLength = TimeSpan.FromSeconds(secondsToVibrate);

//            Vibration.Default.Vibrate(vibrationLength);
//        }

//        private void VibrateStopButton_Clicked(object sender, EventArgs e) =>
//            Vibration.Default.Cancel();
//    }

//    public class AKSGeocoding
//    {
//        public async Task<Location> GetLocationAsync(string address)
//        {
//            //string address = "Microsoft Building 25 Redmond WA USA";
//            IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(address);

//            Location location = locations?.FirstOrDefault();

//            if (location != null)
//                Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
//            return location;
//        }

//        private async Task<string> GetGeocodeReverseData(double latitude = 47.673988, double longitude = -122.121513)
//        {
//            IEnumerable<Placemark> placemarks = await Geocoding.Default.GetPlacemarksAsync(latitude, longitude);

//            Placemark placemark = placemarks?.FirstOrDefault();

//            if (placemark != null)
//            {
//                return
//                    $"AdminArea:       {placemark.AdminArea}\n" +
//                    $"CountryCode:     {placemark.CountryCode}\n" +
//                    $"CountryName:     {placemark.CountryName}\n" +
//                    $"FeatureName:     {placemark.FeatureName}\n" +
//                    $"Locality:        {placemark.Locality}\n" +
//                    $"PostalCode:      {placemark.PostalCode}\n" +
//                    $"SubAdminArea:    {placemark.SubAdminArea}\n" +
//                    $"SubLocality:     {placemark.SubLocality}\n" +
//                    $"SubThoroughfare: {placemark.SubThoroughfare}\n" +
//                    $"Thoroughfare:    {placemark.Thoroughfare}\n";

//            }

//            return "";
//        }
//    }

//    public class AKSPref
//    {
//        private void pref()
//        {
//            // Set a string value:
//            Preferences.Default.Set("first_name", "John");

//            // Set an numerical value:
//            Preferences.Default.Set("age", 28);

//            // Set a boolean value:
//            Preferences.Default.Set("has_pets", true);
//        }
//        private void get()
//        {
//            string firstName = Preferences.Get("first_name", "Unknown");
//            int age = Preferences.Get("age", -1);
//            bool hasPets = Preferences.Get("has_pets", false);

//            //check
//            bool hasKey = Preferences.ContainsKey("my_key");
//            //remove
//            Preferences.Remove("first_name");
//            //clear all
//            Preferences.Clear();
//        }

//    }


//    public class aksSpeak
//    {
//        public async void Speak(string text) =>
//    await TextToSpeech.Default.SpeakAsync(text);
//        CancellationTokenSource cts;

//        public async Task SpeakNowDefaultSettingsAsync()
//        {
//            cts = new CancellationTokenSource();
//            await TextToSpeech.Default.SpeakAsync("Hello World", cancelToken: cts.Token);

//            // This method will block until utterance finishes.
//        }

//        // Cancel speech if a cancellation token exists & hasn't been already requested.
//        public void CancelSpeech()
//        {
//            if (cts?.IsCancellationRequested ?? true)
//                return;

//            cts.Cancel();
//        }
//        bool isBusy = false;

//        public void SpeakMultiple()
//        {
//            isBusy = true;

//            Task.WhenAll(
//                TextToSpeech.Default.SpeakAsync("Hello World 1"),
//                TextToSpeech.Default.SpeakAsync("Hello World 2"),
//                TextToSpeech.Default.SpeakAsync("Hello World 3"))
//                .ContinueWith((t) => { isBusy = false; }, TaskScheduler.FromCurrentSynchronizationContext());
//        }
//        public async void SpeakSettings()
//        {
//            IEnumerable<Locale> locales = await TextToSpeech.Default.GetLocalesAsync();

//            SpeechOptions options = new SpeechOptions()
//            {
//                Pitch = 1.5f,   // 0.0 - 2.0
//                Volume = 0.75f, // 0.0 - 1.0
//                Locale = locales.FirstOrDefault()
//            };

//            await TextToSpeech.Default.SpeakAsync("How nice to meet you!", options);
//        }
//    }

//    //var celsius = UnitConverters.FahrenheitToCelsius(32.0);
//}

////FahrenheitToCelsius
////CelsiusToFahrenheit
////CelsiusToKelvin
////KelvinToCelsius
////MilesToMeters
////MilesToKilometers
////KilometersToMiles
////MetersToInternationalFeet
////InternationalFeetToMeters
////DegreesToRadians
////RadiansToDegrees
////DegreesPerSecondToRadiansPerSecond
////RadiansPerSecondToDegreesPerSecond
////DegreesPerSecondToHertz
////RadiansPerSecondToHertz
////HertzToDegreesPerSecond
////HertzToRadiansPerSecond
////KilopascalsToHectopascals
////HectopascalsToKilopascals
////KilopascalsToPascals
////HectopascalsToPascals
////AtmospheresToPascals
////PascalsToAtmospheres
////CoordinatesToMiles
////CoordinatesToKilometers
////KilogramsToPounds
////PoundsToKilograms
////StonesToPounds
////PoundsToStones



////trgers
////< Entry Placeholder = "Enter name" >
////    < Entry.Triggers >
////        < Trigger TargetType = "Entry"
////                 Property = "IsFocused"
////                 Value = "True" >
////            < Setter Property = "BackgroundColor"
////                    Value = "Yellow" />
////            < !--Multiple Setter elements are allowed -->
////        </Trigger>
////    </Entry.Triggers>
////</Entry>

////< ContentPage.Resources >
////    < Style TargetType = "Entry" >
////        < Style.Triggers >
////            < Trigger TargetType = "Entry"
////                     Property = "IsFocused"
////                     Value = "True" >
////                < Setter Property = "BackgroundColor"
////                        Value = "Yellow" />
////                < !--Multiple Setter elements are allowed -->
////            </Trigger>
////        </Style.Triggers>
////    </Style>
////</ContentPage.Resources>

////Data Triger
////< Entry x: Name = "entry"
////       Text = ""
////       Placeholder = "Enter text" />
////< Button Text = "Save" >
////    < Button.Triggers >
////        < DataTrigger TargetType = "Button"
////                     Binding = "{Binding Source={x:Reference entry},
////                                       Path = Text.Length}"
////                     Value = "0" >
////            < Setter Property = "IsEnabled"
////                    Value = "False" />
////            < !--Multiple Setter elements are allowed -->
////        </DataTrigger>
////    </Button.Triggers>
////</Button>

////Event trigger
//public class NumericValidationTriggerAction : TriggerAction<Entry>
//{
//    protected override void Invoke(Entry entry)
//    {
//        double result;
//        bool isValid = Double.TryParse(entry.Text, out result);
//        entry.TextColor = isValid ? Colors.Black : Colors.Red;
//    }
//}

////Multi trigger

////< Entry x: Name = "email"
////       Text = "" />
////< Entry x: Name = "phone"
////       Text = "" />
////< Button Text = "Save" >
////    < Button.Triggers >
////        < MultiTrigger TargetType = "Button" >
////            < MultiTrigger.Conditions >
////                < BindingCondition Binding = "{Binding Source={x:Reference email},
////                                            Path = Text.Length}"
////                                  Value = "0" />
////                < BindingCondition Binding = "{Binding Source={x:Reference phone},
////                                            Path = Text.Length}"
////                                  Value = "0" />
////            </ MultiTrigger.Conditions >
////            < Setter Property = "IsEnabled" Value = "False" />
////            < !--multiple Setter elements are allowed -->
////        </MultiTrigger>
////    </Button.Triggers>
////</Button>
////<PropertyCondition Property="Text"
////Value = "OK" />
////
//public class FadeTriggerAction : TriggerAction<VisualElement>
//{
//    public int StartsFrom { get; set; }

//    protected override void Invoke(VisualElement sender)
//    {
//        sender.Animate("FadeTriggerAction", new Animation((d) =>
//        {
//            var val = StartsFrom == 1 ? d : 1 - d;
//            sender.BackgroundColor = Color.FromRgb(1, val, 1);
//        }),
//        length: 1000, // milliseconds
//        easing: Easing.Linear);
//    }
//}

////web server
//public class RestService : IRestService
//{
//    HttpClient _client;
//    JsonSerializerOptions _serializerOptions;

//    public List<TodoItem> Items { get; private set; }

//    public RestService()
//    {
//        _client = new HttpClient();
//        _serializerOptions = new JsonSerializerOptions
//        {
//            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
//            WriteIndented = true
//        };
//    }
//    ///...
//    public async Task<List<TodoItem>> RefreshDataAsync()
//    {
//        Items = new List<TodoItem>();

//        Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));
//        try
//        {
//            HttpResponseMessage response = await _client.GetAsync(uri);
//            if (response.IsSuccessStatusCode)
//            {
//                string content = await response.Content.ReadAsStringAsync();
//                Items = JsonSerializer.Deserialize<List<TodoItem>>(content, _serializerOptions);
//            }
//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine(@"\tERROR {0}", ex.Message);
//        }

//        return Items;
//    }
//    public async Task SaveTodoItemAsync(TodoItem item, bool isNewItem = false)
//    {
//        Uri uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

//        try
//        {
//            string json = JsonSerializer.Serialize<TodoItem>(item, _serializerOptions);
//            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

//            HttpResponseMessage response = null;
//            if (isNewItem)
//                response = await _client.PostAsync(uri, content);
//            else
//                response = await _client.PutAsync(uri, content);

//            if (response.IsSuccessStatusCode)
//                Debug.WriteLine(@"\tTodoItem successfully saved.");
//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine(@"\tERROR {0}", ex.Message);
//        }
//    }
//    public async Task SaveTodoItemAsync(TodoItem item, bool isNewItem = false)
//    {
//        ...
//  response = await _client.PutAsync(uri, content);
//        ...
//}
//    public async Task DeleteTodoItemAsync(string id)
//    {
//        Uri uri = new Uri(string.Format(Constants.RestUrl, id));

//        try
//        {
//            HttpResponseMessage response = await _client.DeleteAsync(uri);
//            if (response.IsSuccessStatusCode)
//                Debug.WriteLine(@"\tTodoItem successfully deleted.");
//        }
//        catch (Exception ex)
//        {
//            Debug.WriteLine(@"\tERROR {0}", ex.Message);
//        }
//    }
//}

//public interface IRestService
//{
//}

//public class TodoItem
//{
//    public string ID { get; set; }
//    public string Name { get; set; }
//    public string Notes { get; set; }
//    public bool Done { get; set; }
//}

//public static string BaseAddress =
//    DeviceInfo.Platform == DevicePlatform.Android ? "http://10.0.2.2:5000" : "http://localhost:5000";
//public static string TodoItemsUrl = $"{BaseAddress}/api/todoitems/";


//{
//    ...
//  "profiles": {
//        "http": {
//            "commandName": "Project",
//      "dotnetRunMessages": true,
//      "launchBrowser": true,
//      "launchUrl": "api/todoitems",
//      "applicationUrl": "http://localhost:5000",
//      "environmentVariables": {
//                "ASPNETCORE_ENVIRONMENT": "Development"
//      }
//        },
//    ...
//  }
//}

//<? xml version = "1.0" encoding = "utf-8" ?>
//< network - security - config >
//  < domain - config cleartextTrafficPermitted = "true" >
//    < domain includeSubdomains = "true" > 10.0.2.2 </ domain >
//  </ domain - config >
//</ network - security - config >

//<? xml version = "1.0" encoding = "utf-8" ?>
//< manifest >
//    < application android:networkSecurityConfig = "@xml/network_security_config"...>
//        ...
//    </ application >
//</ manifest >

//< key > NSAppTransportSecurity </ key >
//< dict >
//    < key > NSAllowsLocalNetworking </ key >
//    < true />
//</ dict >



