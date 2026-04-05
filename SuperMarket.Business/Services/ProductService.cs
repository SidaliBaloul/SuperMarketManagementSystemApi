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
    public class ProductService : IProductService
    {
        private readonly IProductRepository _Repository;

        public ProductService(IProductRepository repository)
        {
            _Repository = repository;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _Repository.GetProductsAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            bool IsProductAlreadyExists = await _Repository.IsProductAlreadyExists(product.BarCode);

            if (IsProductAlreadyExists)
                throw new Exception($"Product With BarCode : {product.BarCode} Already Exists! Try Another.");

            await _Repository.AddProductAsync(product);
        }

        public async Task UpdateProductAsync(Product product)
        {
            await _Repository.UpdateProductAsync(product);
        }

        public async Task<Product> GetProductByIdAsync(int productid)
        {
            return await _Repository.GetProductByIdAsync(productid);
        }

        public async Task DeleteProductAsync(int productId)
        {
            Product product = await _Repository.GetProductByIdAsync(productId);

            if (product == null)
                throw new KeyNotFoundException($"Product With ID : {productId} Not Found! ");

            await _Repository.DeleteProductAsync(product);
        }

        public async Task<Product> GetProductByBarCode(string barcode)
        {
            return await _Repository.GetProductByBarCode(barcode);
        }

        public async Task<List<Product>> GetProductsByName(string name)
        {
            return await _Repository.GetProductsByName(name);
        }

    }
}
