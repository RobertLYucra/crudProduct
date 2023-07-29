using ProductCrud.Contracts.Params;
using ProductCrud.Models;

namespace ProductCrud.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> InsertProduct(ProductParams product);
        Task<Product> UpdateProduct(ProductParams product, int id);
        Task<bool> DeleteProduct(int productId);
        Task<List<Product>> GetAllProdcut();
        Task<Product> GetProductById(int productId);
    }
}
