using Core.Dtos.Requests;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Create(Guid companyId, CreateProductDto product)
        {
            bool isSaved;

            isSaved = await _productService.AddProduct(companyId, product);

            if (isSaved)
            {
                return Ok();
            }
            return BadRequest("Error occured while creating the product");
        }

        [HttpGet("getAll")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GetAll(Guid companyId)
        {
            var result = await _productService.GetAllProducts(companyId);

            return Ok(result);
        }

        [HttpGet("getByName")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetByName(string companyName)
        {
            var result = await _productService.GetAllProducts(companyName);

            return Ok(result);
        }

        [HttpGet("getOne")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GetOne(Guid productId)
        {
            var result = await _productService.GetProductById(productId);

            return Ok(result);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Edit(Guid productId, EditProductDto model)
        {
            var result = await _productService.EditProduct(productId, model);

            return Ok(result);
        }
    }
}
