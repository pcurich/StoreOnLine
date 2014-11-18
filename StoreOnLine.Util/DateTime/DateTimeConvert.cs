namespace StoreOnLine.Util.DateTime
{
    public class DateTimeConvert
    {
        public static long GetDateTimeNow()
        {
            var year = System.DateTime.Now.Year * 10000000000;
            var month = System.DateTime.Now.Month * 100000000;
            var day = System.DateTime.Now.Day * 1000000;
            var hour = System.DateTime.Now.Hour * 10000;
            var minute = System.DateTime.Now.Minute * 100;
            var second = System.DateTime.Now.Second;

            return year + month + day + hour + minute + second;
        }

        public static long GetDateTimeUpdate(System.DateTime date)
        {
            var year = date.Year * 10000000000;
            var month = date.Month * 100000000;
            var day = date.Day * 1000000;
            var hour = date.Hour * 10000;
            var minute = date.Minute * 100;
            var second = date.Second;

            return year + month + day + hour + minute + second;
        }
    }
}
