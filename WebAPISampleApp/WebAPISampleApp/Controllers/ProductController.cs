using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPISampleApp.Model;
using WebAPISampleApp.Service;
using static WebAPISampleApp.CommonMethod.Common;

namespace WebAPISampleApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductRepository _repository;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public IActionResult GetAll([FromQuery] ProductFilterOption productFilterOption, ProducSorting sorting)
        {
            return Ok(_repository.GetAll(productFilterOption, sorting));
        }

        [HttpGet("{id}")]
        public IActionResult GetById(string id)
        {
            try
            {
                return Ok(_repository.GetById(id));
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpPost]
        public IActionResult Create(ProductModel model)
        {
            var data = _repository.Add(model);

            return Ok( new { Success= true, Data = data });
        }

        [HttpPut("{id}")]
        public IActionResult Edit(string id, ProductModel editProduct)
        {
            try
            {
                var isSuccess = _repository.Update(id, editProduct);

                if (!isSuccess)
                {
                    return NotFound();
                }

                return StatusCode(StatusCodes.Status204NoContent);
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
                var isSuccess = _repository.Remove(id);

                if (!isSuccess)
                {
                    return NotFound();
                }

                return StatusCode(StatusCodes.Status204NoContent);
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}
