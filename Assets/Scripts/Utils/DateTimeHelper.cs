namespace ORCAS.Utils
{
    public static class DateTimeHelper
    {
        public static bool IsBetweenHours(System.DateTime dayTime, int startHour, int endHour)
        {
            if (startHour < endHour)
            {
                return dayTime.Hour >= startHour && dayTime.Hour < endHour;
            }
            else
            {
                return dayTime.Hour >= endHour && dayTime.Hour < startHour;
            }
        }
    }
}