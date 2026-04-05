using SuperMarket.Domain.Entities;

namespace SuperMarket.Data.Interfaces
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetCartsAsync();
        Task AddCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(Cart cart);
        Task<Cart> GetCartByIdAsync(int cartId);
        Task<Cart> GetCartByProductId(int productId);
        Task ClearCart();
        Task<decimal> GetCartTotalAmount();
    }
}
