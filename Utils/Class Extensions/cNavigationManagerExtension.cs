namespace BlazorUI.Utils.Class_Extensions
{
    public static class cNavigationManagerExtension
    {
        public static T? GetQueryParameterValue<T>(this Microsoft.AspNetCore.Components.NavigationManager pobjNavigationManager, System.String pstrKey)
        {
            T result;

            if (TryGetQueryParameterValue(pobjNavigationManager, pstrKey, out result))
            {
                return result;
            }

            return default(T);
        }
        public static System.Boolean TryGetQueryParameterValue<T>(this Microsoft.AspNetCore.Components.NavigationManager pobjNavigationManager, System.String pstrKey, out T pobjValue)
        {
            System.Uri objURI = pobjNavigationManager.ToAbsoluteUri(pobjNavigationManager.Uri);

            if (Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(objURI.Query).TryGetValue(pstrKey, out var strValueFromQueryString))
            {
                if (typeof(T) == typeof(System.Int32) && System.Int32.TryParse(strValueFromQueryString, out var intValue))
                {
                    pobjValue = (T)(System.Object)intValue;
                    return true;
                }

                if (typeof(T) == typeof(System.String))
                {
                    pobjValue = (T)(System.Object)strValueFromQueryString.ToString();
                    return true;
                }

                if (typeof(T) == typeof(System.Decimal) && System.Decimal.TryParse(strValueFromQueryString, out var decValue))
                {
                    pobjValue = (T)(System.Object)decValue;
                    return true;
                }

                if (typeof(T) == typeof(System.Guid) && System.Guid.TryParse(strValueFromQueryString, out var guidValue))
                {
                    pobjValue = (T)(System.Object)guidValue;
                    return true;
                }
            }

            pobjValue = default;
            return false;
        }

        public static void AddOrUpdateQueryParameter<T>(this Microsoft.AspNetCore.Components.NavigationManager pobjNavigationManager, System.String pstrKey, T pobjValue)
        {
            System.String strURI = pobjNavigationManager.Uri;
            System.UriBuilder objUriBuilder = new System.UriBuilder(strURI);
            // Parse the existing query string
            System.Collections.Generic.Dictionary<System.String, Microsoft.Extensions.Primitives.StringValues> dictQueryParams = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(objUriBuilder.Query);
            // Update or add the parameter
            dictQueryParams[pstrKey] = pobjValue?.ToString();
            // Build the updated query string
            System.Collections.Generic.List<System.String>  alstrUpdatedQuery = new System.Collections.Generic.List<System.String>();
            foreach (System.Collections.Generic.KeyValuePair<System.String, Microsoft.Extensions.Primitives.StringValues> kvpParam in dictQueryParams)
            {
                foreach (var val in kvpParam.Value)
                {
                    alstrUpdatedQuery.Add($"{kvpParam.Key}={Uri.EscapeDataString(val)}");
                }
            }
            objUriBuilder.Query = System.String.Join("&", alstrUpdatedQuery);
            // Navigate to the updated URI
            pobjNavigationManager.NavigateTo(objUriBuilder.ToString());
        }
    }
}
