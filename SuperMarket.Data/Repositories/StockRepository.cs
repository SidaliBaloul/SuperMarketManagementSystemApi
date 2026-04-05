using Microsoft.EntityFrameworkCore;
using SuperMarket.Data.DBContexte;
using SuperMarket.Domain.Entities;
using SuperMarket.Data.Interfaces;

namespace SuperMarket.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly SuperMarketDbContext _Context;

        public StockRepository(SuperMarketDbContext context)
        {
            _Context = context;
        }

        public async Task<List<Stock>> GetStockProductsAsync()
        {
            return await _Context.Stocks.ToListAsync();
        }

        public async Task AddStockAsync(Stock stock)
        {
            await _Context.Stocks.AddAsync(stock);
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteStockAsync(Stock stock)
        {
            _Context.Stocks.Remove(stock);
            await _Context.SaveChangesAsync();
        }

        public async Task UpdateStockAsync(Stock stock)
        {
            _Context.Stocks.Update(stock);
            await _Context.SaveChangesAsync();
        }

        public async Task<Stock> GetStockById(int stockId)
        {
            return await _Context.Stocks.FirstOrDefaultAsync(s => s.StockId == stockId);
        }

        public async Task<Stock> GetStockByProductId(int productId)
        {
            return await _Context.Stocks.OrderBy(s => s.ExpDate).FirstOrDefaultAsync(s => s.ProductId == productId);
        }

        public async Task UpdateStockQuantity(Stock stock, int usedquantity)
        {
            stock.QuantityAvailable -= usedquantity;
            await _Context.SaveChangesAsync();
        }

        public async Task<List<Stock>> GetStocksByProduct(int productId)
        {
            return await _Context.Stocks.Where(s => s.ProductId == productId).ToListAsync();
        }
    }
}
