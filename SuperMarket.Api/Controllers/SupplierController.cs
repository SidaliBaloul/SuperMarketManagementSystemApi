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
    public class SupplierController : ControllerBase
    {
        private readonly ISupplierService _Supplier;

        public SupplierController(ISupplierService supplier)
        {
            _Supplier = supplier;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SupplierDto>>> GetSuppliers()
        {
            List<Supplier> suppliers = await _Supplier.GetSuppliersAsync();

            if(suppliers.Count == 0)
                return NotFound("No Supplier Found! ");

            var dto = suppliers.Select(s => new SupplierDto { SupplierId = s.SupplierId, Email = s.Email, Name = s.Name, Phone = s.Phone });

            return Ok(dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult> AddNewSupplier(AddSupplierDto newsupplier)
        {
            Supplier supplier = new Supplier
            {
                Name = newsupplier.Name,
                Phone = newsupplier.Phone,
                Email = newsupplier.Email
            };

            await _Supplier.AddSupplierAsync(supplier);
            return CreatedAtAction(nameof(GetSuppliers), new { id = supplier.SupplierId });
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<SupplierDto>> GetSupplierById(int id)
        {
            Supplier supplier = await _Supplier.GetSupplierById(id);

            if (supplier == null)
                return NotFound($"Supplier With ID : {id} NOT FOUND! ");

            SupplierDto dto = new SupplierDto
            {
                SupplierId = supplier.SupplierId,
                Email = supplier.Email,
                Name = supplier.Name,
                Phone = supplier.Phone
            };

            return Ok(dto);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> DeleteSupplier(int id)
        {
            try
            {
                await _Supplier.DeleteSupplierAsync(id);

                return Ok("Supplier Has Been Deleted Succesfully! ");
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new { msg = ex.Message });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id:int}")]
        public async Task<ActionResult> UpdateSupplier(int id, UpdateSupplierDto updatedsupplier)
        {
            Supplier supplier = await _Supplier.GetSupplierById(id);

            if (supplier == null)
                return NotFound($"Supplier With ID : {id} NOT FOUND! ");

            supplier.Name = updatedsupplier.Name;
            supplier.Email = updatedsupplier.Email;
            supplier.Phone = updatedsupplier.Phone;

            await _Supplier.UpdateSupplierAsync(supplier);

            return Ok("Supplier Is Updated Succesfully! ");
        }
    }
}
