using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.Inputs.Select
{
    public partial class Option<TValue>
    {
        [CascadingParameter(Name = "ParentSelect")] public SelectInput<TValue>? mobjParentInput { get; set; }
        [Parameter] public System.Boolean SelectAll { get; set; } = false;
        [Parameter, EditorRequired] public TValue Value { get; set; } = default(TValue);
        [Parameter, EditorRequired] public RenderFragment ChildContent { get; set; }

        private System.Boolean mblnIsSelected => (SelectAll && mobjParentInput?.mobjCurrentValue.Count == mobjParentInput?.malobjOptions.Count) || (System.Boolean)(mobjParentInput?.mobjCurrentValue.Contains(Value) ?? false);

        protected override void OnInitialized()
        {
            if (!SelectAll) mobjParentInput?.fncAddOption(this);
        }

        public void OnValueChanged(ChangeEventArgs args)
        {
            if (SelectAll)
            {
                mobjParentInput?.subToggleSelectAll();
            }
            else
            {
                mobjParentInput?.subUpdateSelectedValue(Value, mblnIsSelected);
            }
        }
    }
}
