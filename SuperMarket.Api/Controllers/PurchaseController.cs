using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SuperMarket.Business.Interfaces;
using SuperMarket.Domain.Entities;
using SuperMarketManagementSystemApi.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SuperMarketManagementSystemApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        private readonly IPurchaseService _Purchase;

        public PurchaseController(IPurchaseService purchase)
        {
            _Purchase = purchase;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetPurchases()
        {
            List<Purchase> purchases = await _Purchase.GetAllPurchasesAsync();

            if (purchases.Count == 0)
                return NotFound("No Purchases");

            var dto = purchases.Select(p => new PurchaseDto
            {
                PurchaseId = p.PurchaseId,
                ProductId = p.ProductId,
                PricePerUnit = p.PricePerUnit,
                Quantity = p.Quantity,
                ExpDate = p.ExpDate,
                PurchaseDate = p.PurchaseDate,
                StockedIn = p.StokedIn,
                SupplierId = p.SupplierId,
                Total = p.Total
            });

            return Ok(dto);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<ActionResult> AddNewPurchase([FromBody] AddPurchaseDto newPurchase)
        {
            Purchase purchase = new Purchase
            {
                ProductId = newPurchase.ProductId,
                Quantity = newPurchase.Quantity,
                SupplierId = newPurchase.SupplierId,
                PurchaseDate = DateOnly.FromDateTime(newPurchase.PurchaseDate),
                StokedIn = false,
                ExpDate = DateOnly.FromDateTime(newPurchase.ExpDate)
            };

            try
            {
                await _Purchase.AddPurchaseAsync(purchase);

                return CreatedAtAction(nameof(GetPurchases), new { PurchaseId = purchase.PurchaseId });
            }
            catch(Exception ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}/stockIn")]
        public async Task<ActionResult> StockInPurchase(int id)
        {
            try
            {
                await _Purchase.StockInPurchase(id);

                return Ok("Purchase StockedIn Succesfully! ");
            }
            catch(Exception ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PurchaseDto>> GetPurchaseById(int id)
        {
            Purchase Purchase = await _Purchase.GetPurchaseByIdAsync(id);

            if (Purchase == null)
                return NotFound($"Purchase With ID : {id} Not Found! ");

            PurchaseDto dto = new PurchaseDto
            {
                PurchaseId = id,
                ProductId = Purchase.ProductId,
                Quantity = Purchase.Quantity,
                PricePerUnit = Purchase.PricePerUnit,
                Total = Purchase.Total,
                SupplierId = Purchase.SupplierId,
                PurchaseDate = Purchase.PurchaseDate,
                ExpDate = Purchase.ExpDate,
                StockedIn = Purchase.StokedIn 
            };

            return Ok(dto);
        }

        [HttpGet("supplier/{supplierId:int}")]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetPurchasesBySupplier(int supplierId)
        {
            try
            {
                List<Purchase> purchases = await _Purchase.GetPurchasesBySupplier(supplierId);

                if (purchases == null)
                    return NotFound($"No Purchases Found With This Supplier! ");

                List<PurchaseDto> dtos = purchases.Select(p => new PurchaseDto
                {
                    PurchaseId = p.PurchaseId,
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    PricePerUnit = p.PricePerUnit,
                    Total = p.Total,
                    SupplierId = p.SupplierId,
                    PurchaseDate = p.PurchaseDate,
                    StockedIn = p.StokedIn,
                    ExpDate = p.ExpDate
                }).ToList();

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return NotFound(new { msg = ex.Message });
            }

        }
    }
}
