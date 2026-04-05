using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace SuperMarketManagementSystemApi.DTOs
{
    public class PurchaseDto
    {
        public int PurchaseId { get; set; }

        [Required(ErrorMessage ="ProductId Is Required")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Quantity Is Required"), Range(1,1000000000000000,ErrorMessage ="Quantity Must Be atleast 1 unit! ")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "PricePerUnit Is Required"), Range(0.01, double.MaxValue, ErrorMessage ="PricePerUnit Must Be Greater Then 0")]
        public decimal PricePerUnit { get; set; }

        [Required(ErrorMessage = "Total Is Required"), Range(0.01, double.MaxValue, ErrorMessage = "Total Must Be Greater Then 0")]
        public decimal Total { get; set; }

        [Required(ErrorMessage = "SupplierId Is Required")]
        public int SupplierId { get; set; }

        [Required(ErrorMessage = "Purchase Date Is Required")]
        public DateOnly PurchaseDate { get; set; }

        [Required(ErrorMessage = "StockedIn Is Required"), Range(0,1, ErrorMessage ="StockedIn 0 or 1")]
        public bool StockedIn { get; set; }

        [Required(ErrorMessage = "Expiration Date Is Required")]
        public DateOnly ExpDate { get; set; }
    }
}
