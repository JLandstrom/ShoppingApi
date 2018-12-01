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
    [Route("api/ShoppingItem")]
    [ApiController]
    public class ShoppingItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/ShoppingItem
        [HttpGet]
        public IActionResult Get()
        {
            var shoppingItems = _unitOfWork.ShoppingItems.Get();
            if (shoppingItems == null || shoppingItems.Count <= 0)
                return NotFound();
            return Ok(shoppingItems.Select(DomainToDtoMapper.MapShoppingItem));
        }

        // GET: api/ShoppingItem/5
        [HttpGet("{id}", Name = "GetShoppingItem")]
        public IActionResult Get(int id)
        {
            var shoppingItem = _unitOfWork.ShoppingItems.Get(id);
            if (shoppingItem == null)
                return NotFound();
            return Ok(DomainToDtoMapper.MapShoppingItem(shoppingItem));
        }

        // POST: api/ShoppingItem
        [HttpPost]
        public IActionResult Post([FromBody] ShoppingItemDto shoppingItem)
        {
            if (ModelState.IsValid)
            {
                var domainItem = DtoToDomainMapper.MapShoppingItem(shoppingItem);
                _unitOfWork.ShoppingItems.Create(domainItem);
                _unitOfWork.Complete();
                return Ok(new { id = domainItem.ItemId });
            }

            return BadRequest();
        }

        // PUT: api/ShoppingItem/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ShoppingItemDto shoppingItem)
        {
            if (ModelState.IsValid)
            {
                var existingItem = _unitOfWork.ShoppingItems.Get(id);
                if (existingItem == null)
                    return NotFound();
                var domainItem = DtoToDomainMapper.MapShoppingItem(shoppingItem);
                _unitOfWork.ShoppingItems.Update(domainItem);
                _unitOfWork.Complete();
                return NoContent();
            }

            return BadRequest();
        }

        // DELETE: api/ShoppingItem/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingCategory = _unitOfWork.ShoppingItems.Get(id);
            if (existingCategory == null)
                return NotFound();
            _unitOfWork.ShoppingItems.Delete(existingCategory);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}
