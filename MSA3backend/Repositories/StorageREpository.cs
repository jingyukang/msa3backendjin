using MSA3backend.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MSA3backend.Repositories
{
    public interface IStorageRepository
    {
        Task<List<Item>> GetStorageAsync();
        Task<Item> GetItemByIdAsync(string id);
        Task<Item> CreateItemAsync(Item item);
        Task<Item> UpdateItemAsync(UpdateItem item);
        Task DeleteItemAsync(string id);
    }
    public class StorageRepository : IStorageRepository
    {
        private readonly IMongoCollection<Item> _items;

        public StorageRepository(IMarketDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _items = database.GetCollection<Item>(settings.StorageCollectionName);
        }
        public async Task<List<Item>> GetStorageAsync()
        {
            return await _items.Find(i => true).ToListAsync();
        }
        public async Task<Item> GetItemByIdAsync(string id)
        {
            return await _items.Find<Item>(i => i.Id == id).FirstOrDefaultAsync();
        }
        public async Task<Item> CreateItemAsync(Item item)
        {
            await _items.InsertOneAsync(item);
            return await GetItemByIdAsync(item.Id);
        }
        public async Task<Item> UpdateItemAsync(UpdateItem item)
        {
            Item updateItem = await _items.Find<Item>(i => i.Id == item.Id).FirstOrDefaultAsync();
            if (item.ItemName != null)
            {
                updateItem.ItemName = item.ItemName;
            }
            if (item.ItemDescription != null)
            {
                updateItem.ItemDescription = item.ItemDescription;
            }
            if (item.Price != null && item.Price >= 0)
            {
                updateItem.Price = (int)item.Price;
            }
            if (item.Quantity != null && item.Quantity >= 0)
            {
                updateItem.Quantity = (int)item.Quantity;
            }
            await _items.ReplaceOneAsync(i => i.Id == updateItem.Id, updateItem);
            return await GetItemByIdAsync(item.Id);
        }
        public async Task DeleteItemAsync(string id)
        {
            await _items.DeleteOneAsync(i => i.Id == id);
            //return await _items.Find(i => true).ToListAsync();
        }
    }
}

