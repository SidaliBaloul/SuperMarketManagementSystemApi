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
    public class SaleDetailService : ISaleDetailService
    {
        private readonly ISaleDetailsRepository _Repository;

        public SaleDetailService(ISaleDetailsRepository repository)
        {
            _Repository = repository;
        }

        public async Task<List<SaleDetail>> GetSaleDetailsAsync()
        {
            return await _Repository.GetSaleDetailsAsync();
        }

        public async Task AddSaleDetailsRecordAsync(SaleDetail record)
        {
            await _Repository.AddSaleDetailsRecordAsync(record);
        }
    }
}
