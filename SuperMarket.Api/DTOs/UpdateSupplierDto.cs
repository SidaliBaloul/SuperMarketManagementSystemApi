using System.ComponentModel.DataAnnotations;

namespace SuperMarketManagementSystemApi.DTOs
{
    public class UpdateSupplierDto
    {
        [Required(ErrorMessage = "Supplier Name Is Required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Supplier Phone Number Is Required")]
        public decimal Phone { get; set; }

        [Required(ErrorMessage = "Supplier Email Is Required")]
        public string Email { get; set; }
    }
}
