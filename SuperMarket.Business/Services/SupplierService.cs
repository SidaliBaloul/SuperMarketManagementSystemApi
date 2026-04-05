using SuperMarket.Business.Interfaces;
using SuperMarket.Domain.Entities;
using SuperMarket.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Services
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _Repository;

        public SupplierService(ISupplierRepository repository)
        {
            _Repository = repository;
        }

        public async Task<List<Supplier>> GetSuppliersAsync()
        {
            return await _Repository.GetSuppliersAsync();
        }

        public async Task AddSupplierAsync(Supplier supplier)
        {
            await _Repository.AddSupplierAsync(supplier);
        }

        public async Task DeleteSupplierAsync(int supplierId)
        {
            Supplier supplier = await _Repository.GetSupplierById(supplierId);

            if (supplier == null)
                throw new KeyNotFoundException($"Supplier With ID : {supplierId} Not Found! ");

            await _Repository.DeleteSupplierAsync(supplier);
        }

        public async Task<Supplier> GetSupplierById(int supplierId)
        {
            return await _Repository.GetSupplierById(supplierId);
        }

        public async Task UpdateSupplierAsync(Supplier supplier)
        {
            await _Repository.UpdateSupplierAsync(supplier);
        }
    }
}
