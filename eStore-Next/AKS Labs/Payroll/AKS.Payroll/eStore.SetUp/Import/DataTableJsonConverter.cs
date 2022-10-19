using System.Data;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace eStore.SetUp.Import
{
    public class DataTableJsonConverter : JsonConverter<DataTable>
    {
        public override DataTable Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var jsonDoc = JsonDocument.ParseValue(ref reader);
            JsonElement rootElement = jsonDoc.RootElement;
            DataTable dataTable = rootElement.JsonElementToDataTable();
            return dataTable;
        }

        public override void Write(Utf8JsonWriter jsonWriter, DataTable value, JsonSerializerOptions options)
        {
            jsonWriter.WriteStartArray();
            foreach (DataRow dr in value.Rows)
            {
                jsonWriter.WriteStartObject();
                foreach (DataColumn col in value.Columns)
                {
                    string key = col.ColumnName.Trim();

                    Action<string> action = GetWriteAction(dr, col, jsonWriter);
                    action.Invoke(key);

                    static Action<string> GetWriteAction(
                        DataRow row, DataColumn column, Utf8JsonWriter writer) => row[column] switch
                        {
                            // bool
                            bool value => key => writer.WriteBoolean(key, value),

                            // numbers
                            byte value => key => writer.WriteNumber(key, value),
                            sbyte value => key => writer.WriteNumber(key, value),
                            decimal value => key => writer.WriteNumber(key, value),
                            double value => key => writer.WriteNumber(key, value),
                            float value => key => writer.WriteNumber(key, value),
                            short value => key => writer.WriteNumber(key, value),
                            int value => key => writer.WriteNumber(key, value),
                            ushort value => key => writer.WriteNumber(key, value),
                            uint value => key => writer.WriteNumber(key, value),
                            ulong value => key => writer.WriteNumber(key, value),

                            // strings
                            DateTime value => key => writer.WriteString(key, value),
                            Guid value => key => writer.WriteString(key, value),

                            _ => key => writer.WriteString(key, row[column].ToString())
                        };
                }
                jsonWriter.WriteEndObject();
            }
            jsonWriter.WriteEndArray();
        }
    }
}