using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentTest.Data;
using StudentTest.Models;

namespace StudentTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
       
        private readonly AppDbContext _context;
        
        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("GetProductList")]
        public async Task<IActionResult> GetProductList()
        {
            var productsList = _context.products.ToList();
            return productsList.Count > 0 ? Ok(productsList) : NotFound();
        }


        [HttpPost]
        [Route("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product productData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(productData);
                await _context.SaveChangesAsync();
            }
            return Ok(productData);
        }
        [HttpDelete]
        [Route("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct(int? id)
        {
            if (ModelState.IsValid)
            {
                if (id == 0 || id < 0)
                {
                    return NotFound();
                }
                else
                {
                    // find student
                    var product = await _context.products.FindAsync(id);
                    if (product == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        _context.products.Remove(product);
                        await _context.SaveChangesAsync();
                        return Ok("Product Deleted Successfully");
                    }
                }
            }
            return BadRequest("Internal Server Error");
        }


        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(int id, [FromBody] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    return Ok(string.Concat("Product with ProductID id", product.Id, " has been updated"));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.products.Any(e => e.Id == product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return BadRequest("Internal Server Error");
        }
    }
}
