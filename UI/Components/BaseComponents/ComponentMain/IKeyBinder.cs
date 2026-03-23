using Microsoft.JSInterop;

namespace BlazorUI.Components.BaseComponents.ComponentMain
{
    public interface IKeyBinder
    {
        public enum enmExtraKey
        {
            typNone = 0,
            typCTRL = 10,
            typALT = 20,
            typSHIFT = 30,
            typMETA = 40,
        }

        public void fncKeyBind(System.String pstrKey, enmExtraKey penmExtraKey, System.String pstrKeyBindIdentifier = "Default");

        [JSInvokable("subHandleKeyPressed")]
        public void subHandleKeyPressed(System.String pstrKeyBindIdentifier);

        public abstract void subKeyPressed(System.String pstrKeyBindIdentifier);
    }
}
