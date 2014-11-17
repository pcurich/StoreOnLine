namespace StoreOnLine.Util.DateTime
{
    public class DateTimeConvert
    {
        public static string GetDateTimeNow()
        {
            return System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        public static string GetDateTimeUpdate(System.DateTime date)
        {
            return date.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
