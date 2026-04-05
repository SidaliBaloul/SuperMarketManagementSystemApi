using SuperMarket.Domain.Entities;

namespace SuperMarket.Data.Interfaces
{
    public interface ISupplierRepository
    {
        Task<List<Supplier>> GetSuppliersAsync();
        Task AddSupplierAsync(Supplier supplier);
        Task<Supplier> GetSupplierById(int supplierId);
        Task DeleteSupplierAsync(Supplier supplier);
        Task UpdateSupplierAsync(Supplier supplier);
    }
}
