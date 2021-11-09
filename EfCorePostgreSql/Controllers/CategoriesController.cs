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
    public class CategoriesController : ControllerBase
    {
        private readonly PostgreDbContext _context;

        public CategoriesController(PostgreDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            return Ok(await _context.Categories.AsNoTracking().ToListAsync());
        }


        [HttpGet]
        [Route("{categoryId:int}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            return Ok(await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == categoryId));
        }


        [HttpPost]
        public async Task<IActionResult> AddCategory(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return Ok();
        }


        [HttpDelete]
        [Route("{categoryId:int}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.AsNoTracking().FirstOrDefaultAsync(x => x.Id == categoryId);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
