using System.Text.Json.Serialization;

namespace BlazorUI.Components.Inputs.RichTextInput.Classes
{
    public enum enumBlockType
    {
        typBody = 0,
        typHeading1 = 10,
        typHeading2 = 20,
        typHeading3 = 30,
        typBulletList = 40,
        typBlockQuote = 50,
        typImage = 60,
    }

    public enum enumTextAlign
    {
        typLeft = 0,
        typCenter = 10,
        typRight = 20,
    }

    public class cRichTextBlock
    {
        [JsonPropertyName("blockType")]
        public enumBlockType menmBlockType { get; set; }

        [JsonPropertyName("indentation")]
        public System.Int32? mintIndentation { get; set; }

        [JsonPropertyName("textAlign")]
        public enumTextAlign? menmTextAlign { get; set; }

        [JsonPropertyName("textNodes")]
        public List<cRichTextNode>? malobjTextNodes { get; set; }

        [JsonPropertyName("data")]
        public cRichTextImageData? mobjImageData { get; set; }
    }
}
