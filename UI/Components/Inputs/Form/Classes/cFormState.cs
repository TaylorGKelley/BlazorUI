namespace BlazorUI.Components.Inputs.Form.Classes
{
    public class cFormState
    {
        public System.Boolean mblnIsDisabled { get; set; } = false;
        public System.String? mstrGlobalErrorMessage { get; set; } = null;

        public System.Collections.Generic.Dictionary<System.String, cFormField> mdictFormFields { get; set; } =
            new System.Collections.Generic.Dictionary<System.String, cFormField>();
    }
}
