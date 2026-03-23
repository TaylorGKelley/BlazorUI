namespace BlazorUI.Services
{
    public class cApplicationState
    {
        #region Class Declarations
        private Dictionary<string, object> mdictStateStore { get; set; } = new Dictionary<string, object>();
        #endregion

        #region Values Management
        public bool fncStateItemExists(string pstrKey)
        {
            return mdictStateStore.ContainsKey(pstrKey);
        }
        public void fncAddStateItem<T>(string pstrKey, T pobjValue)
        {
            object? objValueToStore = null;

            if (pobjValue is string ||
                pobjValue is short ||
                pobjValue is int ||
                pobjValue is long ||
                pobjValue is double ||
                pobjValue is bool ||
                pobjValue is DateTime)
            {
                objValueToStore = pobjValue;
            }
            else
            {
                objValueToStore = System.Text.Json.JsonSerializer.Serialize(pobjValue);
            }

            if (mdictStateStore.ContainsKey(pstrKey))
            {
                mdictStateStore[pstrKey] = objValueToStore;
            }
            else
            {
                mdictStateStore.Add(pstrKey, objValueToStore);
            }
            NotifyStateChanged();
        }
        public T? fncGetStateItem<T>(string pstrKey)
        {

            if (mdictStateStore.TryGetValue(pstrKey, out object? objValue))
            {
                if (typeof(T) == typeof(string) ||
                    typeof(T) == typeof(short) ||
                    typeof(T) == typeof(int) ||
                    typeof(T) == typeof(long) ||
                    typeof(T) == typeof(double) ||
                    typeof(T) == typeof(bool) ||
                    typeof(T) == typeof(DateTime))
                {
                    return (T)objValue;
                }
                else
                {
                    return System.Text.Json.JsonSerializer.Deserialize<T>(objValue.ToString()!);
                }
            }
            else
            {
                return default;
            }
        }
        public void fncRemoveStateItem(string pstrKey)
        {
            mdictStateStore.Remove(pstrKey);
            NotifyStateChanged();
        }
        #endregion

        #region NotifyStateChanged
        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();
        #endregion
    }
}
