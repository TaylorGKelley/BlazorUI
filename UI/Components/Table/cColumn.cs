using Microsoft.AspNetCore.Components;

namespace BlazorUI.Components.Table
{
    public enum enumTextAlign
    {
        typLeft = 0,
        typCenter = 10,
        typRight = 20,
    }

    public enum enumColumnType
    {
        typAction = 0,
        typData = 10,
    }

    public class cColumn<TData>
    {


        public cColumn(enumColumnType penmColumnType, System.String pstrFieldName, System.String pstrDisplayName, System.Boolean pblnIsHidden = false, enumTextAlign penmTextAlign = enumTextAlign.typLeft, System.Int32? pintWidth = null, RenderFragment<TData>? pobjChildContent = null, Action<TData>? pactOnClick = null)
        {
            menmColumnType = penmColumnType;
            mstrFieldName = pstrFieldName;
            mstrDisplayName = pstrDisplayName;
            mblnIsHidden = pblnIsHidden;
            menmTextAlign = penmTextAlign;
            mintWidth = pintWidth;
            ChildContent = pobjChildContent;
            mactOnClick = pactOnClick;
        }

        public enumColumnType menmColumnType { get; set; }
        public System.String mstrFieldName { get; set; } = System.String.Empty;
        public System.String mstrDisplayName { get; set; } = System.String.Empty;
        public System.Boolean mblnIsHidden { get; set; } = false;
        public enumTextAlign menmTextAlign { get; set; } = enumTextAlign.typLeft;
        public System.Int32? mintWidth { get; set; } = null;
        public RenderFragment<TData>? ChildContent { get; set; } = null;
        public Action<TData>? mactOnClick { get; set; } = null;
    }
}
