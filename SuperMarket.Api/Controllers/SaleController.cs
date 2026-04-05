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
    public class SaleController : ControllerBase
    {
        private readonly ISaleService _Sale;

        public SaleController(ISaleService sale)
        {
            _Sale = sale;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSales()
        {
            List<Sale> sales = await _Sale.GetSalesAsync();

            if (sales.Count == 0)
                return NotFound("No Sales! ");

            var dto = sales.Select(s => new SaleDto { SaleId = s.SaleId, UserId = s.UserId, Amount = s.Amount, Date = s.Date });

            return Ok(dto);
        }

        [HttpPost]
        public async Task<ActionResult> AddSale()
        {
            int userid = 2;

            if (!await _Sale.AddSaleAsync(userid))
                return NotFound($"User With ID : {userid} Not Found!");


            return Ok("SaleCreated Succesfully");
        }

        [HttpGet("report")]
        public async Task<ActionResult<IEnumerable<SaleDto>>> GetSalesRecords([FromQuery] DateTime from, [FromQuery] DateTime to)
        {
            List<Sale> sales = await _Sale.GetSalesFromToDateTime(from, to);

            if (sales.Count == 0)
                return NotFound("No Sales In Between These Dates! ");

            List<SaleDto> dtos = sales.Select(s => new SaleDto { SaleId = s.SaleId, Amount = s.Amount, UserId = s.UserId, Date = s.Date }).ToList();

            return Ok(dtos);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult> GetSaleById(int id)
        {
            Sale sale = await _Sale.GetSaleById(id);

            if(sale == null)
                return NotFound($"Sale With ID : {id} Not Found!");

            SaleDto dto = new SaleDto
            {
                SaleId = sale.SaleId,
                Amount = sale.Amount,
                UserId = sale.UserId,
                Date = sale.Date
            };

            return Ok(dto);

        }
    }
}
