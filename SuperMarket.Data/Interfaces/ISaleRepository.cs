using SuperMarket.Domain.Entities;

namespace SuperMarket.Data.Interfaces
{
    public interface ISaleRepository
    {
        Task<List<Sale>> GetSalesAsync();
        Task AddSaleAsync(Sale sale);
        Task<int> AddSaleAsyncWuthReturningId(Sale sale);
        Task<List<Sale>> GetSalesFromToDateTime(DateTime from, DateTime to);
        Task<Sale> GetSaleById(int id);
    }
}
