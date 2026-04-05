using SuperMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Interfaces
{
    public interface IPurchaseService
    {
        Task AddPurchaseAsync(Purchase purchase);
        Task<List<Purchase>> GetAllPurchasesAsync();
        Task UpdatePurchaseAsync(Purchase purchase);
        Task<Purchase> GetPurchaseByIdAsync(int purchaseId);
        Task StockInPurchase(int id);
        Task<List<Purchase>> GetPurchasesBySupplier(int supplierId);
    }
}
