using SuperMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Interfaces
{
    public interface ISaleService
    {
        Task<List<Sale>> GetSalesAsync();
        Task<bool> AddSaleAsync(int userId);
        Task<int> AddSaleAsyncWuthReturningId(Sale sale);
        Task<List<Sale>> GetSalesFromToDateTime(DateTime from, DateTime to);
        Task<Sale> GetSaleById(int id);
    }
}
