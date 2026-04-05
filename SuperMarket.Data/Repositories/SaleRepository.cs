using Microsoft.EntityFrameworkCore;
using SuperMarket.Data.DBContexte;
using SuperMarket.Domain.Entities;
using SuperMarket.Data.Interfaces;

namespace SuperMarket.Data.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly SuperMarketDbContext _Context;

        public SaleRepository(SuperMarketDbContext context)
        {
            _Context = context;
        }

        public async Task<List<Sale>> GetSalesAsync()
        {
            return await _Context.Sales.ToListAsync();
        }

        public async Task AddSaleAsync(Sale sale)
        {
            await _Context.Sales.AddAsync(sale);

            await _Context.SaveChangesAsync();
        }

        public async Task<int> AddSaleAsyncWuthReturningId(Sale sale)
        {
            await _Context.Sales.AddAsync(sale);

            await _Context.SaveChangesAsync();

            return sale.SaleId;
        }

        public async Task<List<Sale>> GetSalesFromToDateTime(DateTime from, DateTime to)
        {
            return await _Context.Sales.Where(s => s.Date >= from && s.Date <= to).ToListAsync();  
        }

        public async Task<Sale> GetSaleById(int id)
        {
            return await _Context.Sales.FirstOrDefaultAsync(s => s.SaleId == id);
        }
    }
}
