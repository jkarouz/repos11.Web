namespace repos11.Web.Utilities.Security
{
    public class ConstansAuthentication
    {
        public static string DeafultCookie
        {

            get
            {
                var assembly = typeof(MvcApplication).Assembly.GetName();
                return $"{assembly.Name.Replace(" ", "_").Replace(".", "_").ToLower()}_type";
            }
        }

        public static string DeafultCookieName
        {

            get
            {
                var assembly = typeof(MvcApplication).Assembly.GetName();
                return $"{assembly.Name.Replace(" ", "_").Replace(".", "_").ToLower()}_{assembly.Version.ToString().Replace(".", ",").ToLower()}";
            }
        }

        public static string DeafultProvider
        {

            get
            {
                var assembly = typeof(MvcApplication).Assembly.GetName();
                return $"{assembly.Name.Replace(" ", "_").Replace(".", "_").ToLower()}_provider";
            }
        }
    }
}