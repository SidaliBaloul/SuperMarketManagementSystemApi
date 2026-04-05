using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Business.Interfaces;
using SuperMarket.Domain.Entities;
using SuperMarketManagementSystemApi.DTOs;

namespace SuperMarketManagementSystemApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _Cart;
        public CartController(ICartService cart)
        {
            _Cart = cart;
        }

        [HttpGet]
        public async Task<ActionResult> GetCartList()
        {
            List<Cart> carts = await _Cart.GetCartsAsync();

            if (carts.Count == 0)
                return Ok("Cart Is Empty");

            var result = carts.Select(c => new CartDto { No = c.No, ProductId = c.ProductId , Quantity = c.Quantity, Total = c.Total});

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> AddProductToCart(AddCartDto newcart)
        {
            Cart cart = new Cart
            {
                ProductId = newcart.ProductId,
                Quantity = newcart.Quantity,
                Total = 0
            };

            try
            {
                await _Cart.AddProductToCartAsync(cart);

                return CreatedAtAction(nameof(GetCartList), new { No = cart.No });
            }
            catch(Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
            
        }

        [HttpPut("product/{productId:int}/increase")]
        public async Task<ActionResult> AddOneExistingProductUnitToCart(int productId)
        {
            try
            {
                await _Cart.AddRemoveOneExistingProductUnitToCart(productId);

                return Ok("Unit Added Succesfully! ");
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpPut("product/{productId:int}/decrease")]
        public async Task<ActionResult> RemoveOneExistingProductUnitToCart(int productId)
        {
            try
            {
                await _Cart.AddRemoveOneExistingProductUnitToCart(productId, -1);

                return Ok("Unit Removed Succesfully! ");
            }
            catch (Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateCart(int id, UpdateCartDto updatedcart)
        {
            Cart cart = await _Cart.GetCartByIdAsync(id);

            if (cart == null)
                return NotFound($"Cart with ID : {id} Not Found!");


            cart.Quantity = updatedcart.Quantity;

            try
            {
                await _Cart.UpdateCartAsync(cart);
                return Ok("Cart Has Been Updated Succesfully! ");
            }
            catch(Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }
  
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteInCart(int id)
        {
            try
            {
                await _Cart.DeleteCartAsync(id);

                return Ok("Cart Deleted Succesfully! ");

            }catch(KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartDto>> GetCartById(int id)
        {
            Cart cart = await _Cart.GetCartByIdAsync(id);

            if (cart == null)
                return NotFound($"Cart with ID : {id} Not Found! ");

            CartDto dto = new CartDto
            {
                No = cart.No,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
                Total = cart.Total
            };

            return Ok(dto);
        }

        [HttpDelete]
        public async Task<ActionResult> ClearCart()
        {
            await _Cart.ClearCart();
            return Ok("Cart Cleared ");
        }

        [HttpGet("total")]
        public async Task<ActionResult<Decimal>> GetCartTotalAmount()
        {
            decimal TotalAmount = await _Cart.GetCartTotalAmount();

            return Ok(TotalAmount);
        }

        [HttpGet("Product/{id}")]
        public async Task<ActionResult<Cart>> IsProductExistsInCart(int id)
        {
            Cart cart = await _Cart.GetCartByProductId(id);

            if (cart == null)
                return NotFound($"Product With ID : {id} Has No Cart! ");

            CartDto dto = new CartDto
            {
                No = cart.No,
                ProductId = cart.ProductId,
                Quantity = cart.Quantity,
                Total = cart.Total
            };

            return Ok(dto);
        }
    }
}
