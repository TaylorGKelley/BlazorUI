namespace BlazorUI.Components.Table
{
    public static class cExportTable
    {
        #region fncExportToCSV
        /// <summary>
        /// Takes in a list of columns and data and exports it as a csv table
        /// </summary>
        /// <typeparam name="TData">The type of data in the data list</typeparam>
        /// <param name="palobjColumns">The list of column objects to map to the data list</param>
        /// <param name="palobjData">The data list</param>
        /// <returns></returns>
        public static System.String fncExportToCSVFile<TData>(System.Collections.Generic.List<cColumn<TData>> palobjColumns, System.Collections.Generic.List<TData> palobjData)
        {
            // Create a StringBuilder to construct the CSV
            var objCSVBuilder = new System.Text.StringBuilder();
            // Write the header row
            var alobjVisibleColumns = palobjColumns.Where(column => !column.mblnIsHidden).ToList();
            objCSVBuilder.AppendLine(System.String.Join(",", alobjVisibleColumns.Select(column => column.mstrDisplayName)));
            // Write the data rows
            foreach (TData objData in palobjData)
            {
                var row = alobjVisibleColumns
                    .Select(column =>
                    {
                        System.Object? objValue = subGetField(column.mstrFieldName, objData);
                        return objValue != null ? subFormatValue(objValue.ToString()!) : System.String.Empty;
                    })
                    .ToList();
                objCSVBuilder.AppendLine(System.String.Join(",", row));
            }

            System.Byte[] abytCSVFile = System.Text.Encoding.UTF8.GetBytes(objCSVBuilder.ToString());
            return Convert.ToBase64String(abytCSVFile);
        }
        #endregion

        #region subFormatValue
        private static System.String subFormatValue(System.String pstrValue)
        {
            // Escapes values to ensure proper CSV formatting
            if (pstrValue.Contains(",") || pstrValue.Contains("\"") || pstrValue.Contains("\n"))
            {
                pstrValue = pstrValue.Replace("\"", "\"\"");
                return $"\"{pstrValue}\"";
            }
            return pstrValue;
        }
        #endregion

        #region subGetField
        private static System.Object? subGetField<TData>(System.String FieldId, TData pobjData)
        {
                if (pobjData == null) return null;

                System.Reflection.PropertyInfo? objPropertyInfo = typeof(TData).GetProperty(FieldId);

                System.Object? objValue = objPropertyInfo?.GetValue(pobjData);

                return objValue;
        }
        #endregion
    }
}
