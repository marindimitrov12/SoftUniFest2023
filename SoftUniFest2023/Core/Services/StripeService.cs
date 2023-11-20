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
        private readonly ProductService _service;
        public StripeService(ProductCreateOptions options, ProductService service)
        {
                _options = options;
                _service = service;
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
    }
}
