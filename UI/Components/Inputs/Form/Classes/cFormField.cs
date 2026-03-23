namespace BlazorUI.Components.Inputs.Form.Classes
{
    public class cFormField
    {
        public System.String mstrFormFieldId { get; set; } = System.String.Empty;
        public System.Action? mactRunValidation { get; set; } = null;
        public System.Boolean mblnRequired { get; set; } = false;
        public System.String? mstrErrorMessage { get; set; } = null;
        public System.String? mstrWarningMessage { get; set; } = null;
    }
}
