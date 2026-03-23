using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using BlazorUI.Services;
using static BlazorUI.Components.BaseComponents.ComponentMain.IKeyBinder;

namespace BlazorUI.Components.BaseComponents.ComponentMain
{
    public partial class ComponentMain : cErrorFunctions, IKeyBinder, ICookieActions
    {
        #region Class Declarations
        [Inject] protected Services.Toast.cToastService mobjToastService { get; set; }
        [Inject] protected IJSRuntime mobjJSRuntime { get; set; }
        [Inject] protected NavigationManager mobjNavigationManager { get; set; }
        [Inject] protected cApplicationState mobjApplicationState { get; set; }

        //private const System.String mstrJSCookiePath = "./js/Utils/Cookies.js";
        //private const System.String mstrJSKeyPressBinderPath = "./js/Utils/KeyPressBinder.js";
        #endregion

        #region OnAfterRenderAsync
        protected override async System.Threading.Tasks.Task OnAfterRenderAsync(System.Boolean firstRender)
        {
            try
            {
                if (firstRender)
                {
                    // insert script into body tag so that any component can call the custom js eval function.
                    // This allows components to call a js function and pass parameters in available as `parameters[i]`
                    await mobjJSRuntime.InvokeVoidAsync("eval", @"
                        const scriptId = 'base_component_script';

                        if (!document.getElementById(scriptId)) {
                            const script = document.createElement('script');
                            script.id = scriptId;
                            script.type = 'text/javascript';

                            script.textContent = `
                                /**
                                    * Pass in strJSCode as a raw code block, this function will then execute it and pass any extra parameters in as params[0],
                                    * if you want to return a value, simply call return val...
                                    */
                                function js(strJSCode, ...parameters) {
                                    // Wrap code in function with access to parameters
                                    const code = eval(\`(...params) => {
                                        \${ strJSCode }
                                        }\`);

                                    // Call function
                                    return code(...parameters);
                                }
                            `;

                            document.body.appendChild(script);
                        }
                    ");
                }
            }
            catch (System.Exception ex) {
                // Silently fail. All instances of this *could* fail, but most likely at least one instance will work and all components will be working.
            }
        }
        #endregion

        #region subToast
        public override void subToast(Services.Toast.enumToastType penmToastType, System.String pstrTitle, System.String pstrContent = "", Services.Toast.cToastAction? pobjToastAction = null)
        {
            mobjToastService.fncToast(penmToastType, pstrTitle, pstrContent, pobjToastAction?.mstrDisplayText, pobjToastAction?.mactOnActionClick);
        }
        #endregion

        #region fncKeyBind
        /// <summary>
        /// This function should be called upon loading the blazor component once JS is available (i.e. OnAfterRenderAsync for server components). Be sure the function subKeyPressed is overridden.
        /// </summary>
        /// <param name="pstrKey">String of key value (i.e. "k", "s")</param>
        /// <param name="penmExtraKey">The modifier key that is held (i.e. CTRL, ALT)</param>
        /// <param name="pstrKeyBindIdentifier">Optional param, value of "Default" if not specified</param>
        /// <exception cref="System.Exception"></exception>
        public async void fncKeyBind(System.String pstrKey, enmExtraKey penmExtraKey, System.String pstrKeyBindIdentifier = "Default")
        {
            try
            {
                // Init KeyBind JS function
                //IJSObjectReference module = await mobjJSRuntime.InvokeAsync<IJSObjectReference>("import", mstrJSKeyPressBinderPath);
                //await module.InvokeVoidAsync("initKeyBinder", pstrKey, penmExtraKey, pstrKeyBindIdentifier, DotNetObjectReference.Create(this));
                await mobjJSRuntime.InvokeVoidAsync("js", @"
                    const key = params[0];
                    const extraKey = params[1];
                    const keyBindIdentifier = params[2];
                    const DotNet = params[3];

                    window.addEventListener('keydown', (e) => {
                        let extraKeyPressed;

                        // Check if extra key is pressed
                        switch (extraKey) {
                            case 0: // typNone
                                extraKeyPressed = true;
                                break;
                            case 10: // typCTRL
                                extraKeyPressed = e.ctrlKey;
                                break;
                            case 20: // typALT
                                extraKeyPressed = e.altKey;
                                break;
                            case 30: // typSHIFT
                                extraKeyPressed = e.shiftKey;
                                break;
                            case 40: // typMETA
                                extraKeyPressed = e.metaKey;
                                break;
                        }

                        // if key compo is pressed, prevent default and execute dotnet function
                        if (key?.toUpperCase() === e.key?.toUpperCase() && extraKeyPressed) {
                            e.preventDefault();
                            DotNet.invokeMethodAsync('subHandleKeyPressed', keyBindIdentifier);
                        }
                    });", pstrKey, penmExtraKey, pstrKeyBindIdentifier, DotNetObjectReference.Create(this));
            }
            catch (System.Exception ex)
            {
                subSetLastError(System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }
        #endregion

        #region subKeyPressed
        /// <summary>
        /// Private function called by JavaScript which calls subKeyPressed. Please override subKeyPressed only, do not call this function manually.
        /// </summary>
        [JSInvokable("subHandleKeyPressed")]
        public void subHandleKeyPressed(System.String pstrKeyBindIdentifier)
        {
            // This function exists so that developers only need to override subKeyPressed
            // without having to declare it as [JSInvokable] to help prevent errors in development
            subKeyPressed(pstrKeyBindIdentifier);
        }

        /// <summary>
        /// Run actions when key shortcuts are pressed.
        /// Use switch statement to run specific actions if there are mutliple shortcuts per component.
        /// Register these shortcuts in the fncKeyBind function ran in OnAfterRenderAsync (for SSR) or OnInitializedAsync/OnParamtersSetAsync (for WASM)
        /// If no Key Bind identifier is specified when registering in fncKeyBind,
        /// </summary>
        /// <param name="pstrKeyBindIdentifier">Specified in fncKeyBind with default value of "Default".</param>
        public virtual void subKeyPressed(System.String pstrKeyBindIdentifier)
        {
            throw new System.NotImplementedException("subKeyPressed must be overriden when registering a key press with fncKeyBind");
        }
        #endregion

        #region Cookies
        #region subSetCookieAsync
        /// <summary>
        /// Sets cookie in browser via JSInterop with basic expires value.
        /// </summary>
        /// <param name="pstrCookieName">Name/Key of cookie</param>
        /// <param name="pobjCookieValue">Value to be set with cookie. Use any value and it will be serialized to JSON for storage, then deserialized when retrieving with subGetCookieAsync</param>
        /// <param name="pdteExpires">Set a date/time for when the cookie should expire. Leave empty for using as a session cookie (Cleared upon leaving the site)</param>
        public async System.Threading.Tasks.Task subSetCookieAsync(System.String pstrCookieName, System.Object pobjCookieValue, System.DateTime? pdteExpires = null)
        {
            System.String strCookieValue = System.Text.Json.JsonSerializer.Serialize(pobjCookieValue);
            //IJSObjectReference objJSCookieModule = await mobjJSRuntime.InvokeAsync<IJSObjectReference>("import", mstrJSCookiePath);
            //await objJSCookieModule.InvokeVoidAsync("setCookie", pstrCookieName, strCookieValue, pintDaysTillExpiration);
            await mobjJSRuntime.InvokeVoidAsync("js", @"
                const name = params[0];
                const value = params[1];
                const expirationDate = params[2];

                var expires = '';
                if (expirationDate) {
                    expires = '; expires=' + expirationDate.toUTCString();
                }
                document.cookie = name + '=' + (value || '') + expires + '; path=/';
                ", pstrCookieName, strCookieValue, pdteExpires);

            //objJSCookieModule?.DisposeAsync();
        }
        #endregion

        #region fncGetCookieAsync
        /// <summary>
        /// Gets cookie from browser using JSInterop
        /// </summary>
        /// <typeparam name="T">Expected return type of the cookie's value. Value is stored as JSON to allow for any value, then deserialized before returning.</typeparam>
        /// <param name="pstrCookieName">Name/Key of cookie</param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task<T?> fncGetCookieAsync<T>(System.String pstrCookieName)
        {
            //IJSObjectReference objJSCookieModule = await mobjJSRuntime.InvokeAsync<IJSObjectReference>("import", mstrJSCookiePath);

            //System.String? strCookieValue = await objJSCookieModule.InvokeAsync<System.String?>("getCookie", pstrCookieName);

            System.String? strCookieValue = await mobjJSRuntime.InvokeAsync<System.String?>("js", @"
                const name = params[0];

                var nameEQ = name + '=';
                var ca = document.cookie.split(';');
                for (var i = 0; i < ca.length; i++) {
                    var c = ca[i];
                    while (c.charAt(0) == ' ') c = c.substring(1, c.length);
                    if (c.indexOf(nameEQ) == 0) return c.substring(nameEQ.length, c.length);
                }
                return null;", pstrCookieName);

            //objJSCookieModule?.DisposeAsync();

            return System.Text.Json.JsonSerializer.Deserialize<T>(strCookieValue ?? "null");
        }
        #endregion

        #region subDeleteCookieAsync
        /// <summary>
        /// Deletes a cookie
        /// </summary>
        /// <param name="pstrCookieName"></param>
        /// <returns></returns>
        public async System.Threading.Tasks.Task subDeleteCookieAsync(System.String pstrCookieName)
        {
            //IJSObjectReference objJSCookieModule = await mobjJSRuntime.InvokeAsync<IJSObjectReference>("import", mstrJSCookiePath);

            //System.String? strCookieValue = await objJSCookieModule.InvokeAsync<System.String?>("deleteCookie", pstrCookieName);

            System.String? strCookieValue = await mobjJSRuntime.InvokeAsync<System.String?>("js", @"
                 document.cookie = name + '=; Path=/; Expires=Thu, 01 Jan 1970 00:00:01 GMT;';
            ", pstrCookieName);

            //objJSCookieModule?.DisposeAsync();
        }
        #endregion
        #endregion

        #region subGenerateRandomHex
        /// <summary>
        /// This is used for things like InputMain to generate a random ID for each input. A GUID could also be used, but a hex value is more easily readable
        /// </summary>
        /// <returns></returns>
        internal static System.String subGenerateRandomHex()
        {
            System.Random objRandom = new System.Random();
            System.String strId = System.String.Format("{0:X6}", objRandom.Next(0x1000000));

            return strId;
        }
        #endregion
    }
}
