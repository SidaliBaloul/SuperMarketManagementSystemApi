using System.ComponentModel.DataAnnotations;

namespace SuperMarketManagementSystemApi.DTOs
{
    public class SupplierDto
    {
        public int SupplierId { get; set; }

        public string Name { get; set; }

        public decimal Phone { get; set; }

        public string? Email { get; set; }
    }
}
