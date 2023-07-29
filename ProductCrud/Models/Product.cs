using MongoDB.Bson;

namespace ProductCrud.Models
{
    public class Product
    {
        public ObjectId Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string ProductCategory { get; set; }
        public string ProductDescription { get; set; }
        public DateTime? created { get; set; }
    }
}
