namespace BlazorUI.Services.Toast
{
    public class cToastAction
    {
        public System.String mstrDisplayText { get; set; }
        public System.Action? mactOnActionClick { get; set; } = null;

        public cToastAction(System.String pstrDisplayText, System.Action? pactOnActionClick = null)
        {
            mstrDisplayText = pstrDisplayText;
            mactOnActionClick = pactOnActionClick;
        }
    }
}
