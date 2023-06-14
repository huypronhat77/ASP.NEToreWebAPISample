using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPISampleApp.Data;
using WebAPISampleApp.Model;

namespace WebAPISampleApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MyDbContext _context;

        public CategoryController(MyDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var Categories = _context.categories.ToList();

            return Ok(Categories);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var category = _context.categories.SingleOrDefault(cat => cat.CatId.Equals(id));

            if (category is null)
            {
                return NotFound();
            }

            return Ok(category);
        }

        [HttpPost]
        public IActionResult Create(CategoryModel model)
        {
            try
            {
                var newCategory = new Category()
                {
                    Name = model.Name
                };

                _context.Add(newCategory);
                _context.SaveChanges();

                return Ok(newCategory);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, CategoryModel model)
        {
            var selectedCategory = _context.categories.SingleOrDefault(cat => cat.CatId.Equals(id));

            if (selectedCategory is null)
            {
                return NotFound();
            }

            selectedCategory.Name = model.Name;
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var selectedCategory = _context.categories.SingleOrDefault(cat => cat.CatId.Equals(id));

            if (selectedCategory is null)
            {
                return NotFound();
            }

            _context.categories.Remove(selectedCategory);
            _context.SaveChanges();

            return NoContent();
        }



    }
}
