using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
    public class CartService : ICartService
    {
        private readonly ICartRepository _Repository;
        private readonly IProductService _ProductService;
        private readonly IStockService _StockService;

        public CartService(ICartRepository repository, IProductService productService, IStockService stockservice)
        {
            _Repository = repository;
            _ProductService = productService;
            _StockService = stockservice;
        }

        public async Task<List<Cart>> GetCartsAsync()
        {
            return await _Repository.GetCartsAsync();
        }

        public async Task<Cart> GetCartByProductId(int productId)
        {
            return await _Repository.GetCartByProductId(productId);
        }

        public async Task<bool> IsProductAlreadyExistsInCart(int productId)
        {
            if (await _Repository.GetCartByProductId(productId) == null)
                return false;

            return true;
        }

        public async Task AddProductToCartAsync(Cart cart)
        {
            Product product = await _ProductService.GetProductByIdAsync(cart.ProductId);

            if (product == null)
                throw new Exception($"Product With ID : {cart.ProductId} NOT FOUND! ");

            Stock stock = await _StockService.GetStockByProductId(product.ProductId);

            if (stock == null)
                throw new Exception($"Product With ID : {cart.ProductId} Has No Stock! ");

            Cart cart2 = await GetCartByProductId(product.ProductId);
            int quantityvailable = stock.QuantityAvailable;

            if(cart2 != null)
                quantityvailable -= cart2.Quantity;
            

            if (cart.Quantity > quantityvailable)
                throw new Exception($"This Quantity Is Not Available, Quantity availabl Is : {quantityvailable}");

            cart.Total = cart.Quantity * product.Price;

            if (cart2 == null)
                await _Repository.AddCartAsync(cart);
            else
            {
                cart.Quantity += cart2.Quantity;
                cart.Total += cart2.Total;
                await _Repository.UpdateCartAsync(cart);
            }    

        }

        public async Task AddRemoveOneExistingProductUnitToCart(int productId, int value = 1)
        {
            Product product = await _ProductService.GetProductByIdAsync(productId);

            if (product == null)
                throw new Exception($"Product With ID : {productId} NOT FOUND! ");

            Cart cart = await GetCartByProductId(productId);

            if(cart == null)
                throw new Exception($"Product With ID : {productId} Is Not In Cart! ");

            Stock stock = await _StockService.GetStockByProductId(product.ProductId);

            if (stock.QuantityAvailable == 0 && value == 1)
                throw new Exception($"Product With ID : {cart.ProductId} Is Out Of Stock! ");

            if(cart.Quantity >= stock.QuantityAvailable && value == 1)
                throw new Exception($"Product With ID : {cart.ProductId} Is At Maximum Units Available! ");

            if (cart.Quantity <= 1 && value == -1)
                throw new Exception("You Can't Go Below One Unit!");

            cart.Quantity += value;
            cart.Total = (cart.Quantity * product.Price);

            await _Repository.UpdateCartAsync(cart);
        }

        public async Task UpdateCartAsync(Cart cart)
        {
            Product product = await _ProductService.GetProductByIdAsync(cart.ProductId);

            if (product == null)
                throw new Exception($"Product With ID : {cart.ProductId} NOT FOUND! ");

            Stock stock = await _StockService.GetStockByProductId(product.ProductId);


            if (cart.Quantity >= stock.QuantityAvailable)
                throw new Exception($"Product With ID : {cart.ProductId} Is At Maximum Units Available! ");

            if (cart.Quantity <= 1)
                throw new Exception("You Can't Go Below One Unit!");

            cart.Total = cart.Quantity * product.Price;

            await _Repository.UpdateCartAsync(cart);
        }

        public async Task DeleteCartAsync(int cartId)
        {
            Cart cart = await _Repository.GetCartByIdAsync(cartId);

            if (cart == null)
                throw new KeyNotFoundException($"Cart With ID : {cartId} Not Found! ");

            await _Repository.DeleteCartAsync(cart);
        }

        public async Task<Cart> GetCartByIdAsync(int cartId)
        {
            return await _Repository.GetCartByIdAsync(cartId);
        }

        public async Task ClearCart()
        {
            await _Repository.ClearCart();
        }

        public async Task<decimal> GetCartTotalAmount()
        {
            return await _Repository.GetCartTotalAmount();
        }
    }
}
