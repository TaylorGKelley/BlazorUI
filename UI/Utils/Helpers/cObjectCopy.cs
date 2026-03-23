namespace BlazorUI.Utils.Helpers
{
    public static class cObjectCopy
    {
        public static T fncDeepCopy<T>(T obj)
        {
            var strJson = System.Text.Json.JsonSerializer.Serialize(obj);
            return System.Text.Json.JsonSerializer.Deserialize<T>(strJson);
        }
    }
}
