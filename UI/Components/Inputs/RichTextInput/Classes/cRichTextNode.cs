using System.Text.Json.Serialization;

namespace BlazorUI.Components.Inputs.RichTextInput.Classes
{
    public class cRichTextNode
    {
        [JsonPropertyName("text")]
        public System.String mstrText { get; set; }
        [JsonPropertyName("isBold")]
        public System.Boolean mblnIsBold { get; set; }
        [JsonPropertyName("isUnderline")]
        public System.Boolean mblnIsUnderline { get; set; }
        [JsonPropertyName("isItalic")]
        public System.Boolean mblnIsItalic { get; set; }
        [JsonPropertyName("link")]
        public System.String mstrLink { get; set; }
    }
}
