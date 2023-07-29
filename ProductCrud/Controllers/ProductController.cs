using Microsoft.AspNetCore.Mvc;
using ProductCrud.Contracts.Params;
using ProductCrud.Repository.Interfaces;

namespace ProductCrud.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProdcut();
            return Ok(new
            {
                success = true,
                message = "Todos los productos",
                result = products
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById( int id)
        {
            try
            {
                var product = await _productRepository.GetProductById(id);
                if (product != null)
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Product Encontrado",
                        result = product
                    });
                }
                return NotFound(new
                {
                    success = false,
                    message = "Producto no encontrado",
                    result = ""
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductParams product)
        {
           var insert = await _productRepository.InsertProduct(product);
            if (insert)
            {
                return Ok(new
                {
                    success = true,
                    messagge = "Product creado",
                    results = product
                }); ;
            }
            return BadRequest(new
            {
                success = false,
                messagge = "Error al crear producto..",
                Results = ""
            });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(ProductParams product, [FromRoute]int id)
        {
            var updatedProduct = await _productRepository.UpdateProduct(product,id);
            if (updatedProduct!=null)
            {
                return Ok(new
                {
                    success = true,
                    message = "Producto actualizado",
                    result = updatedProduct
                });
            }
            return BadRequest(new
            {
                success = false,
                message = "Error al actualizar...",
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {

            var elimitated = await _productRepository.DeleteProduct(id);

            if (elimitated)
            {
                return Ok(new
                {
                    success = true,
                    message = "Producto eliminado",
                    result = ""
                });
            }
            return BadRequest(new
            {
                success = false,
                message = "Error al eliminar..",
            });
        }
    }
}
