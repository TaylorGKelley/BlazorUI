namespace BlazorUI.Services.Toast
{
    public class cToastService
    {
        #region Service Events
        public event Action OnStateChange;
        public void NotifyStateChanged() => OnStateChange?.Invoke();
        #endregion

        #region Class Declarations
        public Dictionary<System.Int32, sruToast> fdictToasts = new Dictionary<System.Int32, sruToast>();
        #endregion

        #region fncToast
        public void fncToast(enumToastType penmToastType, System.String pstrTitle, System.String pstrContent, System.String? pstrActionDisplayText, System.Action? pactOnActionClick)
        {
            if (fdictToasts.Count < 6)
            {
                System.Int32 intNextToastId = fdictToasts.Count > 0 ? fdictToasts.MaxBy(kvp => kvp.Key).Key + 1 : 0;
                fdictToasts.Add(intNextToastId, new sruToast(penmToastType, pstrTitle, pstrContent, pstrActionDisplayText, pactOnActionClick));
                NotifyStateChanged();
            }
        }
        #endregion

        #region subRemoveToast
        public void subRemoveToast(System.Int32 pintToastId)
        {
            // Was the Toast closed by the user before the timer ran out?
            if (fdictToasts.ContainsKey(pintToastId))
            {
                fdictToasts.Remove(pintToastId);
                NotifyStateChanged();
            }
        }
        #endregion
    }
}
