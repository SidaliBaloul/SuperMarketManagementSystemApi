using Microsoft.EntityFrameworkCore;
using SuperMarket.Data.DBContexte;
using SuperMarket.Domain.Entities;
using SuperMarket.Data.Interfaces;

namespace SuperMarket.Data.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly SuperMarketDbContext _Context;

        public PurchaseRepository(SuperMarketDbContext context)
        {
            _Context = context;
        }

        public async Task AddPurchaseAsync(Purchase purchase)
        {
            await _Context.Purchases.AddAsync(purchase);
            await _Context.SaveChangesAsync();
        }

        public async Task<List<Purchase>> GetAllPurchasesAsync()
        {
            return await _Context.Purchases.ToListAsync();
        }

        public async Task UpdatePurchase(Purchase purchase)
        {
            _Context.Purchases.Update(purchase);

            await _Context.SaveChangesAsync();
        }

        public async Task<Purchase> GetPurchaseByIdAsync(int purchaseId)
        {
            return await _Context.Purchases.FirstOrDefaultAsync(p => p.PurchaseId == purchaseId);
        }

        public async Task StockInPurchase(int id)
        {
            var purchase = await _Context.Purchases.FindAsync(id);

            if(purchase != null)
            {
                purchase.StokedIn = true;
                await _Context.SaveChangesAsync();
            }
        }

        public async Task<List<Purchase>> GetPurchasesBySupplier(int supplierId)
        {
            return await _Context.Purchases.Where(p => p.SupplierId == supplierId).ToListAsync();
        }
    }
}
