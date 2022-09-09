namespace MSA3backend.Models
{
    public interface IMarketDatabaseSettings 
    {
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
        string StorageCollectionName { get; set; }
    }
    public class MarketDatabaseSettings : IMarketDatabaseSettings
    {
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
        public string StorageCollectionName { get; set; }
    }
}
