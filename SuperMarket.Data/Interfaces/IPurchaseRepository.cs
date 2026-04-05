using SuperMarket.Domain.Entities;

namespace SuperMarket.Data.Interfaces
{
    public interface IPurchaseRepository
    {
        Task AddPurchaseAsync(Purchase purchase);
        Task<List<Purchase>> GetAllPurchasesAsync();
        Task UpdatePurchase(Purchase purchase);
        Task<Purchase> GetPurchaseByIdAsync(int purchaseId);
        Task StockInPurchase(int id);
        Task<List<Purchase>> GetPurchasesBySupplier(int supplierId);
    }
}
