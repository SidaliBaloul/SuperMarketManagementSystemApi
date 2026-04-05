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
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _Repository;
        private readonly IProductService _Product;
        private readonly ISupplierService _Supplier;
        private readonly IStockService _Stock;

        public PurchaseService(IPurchaseRepository repository, IStockService stock, IProductService product, ISupplierService supplier)
        {
            _Repository = repository;
            _Stock = stock;
            _Product = product;
            _Supplier = supplier;
        }

        public async Task AddPurchaseAsync(Purchase purchase)
        {
            Product product = await _Product.GetProductByIdAsync(purchase.ProductId);

            if (product == null)
                throw new Exception($"Product With ID : {purchase.ProductId} Not Found! ");

            Supplier supplier = await _Supplier.GetSupplierById(purchase.SupplierId);
            if (supplier == null)
                throw new Exception($"Supplier With ID : {purchase.SupplierId} Not Found! ");

            if (purchase.ExpDate <= DateOnly.FromDateTime(DateTime.Now))
                throw new Exception("Cannot Enter A Passed Date! ");

            purchase.PricePerUnit = product.Price;
            purchase.Total = purchase.Quantity * purchase.PricePerUnit;

            await _Repository.AddPurchaseAsync(purchase);
        }

        public async Task<List<Purchase>> GetAllPurchasesAsync()
        {
            return await _Repository.GetAllPurchasesAsync();
        }

        public async Task UpdatePurchaseAsync(Purchase purchase)
        {
            await _Repository.UpdatePurchase(purchase);
        }

        public async Task<Purchase> GetPurchaseByIdAsync(int purchaseId)
        {
            return await _Repository.GetPurchaseByIdAsync(purchaseId);
        }

        public async Task StockInPurchase(int id)
        {
            Purchase purchase = await _Repository.GetPurchaseByIdAsync(id);

            if (purchase == null)
                throw new Exception($"Purchase With ID : {id} Not Found! ");

            if(purchase.StokedIn)
                throw new Exception($"Purchase With ID : {id} Already StockedIn! ");


            await _Stock.AddStockAsync(new Stock { ExpDate = purchase.ExpDate, ProductId = purchase.ProductId, QuantityAvailable = purchase.Quantity });
            await _Repository.StockInPurchase(purchase.PurchaseId);

        }

        public async Task<List<Purchase>> GetPurchasesBySupplier(int supplierId)
        {
            Supplier supplier = await _Supplier.GetSupplierById(supplierId);

            if(supplier == null)
                throw new Exception($"Supplier With ID : {supplierId} Not Found! ");

            return await _Repository.GetPurchasesBySupplier(supplierId);
        }
    }
}
