using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DeliveryApp.Formatters;

public class DateTimeJsonFormatter : JsonConverter<DateTime>
{
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        try
        {
            return DateTime.ParseExact(value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
        }
        catch (FormatException)
        {
            Console.WriteLine($"Error parsing date: {value}");
            throw;
        }
            
    }

    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString("yyyy-MM-dd HH:mm:ss"));
    }
} 