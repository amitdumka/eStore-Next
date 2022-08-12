using AKS.Shared.Commons.Models.Inventory;
using System.Text.Json;

namespace AKS.Payroll.Forms.Inventory
{
    public class Utils
    {
        public static DateTime ToDate(string date)
        {
            char c = '-';
            if (date.Contains('/')) c = '/';

            var d = date.Split(c);
            return new DateTime(Int32.Parse(d[2].Split(" ")[0]), Int32.Parse(d[1]), Int32.Parse(d[0]));
        }

        public static int ReadInt(TextBox t)
        {
            return Int32.Parse(t.Text.Trim());
        }

        public static decimal ReadDecimal(TextBox t)
        {
            return decimal.Parse(t.Text.Trim());
        }

        public static decimal ToDecimal(string val)
        {
            return Decimal.Round(decimal.Parse(val.Trim()), 2);
        }

        public static async Task ToJsonAsync<T>(string fileName, List<T> ObjList)
        {
            // string fileName = "WeatherForecast.json";
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, ObjList);
            await createStream.DisposeAsync();
        }

        public static async Task<List<PurchaseItem>?> FromJson<T>(string filename)
        {
            using FileStream openStream = File.OpenRead(filename);
            return JsonSerializer.Deserialize<List<PurchaseItem>>(openStream);
        }

        public static async Task<List<T>?> FromJsonToObject<T>(string filename)
        {
            using FileStream openStream = File.OpenRead(filename);
            return JsonSerializer.Deserialize<List<T>>(openStream);
        }
    }
}