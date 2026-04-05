using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using SuperMarket.Data.DBContexte;
using SuperMarket.Domain.Entities;
using SuperMarket.Data.Interfaces;

namespace SuperMarket.Data.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly SuperMarketDbContext _Context;

        public SupplierRepository(SuperMarketDbContext context)
        {
            _Context = context;
        }

        public async Task<List<Supplier>> GetSuppliersAsync()
        {
            return await _Context.Suppliers.ToListAsync();
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            await _Context.Suppliers.AddAsync(supplier);
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteSupplierAsync(Supplier supplier)
        {
            _Context.Suppliers.Remove(supplier);
            await _Context.SaveChangesAsync();
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            _Context.Suppliers.Update(supplier);
            await _Context.SaveChangesAsync();
        }

        public async Task<Supplier> GetSupplierById(int supplierId)
        {
            return await _Context.Suppliers.FirstOrDefaultAsync(s => s.SupplierId == supplierId);
        }
    }
}
