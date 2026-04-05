using Microsoft.EntityFrameworkCore;
using SuperMarket.Data.DBContexte;
using SuperMarket.Domain.Entities;
using SuperMarket.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SuperMarketDbContext _Context;

        public ProductRepository(SuperMarketDbContext context)
        {
            _Context = context;
        }

        public async Task<List<Product>> GetProductsAsync()
        {
            return await _Context.Products.ToListAsync();
        }

        public async Task AddProductAsync(Product product)
        {
            await _Context.Products.AddAsync(product);

            await _Context.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
             _Context.Products.Update(product);
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
           _Context.Products.Remove(product);

            await _Context.SaveChangesAsync();
        }

        public async Task<Product> GetProductByIdAsync(int productId)
        {
            return await _Context.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
        }

        public async Task<Product> GetProductByBarCode(string barcode)
        {
            return await _Context.Products.FirstOrDefaultAsync(p => p.BarCode == barcode);
        }

        public async Task<bool> IsProductAlreadyExists(string barcode)
        {
            return await _Context.Products.AnyAsync(p => p.BarCode == barcode);
        }

        public async Task<List<Product>> GetProductsByName(string name)
        {
            return await _Context.Products.Where(p => p.Name.Contains(name)).ToListAsync();
        }
    }
}
