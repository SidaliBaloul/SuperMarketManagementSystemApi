using Microsoft.EntityFrameworkCore;
using SuperMarket.Data.DBContexte;
using SuperMarket.Domain.Entities;
using SuperMarket.Data.Interfaces;

namespace SuperMarket.Data.Repositories
{
    public class SaleDetailsRepository : ISaleDetailsRepository
    {
        private readonly SuperMarketDbContext _Context;

        public SaleDetailsRepository(SuperMarketDbContext context)
        {
            _Context = context;
        }

        public async Task<List<SaleDetail>> GetSaleDetailsAsync()
        {
            return await _Context.SaleDetails.ToListAsync();
        }

        public async Task AddSaleDetailsRecordAsync(SaleDetail record)
        {
            await _Context.SaleDetails.AddAsync(record);
            await _Context.SaveChangesAsync();
        }
    }
}
