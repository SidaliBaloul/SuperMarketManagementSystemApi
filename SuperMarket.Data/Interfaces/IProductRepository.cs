using SuperMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsAsync();
        Task AddProductAsync(Product product);
        Task UpdateProductAsync(Product product);
        Task DeleteProductAsync(Product product);
        Task<Product> GetProductByIdAsync(int productId);
        Task<Product> GetProductByBarCode(string barcode);
        Task<bool> IsProductAlreadyExists(string barcode);
        Task<List<Product>> GetProductsByName(string name);

    }
}
