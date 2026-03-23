using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorUI.Components.Inputs.RichTextInput.Classes
{
    public class cRichTextImageData
    {
        [JsonPropertyName("src")]
        public System.String mstrSrc { get; set; }
        [JsonPropertyName("imageId")]
        public System.String mstrImageId { get; set; }
        [JsonPropertyName("alt")]
        public System.String? mstrAlt { get; set; }
        [JsonPropertyName("caption")]
        public System.String? mstrCaption { get; set; }
        [JsonPropertyName("width")]
        public System.Double mintWidth { get; set; }
        /// <summary>
        /// Represents the ratio of the height relative to the element's width (i.e. 1 / heightRatio (aspect ratio), or heightRatio * width = height)
        /// </summary>
        [JsonPropertyName("heightRatio")]
        public System.Single? mintHeightRatio { get; set; }

        [JsonPropertyName("align")]
        public enumTextAlign menmImageAlign { get; set; }
    }
}
