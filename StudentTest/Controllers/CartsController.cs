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
    public class CartsController : Controller
    {
        private readonly AppDbContext _context;

        public CartsController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("GetCartList")]
        public async Task<IActionResult> GetCartList()
        {
            var cartList = _context.carts.ToList();
            return cartList.Count > 0 ? Ok(cartList) : NotFound();
        }

        [HttpPost]
        [Route("CreateCart")]
        public async Task<IActionResult> CreateCart([FromBody] Cart cartData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cartData);
                await _context.SaveChangesAsync();
            }
            return Ok(cartData);
        }

        [HttpDelete]
        [Route("DeleteCart")]
        public async Task<IActionResult> DeleteCart(int? id)
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
                    var cart = await _context.carts.FindAsync(id);
                    if (cart == null)
                    {
                        return NotFound();
                    }
                    else
                    {
                        _context.carts.Remove(cart);
                        await _context.SaveChangesAsync();
                        return Ok("Cart Deleted Successfully");
                    }
                }
            }
            return BadRequest("Internal Server Error");
        }

        [HttpPut]
        [Route("UpdateCart")]
        public async Task<IActionResult> UpdateCart(int id, [FromBody] Cart cart)
        {
            if (id != cart.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cart);
                    await _context.SaveChangesAsync();
                    return Ok(string.Concat("Cart with CartID id ", cart.Id, " has been updated"));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.carts.Any(e => e.Id == cart.Id))
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
