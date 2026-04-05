using SuperMarket.Domain.Entities;

namespace SuperMarket.Data.Interfaces
{
    public interface ISaleDetailsRepository
    {
        Task<List<SaleDetail>> GetSaleDetailsAsync();
        Task AddSaleDetailsRecordAsync(SaleDetail record);
    }
}
