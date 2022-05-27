using System.Text;
using System.Diagnostics;
using System.Text.Json;
using eStoreMobileX.Data.DataModels.Base;

namespace eStoreMobileX.Data.RemoteServer
{
    public static class RemoteConfig
    {
        public static readonly string API_URL = "https://www.aprajitaretails.in/api/";

    }

    /// <summary>
    /// List of WebAPI
    /// </summary>
    public class WebAPI
    {
        public static readonly string APIBase = "https://www.aprajitaretails.in/api/";
        public static readonly string Users = "users";
        public static readonly string Employees = "employees";
        public static readonly string Attendances = "attendances";
        public static readonly string CommonTypes = "enumtypes";
        public static readonly string Products = "productItems";
        public static readonly string InvoiceItems = "invoiceItems";
        public static readonly string Invoices = "invoices";
        public static readonly string Stocks = "stocks";
        public static readonly string ProductStockViews = "productStockViews";
        public static readonly string InvoicePayments = "invoicePayments";

    }

    /// <summary>
    /// Remote Single Server : It is used for single services. 
    /// </summary>
    public class RemoteSingleServer
    {

        private readonly JsonSerializerOptions serializerOptions;
        private readonly HttpClient client;
        private readonly string restUrl;
        private readonly string APIName;
        public RemoteSingleServer(string url, string name)
        {

            restUrl = url;
            APIName = name;
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        //Customer Url based.
        public async Task<T> GetByUrl<T>(string url)
        {
            Uri uri = new Uri(string.Format(url, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return default(T);
        }

        public async Task<T> PostByUrl<T>(string url, string queryParams)
        {
            Uri uri = new Uri(string.Format(url, string.Empty));
            try
            {
                StringContent qParams = new StringContent(queryParams, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, qParams);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return default(T);
        }



    }

    /// <summary>
    /// Remote Server : It fetch and store data to remote server.
    ///                It uses web API and perform data operations.
    /// </summary>

    public class RemoteServer<T>
    {
        private JsonSerializerOptions serializerOptions;
        private HttpClient client;
        private string restUrl;
        private string APIName;

        public RemoteServer(string url, string name)
        {
            restUrl = url;
            APIName = name;
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }

        public async Task<bool> DeleteAsync(int id)
        {
            Uri uri = new Uri(string.Format(restUrl, id));

            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($@"\t{APIName} successfully deleted.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"\t{APIName}:\tERROR {0}", ex.Message);
            }
            return false;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            Uri uri = new Uri(string.Format(restUrl, id));

            try
            {
                HttpResponseMessage response = await client.DeleteAsync(uri);

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($@"\t{APIName} successfully deleted.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"\t{APIName}:\tERROR {0}", ex.Message);
            }
            return false;
        }
        //TODO: here need to implement to take store id as optional paramater. 
        public async Task<List<T>> RefreshDataAsync()
        {
            List<T> Items = new List<T>();

            Uri uri = new Uri(string.Format(restUrl, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<T>>(content, serializerOptions);
                }
                else
                {
                    Debug.WriteLine("Error occured!, No Data has returned");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return Items;
        }

        public async Task<bool> SaveAsync(T item, bool isNewItem)
        {
            Uri uri = new Uri(string.Format(restUrl, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<T>(item, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($@"\t{APIName} successfully saved.");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"\t{APIName}: \tERROR {0}", ex.Message);
                return false;
            }
        }
        public async Task<bool> SaveRangeAsync(List<T> item, bool isNewItem)
        {
            string range = "/AddRange";
            if (!isNewItem)
                range = "/UpdateRange";
            Uri uri = new Uri(string.Format(restUrl + range, string.Empty));

            try
            {
                string json = JsonSerializer.Serialize<List<T>>(item, serializerOptions);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = null;
                if (isNewItem)
                {
                    response = await client.PostAsync(uri, content);
                }
                else
                {
                    response = await client.PutAsync(uri, content);
                }

                if (response.IsSuccessStatusCode)
                {
                    Debug.WriteLine($@"\t{APIName} successfully saved.");
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"\t{APIName}: \tERROR {0}", ex.Message);
                return false;
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            Uri uri = new Uri(string.Format(restUrl + $"/{id}", string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return default(T);
        }

        public async Task<T> GetByIdAsync(string id)
        {
            Uri uri = new Uri(string.Format(restUrl + $"/{id}", string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<T>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return default(T);
        }

        public async Task<List<T>> FindAsync(QueryParam queryParams)
        {
            List<T> Items = new List<T>();

            Uri uri = new Uri(string.Format(restUrl, string.Empty));
            StringContent content = new StringContent(JsonSerializer.Serialize(queryParams), Encoding.UTF8, "application/json");
            try
            {
                HttpResponseMessage response = await client.PostAsync(uri, content);
                if (response.IsSuccessStatusCode)
                {
                    string resContent = await response.Content.ReadAsStringAsync();
                    Items = JsonSerializer.Deserialize<List<T>>(resContent, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"\t{APIName}:\tFind:\tERROR {0}", ex.Message);
            }

            return Items;
        }

        //Custom Url based.
        public async Task<List<TEntity>> GetByUrl<TEntity>(string url)
        {
            Uri uri = new Uri(string.Format(url, string.Empty));
            try
            {
                HttpResponseMessage response = await client.GetAsync(uri);
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<List<TEntity>>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return default(List<TEntity>);
        }

        public async Task<TEntity> PostByUrl<TEntity>(string url, string queryParams)
        {
            Uri uri = new Uri(string.Format(url, string.Empty));
            try
            {
                StringContent qParams = new StringContent(queryParams, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(uri, qParams);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    return JsonSerializer.Deserialize<TEntity>(content, serializerOptions);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(@"\tERROR {0}", ex.Message);
            }

            return default(TEntity);
        }


    }
}
