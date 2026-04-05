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
    public class StockController : ControllerBase
    {
        private readonly IStockService _Stock;

        public StockController(IStockService stock)
        {
            _Stock = stock;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StockDto>>> GetStock()
        {
            List<Stock> stock = await _Stock.GetStockProductsAsync();

            if (stock.Count == 0)
                return NotFound("Stock Is Empty! ");

            var dto = stock.Select(s => new StockDto { StockId = s.StockId, ExpDate = s.ExpDate, ProductId = s.ProductId, QuantityAvailable = s.QuantityAvailable });

            return Ok(dto);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<StockDto>> GetStockbyId(int id)
        {
            Stock stock = await _Stock.GetStockById(id);

            if (stock == null)
                return NotFound($"Stock With ID {id} NOT FOUND! ");

            StockDto dto = new StockDto
            {
                StockId = stock.StockId,
                ExpDate = stock.ExpDate,
                ProductId = stock.ProductId,
                QuantityAvailable = stock.QuantityAvailable
            };

            return Ok(dto);
        }


        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteStock(int id)
        {
            try
            {
                await _Stock.DeleteStockAsync(id);

                return Ok("Stock deleted Successfully! ");
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateStock(int id, UpdateStockDto updatestock)
        {
            Stock stock = await _Stock.GetStockById(id);

            if(stock == null)
                return NotFound($"Stock With ID {id} NOT FOUND! ");

            stock.QuantityAvailable = updatestock.QuantityAvailable;

            await _Stock.UpdateStockAsync(stock);

            return Ok("Stock Updated Succesfully! ");
        }

        [HttpGet("product/{id:int}")]
        public async Task<ActionResult<IEnumerable<StockDto>>> GetStockbyProductId(int id)
        {
            try
            {
                List<Stock> stocks = await _Stock.GetStocksByProduct(id);

                if (stocks.Count == 0)
                    return NotFound($"Product With ID : {id} Is Not Available In Stock! ");

                List<StockDto> dtos = stocks.Select(s => new StockDto
                {
                    StockId = s.StockId,
                    ExpDate = s.ExpDate,
                    ProductId = s.ProductId,
                    QuantityAvailable = s.QuantityAvailable

                }).ToList();

                return Ok(dtos);
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }
    }
}
