namespace BlazorUI.Utils.Classes
{
    public class cFileUpload
    {
        public Microsoft.AspNetCore.Components.Forms.IBrowserFile? mobjFile = null;
        public System.String mstrName { get; set; } = System.String.Empty;
        public System.Int64 mintSize { get; set; } = 0;
        public System.Int32 mintDuplicateNumber { get; set; } = 0;
        public System.String mstrErrorMessage { get; set; } = System.String.Empty;
    }
}
