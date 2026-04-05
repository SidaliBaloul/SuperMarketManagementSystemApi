using System.ComponentModel.DataAnnotations;

namespace SuperMarketManagementSystemApi.DTOs
{
    public class AddSupplierDto
    {
        [Required(ErrorMessage ="Supplier Name Is Required"), MinLength(1, ErrorMessage = "Name Must Contain Atleast One Character! ")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Supplier Phone Number Is Required")]
        public decimal Phone { get; set; }

        [Required(ErrorMessage = "Supplier Email Is Required")]
        public string Email { get; set; }
    }
}
