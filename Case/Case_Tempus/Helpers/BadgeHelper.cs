namespace CaseTempus.Helpers
{
    public class BadgeHelper
    {
        public static string Execute(decimal value)
        {
            if (value <= 980)
                return "bg-red";

            if (value > 980 && value <= 2500)
                return "bg-yellow";

            return "bg-green";
        }
    }
}
