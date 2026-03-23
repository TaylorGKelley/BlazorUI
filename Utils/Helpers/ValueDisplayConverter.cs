namespace BlazorUI.Utils.Helpers
{
    public class ValueDisplayConverter
    {
        public static System.String FormatBytes(System.Int64 pintSizeBytes, System.Boolean pblnIncludeDecimal = false)
        {
            if (pintSizeBytes < 0) throw new System.ArgumentException("Bytes must be non-negative.", nameof(pintSizeBytes));

            System.String[] astrUnits = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB" };
            System.Double intCurrentSize = pintSizeBytes;
            System.Int16 pintUnitIndex = 0;
            while (intCurrentSize >= 1024 && pintUnitIndex < astrUnits.Length - 1)
            {
                intCurrentSize /= 1024;
                pintUnitIndex++;
            }

            if (!pblnIncludeDecimal)
            {
                intCurrentSize = System.Math.Round(intCurrentSize, System.MidpointRounding.AwayFromZero);
            }

            return System.String.Format("{0:0.##} {1}", intCurrentSize, astrUnits[pintUnitIndex]);
        }
    }
}
