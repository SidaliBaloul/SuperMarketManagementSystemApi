using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Business.Interfaces;
using SuperMarket.Business.Services;
using SuperMarket.Domain.Entities;
using SuperMarketManagementSystemApi.DTOs;

namespace SuperMarketManagementSystemApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _Product;
        public ProductController(IProductService product)
        {
            _Product = product;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetProducts()
        {
            List<Product> products = await _Product.GetProductsAsync();

            if (products.Count == 0)
                return NotFound("No Products Available! ");

            var result = products.Select(p => new ProductDto { BarCode = p.BarCode, ProductName = p.Name, Id = p.ProductId, Price = p.Price });

            return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult> AddNewProduct(ProductDto newproduct)
        {
            Product product = new Product
            {
                BarCode = newproduct.BarCode,
                Name = newproduct.ProductName,
                Price = newproduct.Price
            };

            try
            {
                await _Product.AddProductAsync(product);

                return CreatedAtAction(nameof(GetProducts), new { id = product.ProductId });
            }
            catch(Exception ex)
            {
                return BadRequest(new { msg = ex.Message });
            }

        }

        [Authorize(Roles ="Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductDto updatedproduct)
        {
            Product product = await _Product.GetProductByIdAsync(id);

            if (product == null)
                return NotFound($"Product With ID : {id} Not Found! ");

            product.BarCode = updatedproduct.BarCode;
            product.Name = updatedproduct.ProductName;
            product.Price = updatedproduct.Price;

            await _Product.UpdateProductAsync(product);

            return Ok("Product Has Been Updated Succesfully! ");
        }

        [Authorize(Roles ="Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            try
            {
                await _Product.DeleteProductAsync(id);

                return Ok("Product Deleted Succesfully! ");
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDto>> GetProducById(int id)
        {
            Product product = await _Product.GetProductByIdAsync(id);

            if (product == null)
                return NotFound($"Product With ID : {id} Not Found! ");

            ProductDto productDto = new ProductDto
            {
                BarCode = product.BarCode,
                ProductName = product.Name,
                Price = product.Price
            };

            return Ok(productDto);
        }

        [HttpGet("barcode/{barcode}")]
        public async Task<ActionResult<ProductDto>> GetProductByBarCode(string barcode)
        {
            Product product = await _Product.GetProductByBarCode(barcode);

            if (product == null)
                return NotFound($"Product With BarCode : {barcode} Not Found! ");

            ProductDto productDto = new ProductDto
            {
                Id = product.ProductId,
                BarCode = product.BarCode,
                ProductName = product.Name,
                Price = product.Price
            };

            return Ok(productDto);
        }

        [HttpGet("name/{name}")]
        public async Task<ActionResult<ProductDto>> GetProductsByName([FromQuery] string name)
        {
            List<Product> products = await _Product.GetProductsByName(name);

            if(products.Count == 0)
                return NotFound($"Product(s) With Name : {name} Not Found! ");

            List<ProductDto> dtos = products.Select(p => new ProductDto { Id = p.ProductId, BarCode = p.BarCode, ProductName = p.Name, Price = p.Price }).ToList();

            return Ok(dtos);
        }

    }
}
