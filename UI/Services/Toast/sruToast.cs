using Microsoft.AspNetCore.Components;

namespace BlazorUI.Services.Toast
{
    public enum enumToastType
    {
        typError = 0,
        typSuccess = 10,
        typInfo = 20,
        typWarning = 30,
        typFileUpload = 40,
    }

    public struct sruToast
    {
        public enumToastType menmToastType { get; set; }
        public System.String mstrTitle { get; set; }
        public System.String mstrContent { get; set; }
        public System.String? mstrActionDisplayText { get; set; }
        public System.Action? mactOnActionClick { get; set; }
        public System.Boolean mblnHidden { get; set; }

        public sruToast(enumToastType penmToastType, System.String pstrTitle,
                        System.String pstrContent, System.String? pstrActionDisplayText, System.Action? pactOnActionClick = null)
        {
            menmToastType = penmToastType;
            mstrTitle = pstrTitle;
            mstrContent = pstrContent;
            mstrActionDisplayText = pstrActionDisplayText;
            mactOnActionClick = pactOnActionClick;
            mblnHidden = false; // Automatically shown when added
        }
    }
}
