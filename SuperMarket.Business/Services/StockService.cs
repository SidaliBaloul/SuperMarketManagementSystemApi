using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
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
    public class StockService : IStockService
    {
        private readonly IStockRepository _Repository;
        private IProductService _Product;

        public StockService(IStockRepository repository, IProductService product)
        {
            _Repository = repository;
            _Product = product;
        }

        public async Task<List<Stock>> GetStockProductsAsync()
        {
            return await _Repository.GetStockProductsAsync();
        }

        public async Task AddStockAsync(Stock stock)
        {
            await _Repository.AddStockAsync(stock);
        }

        public async Task<Stock> GetStockById(int stockId)
        {
            return await _Repository.GetStockById(stockId);
        }

        public async Task DeleteStockAsync(int stockId)
        {
            Stock stock = await _Repository.GetStockById(stockId);

            if (stock == null)
                throw new KeyNotFoundException($"Stock With ID : {stockId} Not Found! ");

            await _Repository.DeleteStockAsync(stock);
        }

        public async Task UpdateStockAsync(Stock stock)
        {
            await _Repository.UpdateStockAsync(stock);
        }

        public async Task<Stock> GetStockByProductId(int productid)
        {
            return await _Repository.GetStockById(productid);
        }

        public async Task UpdateStockQuantity(int productId, int usedquantity)
        {
            var stock = await _Repository.GetStockById(productId);

            await _Repository.UpdateStockQuantity(stock, usedquantity);
        }

        public async Task<List<Stock>> GetStocksByProduct(int productId)
        {
            Product product = await _Product.GetProductByIdAsync(productId);

            if (product == null) throw new KeyNotFoundException($"Product Id : {productId} Not Found! ");

            return await _Repository.GetStocksByProduct(productId);
        }
    }

}
