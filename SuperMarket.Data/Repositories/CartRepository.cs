using Microsoft.EntityFrameworkCore;
using SuperMarket.Data.DBContexte;
using SuperMarket.Domain.Entities;
using SuperMarket.Data.Interfaces;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace SuperMarket.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly SuperMarketDbContext _Context;

        public CartRepository(SuperMarketDbContext context)
        {
            _Context = context;
        }

        public async Task<List<Cart>> GetCartsAsync()
        {
            return await _Context.Carts.ToListAsync();
        }

        public async Task AddCartAsync(Cart cart)
        {
            await _Context.Carts.AddAsync(cart);
            await _Context.SaveChangesAsync();
        }

        public async Task UpdateCartAsync(Cart cart)
        {
             _Context.Carts.Update(cart);
            await _Context.SaveChangesAsync();
        }

        public async Task DeleteCartAsync(Cart cart)
        {
            _Context.Carts.Remove(cart);
            await _Context.SaveChangesAsync();
        }

        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
            return await _Context.Carts.FirstOrDefaultAsync(c => c.No == cartId);
        }

        public async Task<Cart> GetCartByProductId(int productId)
        {
            return await _Context.Carts.FirstOrDefaultAsync(c => c.ProductId == productId);
        }

        public async Task ClearCart()
        {
            var records = await _Context.Carts.ToListAsync();
            _Context.Carts.RemoveRange(records);

            await _Context.SaveChangesAsync();
        }

        public async Task<decimal> GetCartTotalAmount()
        {
            return await _Context.Carts.SumAsync(c => c.Total);
        }
    }
}
