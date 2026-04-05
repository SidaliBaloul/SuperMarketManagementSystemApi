using SuperMarket.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperMarket.Business.Interfaces
{
    public interface ICartService
    {
        Task<List<Cart>> GetCartsAsync();
        Task AddProductToCartAsync(Cart cart);
        Task UpdateCartAsync(Cart cart);
        Task DeleteCartAsync(int cartId);
        Task<Cart> GetCartByIdAsync(int cartId);
        Task<Cart> GetCartByProductId(int productId);
        Task AddRemoveOneExistingProductUnitToCart(int productId, int value = 1);
        Task ClearCart();
        Task<decimal> GetCartTotalAmount();
    }
}
