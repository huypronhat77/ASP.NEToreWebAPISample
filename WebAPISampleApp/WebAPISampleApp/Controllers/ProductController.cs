using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPISampleApp.Model;

namespace WebAPISampleApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static List<Product> Products = new List<Product>();

        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(Products);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                var seletedProdut = Products.SingleOrDefault(p => p.Id.Equals(Guid.Parse(id)));

                if (seletedProdut is null)
                {
                    return NotFound();
                }

                return Ok(seletedProdut);
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult Create(ProductVM productVM)
        {
            Product product = new Product()
            {
                Id = Guid.NewGuid(),
                Name = productVM.Name,
                Price = productVM.Price
            };
            Products.Add(product);

            return Ok( new { Success= true, Data = product });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, ProductVM editProduct)
        {
            try
            {
                var seletedProdut = Products.SingleOrDefault(p => p.Id.Equals(Guid.Parse(id)));

                if (seletedProdut is null)
                {
                    return NotFound();
                }


                seletedProdut.Name = editProduct.Name;
                seletedProdut.Price = editProduct.Price;

                return Ok(new { Success = true, Data = seletedProdut});
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(string id)
        {
            try
            {
                var seletedProdut = Products.SingleOrDefault(p => p.Id.Equals(Guid.Parse(id)));

                if (seletedProdut is null)
                {
                    return NotFound();
                }

                Products.Remove(seletedProdut);

                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
