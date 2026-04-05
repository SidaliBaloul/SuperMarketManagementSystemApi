using SuperMarket.Domain.Entities;

namespace SuperMarket.Data.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetStockProductsAsync();
        Task AddStockAsync(Stock stock);
        Task<Stock> GetStockById(int stockId);
        Task DeleteStockAsync(Stock stock);
        Task UpdateStockAsync(Stock stock);
        Task<Stock> GetStockByProductId(int  productId);
        Task UpdateStockQuantity(Stock stock, int usedquantity);
        Task<List<Stock>> GetStocksByProduct(int productId);
    }
}
