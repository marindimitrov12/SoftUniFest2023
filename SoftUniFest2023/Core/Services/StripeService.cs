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
        private readonly PriceCreateOptions _priceOptions;
        private readonly ProductUpdateOptions _updateOptions;
        private readonly ProductService _service;
        private readonly PriceService _priceService;
        public StripeService(ProductCreateOptions options,
            ProductService service,
            ProductUpdateOptions updateOptions,
            PriceCreateOptions priceOptions,
            PriceService priceService
            )
        {
            _options = options;
            _service = service;
            _updateOptions = updateOptions;
            _priceOptions = priceOptions;
            _priceService = priceService;
        }
        public async Task<Product> CreateProduct(CreateProductDto productReq)
        {
			try
			{
                _options.Name = productReq.Name;
                _options.Description = productReq.Description;
               var product = await _service.CreateAsync(_options);
                _priceOptions.Product = product.Id;
                _priceOptions.UnitAmountDecimal = productReq.Price * 100;
                _priceOptions.Currency = "bgn";
                await _priceService.CreateAsync(_priceOptions);
                return product;
			}
			catch (Exception ex)
			{

                throw new Exception(ex.Message);
			}
        }

        public async Task<Product> EditProduct( EditProductDto model)
        {
            try
            {
               
                var products=await _service.ListAsync();
                var product = products.FirstOrDefault(x=>x.Name==model.OldName);
                if (product==null)
                {
                    throw new Exception("Product not found");
                }
                _updateOptions.Name = model.Name;
                _updateOptions.Description = model.Description;
            
                var updatedProduct = await _service.UpdateAsync(product.Id, _updateOptions);
              
                
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
