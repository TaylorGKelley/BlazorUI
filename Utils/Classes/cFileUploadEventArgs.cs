namespace BlazorUI.Utils.Classes
{
    public class cFileUploadEventArgs
    {
        public cFileUpload objFileUpload { get; set; } = new cFileUpload();
        //public Microsoft.AspNetCore.Components.Forms.IBrowserFile? objFile { get; set; } = null;

        public cFileUploadEventArgs(cFileUpload pobjFileUpload)
        {
            objFileUpload = pobjFileUpload;
            //objFile = pobjFile;
        }
    }
}
