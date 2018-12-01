using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ShoppingApi.Model.Dto;
using ShoppingApi.Repository.Interface;
using ShoppingApi.Utils;


namespace ShoppingApi.Controllers
{
    [Route("api/ShoppingList")]
    [ApiController]
    public class ShoppingListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/ShoppingList
        [HttpGet]
        public IActionResult Get()
        {
            var shoppingLists = _unitOfWork.ShoppingLists.GetShoppingListsWithItems();
            if (shoppingLists == null || shoppingLists.Count <= 0)
                return NotFound();
            List<ShoppingListDto> listsDtos = new List<ShoppingListDto>();
            foreach (var list in shoppingLists)
            {
                var dto = DomainToDtoMapper.MapShoppingList(list);
                listsDtos.Add(dto);
            }
            return Ok(shoppingLists.Select(DomainToDtoMapper.MapShoppingList));
        }

        // GET: api/ShoppingLists/5
        [HttpGet("{id}", Name = "GetShoppingList")]
        public IActionResult Get(int id)
        {
            var shoppingList = _unitOfWork.ShoppingLists.GetShoppingListWithItems(id);
            if (shoppingList == null)
                return NotFound();
            return Ok(DomainToDtoMapper.MapShoppingList(shoppingList));

        }

        // POST: api/ShoppingList
        [HttpPost]
        public IActionResult Post([FromBody] ShoppingListDto shoppingList)
        {
            if (ModelState.IsValid)
            {
                var domainList = DtoToDomainMapper.MapShoppingList(shoppingList);
                _unitOfWork.ShoppingLists.Create(domainList);
                _unitOfWork.Complete();
                return Ok(new { id = domainList.ListId });
            }

            return BadRequest();
        }

        // PUT: api/ShoppingList/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]ShoppingListDto shoppingList)
        {
            if (ModelState.IsValid)
            {
                var existingList = _unitOfWork.ShoppingLists.Get(id);
                if (existingList == null)
                    return NotFound();
                var domainList = DtoToDomainMapper.MapShoppingList(shoppingList);
                _unitOfWork.ShoppingLists.Update(domainList);
                _unitOfWork.Complete();
                return NoContent();
            }

            return BadRequest();
        }

        // DELETE: api/ShoppingList/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existingList = _unitOfWork.ShoppingLists.Get(id);
            if (existingList == null)
                return NotFound();
            _unitOfWork.ShoppingLists.Delete(existingList);
            _unitOfWork.Complete();
            return NoContent();
        }
    }
}
