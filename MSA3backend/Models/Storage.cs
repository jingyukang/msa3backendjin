using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MSA3backend.Models
{
    public class Item
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }

    public class CreateItem
    {
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }

    public class UpdateItem
    {
        public string Id { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public Nullable<int> Price { get; set; }
        public Nullable<int> Quantity { get; set; }
    }
}
