using MSA3backend.Models;
using MSA3backend.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MSA3backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : Controller
    {

        private readonly IStorageRepository _storageRepository;


        public StorageController(IStorageRepository storageRepository)
        {
            _storageRepository = storageRepository;
        }


        /// <summary>
        /// Storeage Reading action
        /// </summary>
        /// <returns>Data</returns>
        [HttpGet]
        public async Task<ActionResult<List<Item>>> GetStorage()
        {
            return await _storageRepository.GetStorageAsync();
        }



        /// <summary>
        /// Storeage Reading action byId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Item>> GetItemById(string id)
        {
            return await _storageRepository.GetItemByIdAsync(id);
        }



        /// <summary>
        /// Storage Posting action
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem(CreateItem item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            Item aItem = new Item();
            aItem.ItemName = item.ItemName;
            aItem.ItemDescription = item.ItemDescription;
            aItem.Price = item.Price;
            aItem.Quantity = item.Quantity;

            return await _storageRepository.CreateItemAsync(aItem);
        }



        /// <summary>
        /// Storage Updating action
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<ActionResult<Item>> UpdateItem(UpdateItem item)
        {
            return await _storageRepository.UpdateItemAsync(item);
        }



        /// <summary>
        /// Storage a delete action
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteItem(string id)
        {
            await _storageRepository.DeleteItemAsync(id);
            return NoContent();
        }
    }
}
