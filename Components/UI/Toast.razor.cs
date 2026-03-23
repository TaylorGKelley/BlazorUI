using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BlazorUI.Services.Toast;

namespace BlazorUI.Components.UI
{
    public partial class Toast : Components.BaseComponents.ComponentMain.ComponentMain, IAsyncDisposable
    {
        #region Class Declarations
        [Parameter] public EventCallback OnClose { get; set; }
        [Parameter] public EventCallback OnActionClick { get; set; }
        [Parameter] public sruToast fsruToast { get; set; }
        [Parameter] public System.Int32 AnimationDurationMS { get; set; } = 300;


        private ElementReference? fobjToastRef { get; set; } = null;
        private System.Boolean fblnHidden = true;
        private static readonly System.Int16 mintCloseToastSeconds = 4;

        IJSObjectReference? mobjJSToast = null;
        #endregion

        #region fncShow
        protected override async Task OnAfterRenderAsync(System.Boolean firstRender)
        {
            try
            {
                if (!firstRender) return;

                fblnHidden = fsruToast.mblnHidden;
                //IJSObjectReference objJSToastModule = await mobjJSRuntime.InvokeAsync<IJSObjectReference>("import", "./js/Components/Toast/index.js");

                //mobjJSToast = await objJSToastModule.InvokeAsync<IJSObjectReference>("initToast", fobjToastRef, mintCloseToastSeconds, DotNetObjectReference.Create(this));
                mobjJSToast = await mobjJSRuntime.InvokeAsync<IJSObjectReference>("js", @"
                    const toastRef = params[0];
                    const closeTimerDelaySeconds = params[1];
                    const DotNet = params[2];
                    let closeTimer;

                    const initializeCloseTimer = () => {
                        closeTimer = setTimeout(() => {
                            DotNet.invokeMethodAsync('fncHide');
                        }, closeTimerDelaySeconds * 1000);
                    }

                    initializeCloseTimer();

                    if (toastRef) {
                        toastRef.addEventListener('mouseleave', initializeCloseTimer);

                        toastRef.addEventListener('mouseenter', () => clearTimeout(closeTimer));
                    }

                    return {
                        dispose: () => {
                            clearTimeout(closeTimer);
                            toastRef.removeEventListener('mouseleave', initializeCloseTimer);
                            toastRef.removeEventListener('mouseenter', () => clearTimeout(closeTimer));
                        }
                    }", fobjToastRef, mintCloseToastSeconds, DotNetObjectReference.Create(this));
            }
            catch (System.Exception ex)
            {
                subSetLastError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region fncHide
        [JSInvokable("fncHide")]
        public async Task fncHide()
        {
            fblnHidden = true;
            await OnClose.InvokeAsync();
        }
        #endregion

        #region subClose
        private void subClose()
        {
            fncHide();
            mobjJSToast?.InvokeVoidAsync("displose");
        }
        #endregion

        #region subPerformAction
        private async void subPerformAction()
        {
            await OnActionClick.InvokeAsync();
            if (mobjJSToast != null) await mobjJSToast.InvokeVoidAsync("dispose");
            await fncHide();
        }
        #endregion

        #region DisposeAsync
        public async ValueTask DisposeAsync()
        {
            if (mobjJSToast != null)
            {
                await mobjJSToast.DisposeAsync();
            }
        }
        #endregion
    }
}
