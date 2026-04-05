using System.ComponentModel.DataAnnotations;

namespace SuperMarketManagementSystemApi.DTOs
{
    public class AddCartDto
    {
        [Required(ErrorMessage = "Id Is Required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity Is Required"), Range(1, 10000000000000000000, ErrorMessage = "Quantity must be Atleast 1 unit ")]
        public int Quantity { get; set; }

    }
}
