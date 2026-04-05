namespace SuperMarketManagementSystemApi.DTOs
{
    public class StockDto
    {
        public int StockId { get; set; }

        public int ProductId { get; set; }

        public int QuantityAvailable { get; set; }

        public DateOnly ExpDate { get; set; }
    }
}
