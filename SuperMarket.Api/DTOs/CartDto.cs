using System.ComponentModel.DataAnnotations;

namespace SuperMarketManagementSystemApi.DTOs
{
    public class CartDto
    {
        public int No { get; set; }

        [Required(ErrorMessage = "Id Is Required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity Is Required"), Range(1, 10000000000000000000, ErrorMessage = "Quantity must be Atleast 1 unit ")]
        public int Quantity { get; set; }

        [Required(ErrorMessage ="Total is Required"), Range(0, 10000000000000000000, ErrorMessage = "Total Must Be Positive ")]
        public decimal Total { get; set; }
    }
}
