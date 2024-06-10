namespace FrontEnd_EventManagement.Utility
{
    public class ApiType
    {
        public static string EventManagementAPI { get; set; }
        public static string AuthAPIBase { get; set; }

        public const string TokenCookie = "JWTToken";
        public enum ApiTypes
        {
            GET,
            POST
        }
    }
}
