using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Model.Dto;
using ShoppingApi.Repository.Interface;
using ShoppingApi.Utils;

namespace ShoppingApi.Controllers
{
    [Route("api/ItemCategory")]
    [ApiController]
    public class ItemCategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ItemCategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/ItemCategory
        [HttpGet]
        public IActionResult Get()
        {
            var itemCategories = _unitOfWork.ItemCategories.Get();
            if (itemCategories == null || itemCategories.Count <= 0)
                return NotFound();
            return Ok(itemCategories.Select(DomainToDtoMapper.MapItemCategory));
        }

        // GET: api/ItemCategory/5
        [HttpGet("{id}", Name = "GetItemCategory")]
        public IActionResult Get(int id)
        {
            var itemCategories = _unitOfWork.ItemCategories.Get(id);
            if (itemCategories == null)
                return NotFound();
            return Ok(DomainToDtoMapper.MapItemCategory(itemCategories));
        }

        // POST: api/ItemCategory
        [HttpPost]
        public IActionResult Post([FromBody] ItemCategoryDto itemCategory)
        {
            if (ModelState.IsValid)
            {
                var domainCategory = DtoToDomainMapper.MapItemCategory(itemCategory);
                _unitOfWork.ItemCategories.Create(domainCategory);
                _unitOfWork.Complete();
                return Ok(new { id = domainCategory.CategoryId });
            }

            return BadRequest();
        }

        // PUT: api/ItemCategory/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ItemCategoryDto itemCategory)
        {
            if(ModelState.IsValid)
            {
                var existingCategory = _unitOfWork.ShoppingLists.Get(id);
                if (existingCategory == null)
                    return NotFound();
                var domainCategory = DtoToDomainMapper.MapItemCategory(itemCategory);
                _unitOfWork.ItemCategories.Update(domainCategory);
                _unitOfWork.Complete();
                return NoContent();
            }

            return BadRequest();
        }

        // DELETE: api/ItemCategory/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingCategory = _unitOfWork.ItemCategories.Get(id);
            if (existingCategory == null)
                return NotFound();
            _unitOfWork.ItemCategories.Delete(existingCategory);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}
