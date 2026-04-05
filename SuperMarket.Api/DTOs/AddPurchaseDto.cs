using System.ComponentModel.DataAnnotations;

namespace SuperMarketManagementSystemApi.DTOs
{
    public class AddPurchaseDto
    {
        [Required(ErrorMessage = "ProductId Is Required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity Is Required"), Range(1, 1000000000000000, ErrorMessage = "Quantity Must Be atleast 1 unit! ")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "SupplierId Is Required")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Purchase Date Is Required")]
        public DateTime PurchaseDate { get; set; }

        [Required(ErrorMessage = "Expiration Date Is Required")]
        public DateTime ExpDate { get; set; }
    }
}
