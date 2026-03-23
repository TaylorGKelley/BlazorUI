using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.BaseComponents.InputMain
{
    public partial class InputMain<TValue> : cBindingValue<TValue>
    {
        #region Params
        [CascadingParameter(Name = "FormState")] public Components.Inputs.Form.Classes.cFormState mobjFormState { get; set; }

        private System.String mstrUniqueId = "input-" + subGenerateRandomHex();
        //[Parameter]
        public System.String Id
        {
            get => mstrUniqueId;
            set
            {
                mstrUniqueId = $"{value}-{subGenerateRandomHex()}";
            }
        }
        [Parameter] public System.String Label { get; set; } = System.String.Empty;
        [Parameter] public System.String Placeholder { get; set; } = System.String.Empty;
        [Parameter] public System.Boolean Required { get; set; } = false;
        [Parameter] public System.Boolean Optional { get; set; } = false;
        /// <summary>
        /// * Optional *
        /// Default value of false
        /// </summary>
        [Parameter] public System.Boolean Disabled { private get; set; } = false;
        [Parameter] public System.String ErrorMessage { get; set; } = System.String.Empty;
        [Parameter] public System.String WarningMessage { get; set; } = System.String.Empty;
        [Parameter(CaptureUnmatchedValues = true)] public System.Collections.Generic.Dictionary<System.String, System.Object>? Attributes { get; set; }
        #endregion

        #region Props
        public System.String prpErrorMessage
        {
            get
            {
                if (!System.String.IsNullOrEmpty(ErrorMessage))
                {
                return ErrorMessage;
                }
                else if (mobjFormState?.mdictFormFields.ContainsKey(Id) ?? false && !System.String.IsNullOrEmpty(mobjFormState?.mdictFormFields[Id].mstrErrorMessage))
                {
                    return mobjFormState?.mdictFormFields[Id].mstrErrorMessage!;
                }

                return System.String.Empty;
            }
            set
            {
                if (mobjFormState!= null && mobjFormState.mdictFormFields.ContainsKey(Id)) mobjFormState.mdictFormFields[Id].mstrErrorMessage = value;
            }
        }
        protected System.Boolean prpIsDisabled => mobjFormState?.mblnIsDisabled == true ? mobjFormState.mblnIsDisabled : Disabled;
        #endregion

        #region subRegisterFormField
        protected void subRegisterFormField()
        {
            if (mobjFormState == null)
            {
                return;
            }

            Inputs.Form.Classes.cFormField objFormField = new Inputs.Form.Classes.cFormField
            {
                mstrFormFieldId = Id,
                mactRunValidation = subRunValidation,
                mblnRequired = Required,
            };

            if (mobjFormState.mdictFormFields.ContainsKey(Id))
            {
                mobjFormState.mdictFormFields[Id] = objFormField;
            }
            else
            {
                mobjFormState.mdictFormFields.Add(Id, objFormField);
            }
        }
        #endregion

        #region subRunValidation
        protected virtual void subRunValidation()
        {
            throw new System.NotImplementedException($"{System.Reflection.MethodBase.GetCurrentMethod()?.Name ?? "Method"} not overridden");
        }
        #endregion
    }
}
