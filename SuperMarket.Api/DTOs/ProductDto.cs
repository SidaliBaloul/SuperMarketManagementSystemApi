using System.ComponentModel.DataAnnotations;

namespace SuperMarketManagementSystemApi.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "BarCode Is Required"), Length(13, 13, ErrorMessage = "Barcode Length Must Be 13 Characters")]
        public string BarCode { get; set; }

        [Required(ErrorMessage = "Product Name Is Required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage = "Product Price Is Required"), Range(0.01, double.MaxValue, ErrorMessage = "PricePerUnit Must Be Greater Then 0")]
        public decimal Price { get; set; }
    }
}
