using SuperMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Interfaces
{
    public interface ISupplierService
    {
        Task<List<Supplier>> GetSuppliersAsync();
        Task AddSupplierAsync(Supplier supplier);
        Task<Supplier> GetSupplierById(int supplierId);
        Task DeleteSupplierAsync(int supplierId);
        Task UpdateSupplierAsync(Supplier supplier);
    }
}
