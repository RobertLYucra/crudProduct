using MongoDB.Bson;
using MongoDB.Driver;
using ProductCrud.Contracts.Params;
using ProductCrud.Models;
using ProductCrud.Repository.Interfaces;
using ProductCrud.Resource;

namespace ProductCrud.Repository
{
    public class ProductRepository : IProductRepository
    {
        internal MongoConections mongoDB = new MongoConections();
        private IMongoCollection<Product> mongoCollection;

        public ProductRepository()
        {
            mongoCollection = mongoDB.database.GetCollection<Product>("Products");
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            var product = Builders<Product>.Filter.Eq(x => x.ProductId, productId);
            await mongoCollection.DeleteOneAsync(product);
            return true;
        }

        public async Task<List<Product>> GetAllProdcut()
        {
            return await mongoCollection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }

        public async Task<Product> GetProductById(int productId)
        {
            var product = Builders<Product>.Filter.Eq(x => x.ProductId, productId);
            return await mongoCollection.Find(product).FirstOrDefaultAsync();
        }

        public async Task<bool> InsertProduct(ProductParams upload)
        {
            try
            {
                Product product = new Product();
                var list = mongoCollection.FindAsync(new BsonDocument()).Result.ToListAsync();
                product.ProductId = list.Result.Count() + 1;
                product.ProductName = upload.ProductName;
                product.ProductDescription = upload.ProductDescription;
                product.Price = upload.Price;
                product.Stock = upload.Stock;
                product.ProductCategory = upload.ProductCategory;
                product.created = DateTime.UtcNow;

                await mongoCollection.InsertOneAsync(product);

                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Product> UpdateProduct(ProductParams product, int id)
        {
            try
            {
                var filter = Builders<Product>.Filter.Eq(x => x.ProductId, id);
                var productFound = await mongoCollection.Find(filter).FirstOrDefaultAsync();
                if (productFound != null)
                {
                    productFound.ProductName = product.ProductName;
                    productFound.Price = product.Price;
                    productFound.Stock = product.Stock;
                    productFound.ProductDescription = product.ProductDescription;
                    productFound.ProductCategory = product.ProductCategory;
                    var result = await mongoCollection.ReplaceOneAsync(filter, productFound);
                    if (result.IsAcknowledged && result.ModifiedCount > 0)
                    {
                        return productFound;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
