using Core.Dtos.Requests;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace Api.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService2;
        private readonly IStripeService _service;
        public ProductsController(IProductService productService,IStripeService service)
        {
            _productService2 = productService;
            _service = service;
        }

        [HttpPost("create")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Create(Guid companyId, CreateProductDto product)
        {
            bool isSaved;


            isSaved = await _productService2.AddProduct(companyId, product);

            if (isSaved)
            {
                return Ok();
            }
            return BadRequest("Error occured while creating the product");
        }
        [HttpPost("createStripeProduct")]
        //[Authorize(Roles ="Company")]
        public async Task<IActionResult> CreateStripeProduct([FromBody] CreateProductDto productRequest)
        {
            try
            {
                var product= await _service.CreateProduct(productRequest);
                return Ok(product);
            }
            catch (StripeException e)
            {
                // Handle Stripe API errors
                return StatusCode((int)e.HttpStatusCode, new { Error = e.Message });
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, new { Error = "An error occurred while creating the product." });
            }
        }
        [HttpGet("getAllStripeProducts")]
        [Authorize(Roles ="Client")]
        public async Task<IActionResult> GetAllStripeProducts()
        {
            try
            {
                var products = await _service.GetAllProducts();
                return Ok(products);
            }
            catch (StripeException ex)
            {
                return StatusCode((int)ex.HttpStatusCode, new { Error = ex.Message });

            }
            catch (Exception ex)
            {
                // Handle other exceptions
                return StatusCode(500, new { Error = "An error occurred while creating the product." });
            }
        }
        [HttpPut("updateStripeProduct")]
        [Authorize(Roles ="Company")]
        public async Task<IActionResult> UpdateStripeProduct(string productId, [FromBody] EditProductDto model)
        {
            try
            {
                var product = await _service.EditProduct(productId,model);
                return Ok(product); 
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { Error = "An error occurred while updating the product." });
            }
        }

        [HttpGet("getAll")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GetAll(Guid companyId)
        {
            var result = await _productService2.GetAllProducts(companyId);

            return Ok(result);
        }

        [HttpGet("getByName")]
        [Authorize(Roles = "Client")]
        public async Task<IActionResult> GetByName(string companyName)
        {
            var result = await _productService2.GetAllProducts(companyName);

            return Ok(result);
        }

        [HttpGet("getOne")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> GetOne(Guid productId)
        {
            var result = await _productService2.GetProductById(productId);

            return Ok(result);
        }

        [HttpPut("edit")]
        [Authorize(Roles = "Company")]
        public async Task<IActionResult> Edit(Guid productId, EditProductDto model)
        {
            var result = await _productService2.EditProduct(productId, model);

            return Ok(result);
        }
    }
}
