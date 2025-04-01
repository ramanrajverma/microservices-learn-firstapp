using System.ComponentModel.DataAnnotations;

namespace Microservices.Services.CouponAPI.Models
{
    public class Coupon
    {
        [Key]
        public int CouponId { get; set; }
        [Required]
        public string CouponCode { get; set; }
        [Required]
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
        public bool Status { get; set; }

        //public DateTime lastUpdated { get; set; }

        //public int MyProperty2 { get; set; }
    }
}
