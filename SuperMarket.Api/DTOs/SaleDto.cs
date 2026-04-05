namespace SuperMarketManagementSystemApi.DTOs
{
    public class SaleDto
    {
        public int SaleId { get; set; }
        public decimal Amount { get; set; }
        public int UserId { get; set; }
        public DateTime Date {  get; set; }
    }
}
