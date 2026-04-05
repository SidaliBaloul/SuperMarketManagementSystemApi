using SuperMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Interfaces
{
    public interface IStockService
    {
        Task<List<Stock>> GetStockProductsAsync();
        Task AddStockAsync(Stock stock);
        Task<Stock> GetStockById(int stockId);
        Task DeleteStockAsync(int stockId);
        Task UpdateStockAsync(Stock stock);
        Task<Stock> GetStockByProductId(int productId);
        Task UpdateStockQuantity(int productId, int usedquantity);
        Task<List<Stock>> GetStocksByProduct(int productId);
    }
}
