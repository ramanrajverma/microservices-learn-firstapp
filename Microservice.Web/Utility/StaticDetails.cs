namespace Microservice.Web.Utility
{
    public class StaticDetails
    {
        public static string CouponApiBase { get; set; } // = "https://localhost:5001/api/v1/coupon"
        public static string AuthApiBase { get; set; }
        public enum ApiType
        { 
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
