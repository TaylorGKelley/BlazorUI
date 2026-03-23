namespace BlazorUI.Utils.Constants
{
    internal static class cJSONConversion
    {
        internal static System.Text.Json.JsonSerializerOptions objOptions = new System.Text.Json.JsonSerializerOptions
            {
                Converters = { new System.Text.Json.Serialization.JsonStringEnumConverter() },
                WriteIndented = false,
            };
    }
}
