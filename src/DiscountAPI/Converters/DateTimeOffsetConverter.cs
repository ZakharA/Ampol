using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace DiscountAPI.Converters
{
    public class DateTimeOffsetConverter : JsonConverter<DateTimeOffset>
    {
        private const string DateFormat = "dd-MMM-yyyy";

        public override DateTimeOffset Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Debug.Assert(typeToConvert == typeof(DateTimeOffset));
            var dateString = reader.GetString();
            if (DateTimeOffset.TryParseExact(dateString, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal, out var date))
            {
                return date;
            }

            // Fallback for standard ISO 8601 format
            if (DateTimeOffset.TryParse(dateString, out date))
            {
                return date;
            }

            throw new JsonException($"Unable to convert \"{dateString}\" to DateTimeOffset. Expected format: {DateFormat}");
        }

        public override void Write(Utf8JsonWriter writer, DateTimeOffset value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
        }
    }
}