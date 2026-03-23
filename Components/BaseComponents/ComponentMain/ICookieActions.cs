namespace BlazorUI.Components.BaseComponents.ComponentMain
{
    public interface ICookieActions
    {
        #region subSetCookieAsync
        public System.Threading.Tasks.Task subSetCookieAsync(System.String pstrCookieName, System.Object pobjCookieValue, System.DateTime? pdteExpires);
        #endregion

        #region fncGetCookieAsync
        public System.Threading.Tasks.Task<T?> fncGetCookieAsync<T>(System.String pstrCookieName);
        #endregion

        #region subDeleteCookieAsync
        public System.Threading.Tasks.Task subDeleteCookieAsync(System.String pstrCookieName);
        #endregion

    }
}
