using System;

namespace CollectionApp.WEB.Utils
{
    public static class DateUtil
    {
        public static string ConvertDate(DateTime? date)
        {
            if (date != null)
            {
                return String.Format("{0:yyyy-MM-dd}", date);
            }
            return "";
        }
    }
}
