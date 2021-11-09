using EfCorePostgreSql.Context;
using EfCorePostgreSql.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EfCorePostgreSql.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly PostgreDbContext _context;

        public ProductsController(PostgreDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok(await _context.Products.AsNoTracking().ToListAsync());
        }



        [HttpGet]
        [Route("{productId:int}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            return Ok(await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == productId));
        }


        [HttpPost]
        public async Task<IActionResult> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return Ok();
        }


        [HttpDelete]
        [Route("{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var product = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == productId);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
