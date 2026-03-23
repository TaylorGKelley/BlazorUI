using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.BaseComponents.InputMain
{
    public class cBindingValue<TValue> : ComponentMain.ComponentMain
    {
        #region Params
        [Parameter] public TValue Value { get; set; }
        [Parameter] public Microsoft.AspNetCore.Components.EventCallback<TValue> ValueChanged { get; set; }

        public TValue mobjCurrentValue
        {
            get => Value;
            set
            {
                //if (Value != value)
                //{
                Value = value;
                ValueChanged.InvokeAsync(value);
                subValueChanged();
                //}
            }
        }
        #endregion

        #region subValueChanged
        protected virtual void subValueChanged()
        {
            return;
        }
        #endregion
    }
}
