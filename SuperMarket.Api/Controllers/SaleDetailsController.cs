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
    public class SaleDetailsController : ControllerBase
    {
        private readonly ISaleDetailService _SaleDetails;

        public SaleDetailsController(ISaleDetailService saleDetails)
        {
            _SaleDetails = saleDetails;
        }

        [HttpGet("GetSalesDetails")]
        public async Task<ActionResult<IEnumerable<SaleDetailsDto>>> GetSaleDetailsList()
        {
            List<SaleDetail> sales = await _SaleDetails.GetSaleDetailsAsync();

            if(sales.Count == 0)
                return NotFound("No Sales! ");

            var dto = sales.Select(s => new SaleDetailsDto { SaleDetailId = s.SaleDetailId, ProductId = s.ProductId, SaleId = s.SaleId, Quantity= s.Quantity });

            return Ok(dto);
        }
    }
}
