using SuperMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Interfaces
{
    public interface ISaleDetailService
    {
        Task<List<SaleDetail>> GetSaleDetailsAsync();
        Task AddSaleDetailsRecordAsync(SaleDetail record);
    }

}
