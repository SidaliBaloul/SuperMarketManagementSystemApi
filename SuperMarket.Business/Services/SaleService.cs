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
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _Repository;
        private readonly IUserService _User;
        private readonly ICartService _Cart;
        private readonly ISaleDetailService _SaleDetail;
        private readonly IStockService _Stock;


        public SaleService(ISaleRepository repository, IUserService user, ICartService cart, ISaleDetailService saledetail, IStockService stock)
        {
            _Repository = repository;
            _User = user;
            _Cart = cart;
            _SaleDetail = saledetail;
            _Stock = stock;
        }

        public async Task<List<Sale>> GetSalesAsync()
        {
            return await _Repository.GetSalesAsync();
        }

        public async Task<int> AddSaleAsyncWuthReturningId(Sale sale)
        {
            return await _Repository.AddSaleAsyncWuthReturningId(sale);
        }

        public async Task<bool> AddSaleAsync(int userId)
        {
            Userr user = await _User.GetUserById(userId);

            if (user == null)
                return false;

            decimal CartTotalAmout = await _Cart.GetCartTotalAmount();

            Sale sale = new Sale
            {
                UserId = user.UserId,
                Date = DateTime.Now,
                Amount = CartTotalAmout
            };

            int saleId = await _Repository.AddSaleAsyncWuthReturningId(sale);

            List<Cart> cartlist = await _Cart.GetCartsAsync();

            foreach(Cart cart in cartlist)
            {
                await _SaleDetail.AddSaleDetailsRecordAsync(new SaleDetail { SaleId = saleId, ProductId = cart.ProductId, Quantity = cart.Quantity });
                await _Stock.UpdateStockQuantity(cart.ProductId, cart.Quantity);
            }

            await _Cart.ClearCart();

            return true;
        }
 
        public async Task<List<Sale>> GetSalesFromToDateTime(DateTime from, DateTime to)
        {
            return await _Repository.GetSalesFromToDateTime(from,to);
        }

        public async Task<Sale> GetSaleById(int id)
        {
            return await _Repository.GetSaleById(id);
        }
    }
}
