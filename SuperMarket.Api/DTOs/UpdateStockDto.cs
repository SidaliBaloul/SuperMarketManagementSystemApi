using System.ComponentModel.DataAnnotations;

namespace SuperMarketManagementSystemApi.DTOs
{
    public class UpdateStockDto
    {
        [Required(ErrorMessage = "Quantity Is Required"), Range(1,double.MaxValue, ErrorMessage = "Quantity Should be Greater Than 0")]
        public int QuantityAvailable { get; set; }
    }
}
