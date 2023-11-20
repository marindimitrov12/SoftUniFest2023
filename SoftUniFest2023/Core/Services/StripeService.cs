using Core.Dtos.Requests;
using Core.Interfaces;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class StripeService : IStripeService
    {
        private readonly ProductCreateOptions _options;
        private readonly ProductUpdateOptions _updateOptions;
        private readonly ProductService _service;
        public StripeService(ProductCreateOptions options, ProductService service, ProductUpdateOptions updateOptions)
        {
                _options = options;
                _service = service;
            _updateOptions= updateOptions;
        }
        public async Task<Product> CreateProduct(CreateProductDto productReq)
        {
			try
			{
                _options.Name = productReq.Name;
                _options.Description = productReq.Description;
                var product = await _service.CreateAsync(_options);
                return product;
			}
			catch (Exception ex)
			{

                throw new Exception(ex.Message);
			}
        }

        public async Task<Product> EditProduct(string id, EditProductDto model)
        {
            try
            {
                var product=await _service.GetAsync(id);
                if (product==null)
                {
                    throw new Exception("Product not found");
                }
                _updateOptions.Name = model.Name;
                _updateOptions.Description = model.Description;
                var updatedProduct = await _service.UpdateAsync(id, _updateOptions);
                return updatedProduct;
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<List<Product>> GetAllProducts()
        {
            try
            {
                var products = await _service.ListAsync(new ProductListOptions { Limit=100});
                return products.ToList();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
