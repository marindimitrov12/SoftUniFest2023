using Core.Dtos.Requests;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IStripeService
    {
        Task<Product> CreateProduct(CreateProductDto product);
        Task<List<Product>>GetAllProducts();
        Task<Product> EditProduct( EditProductDto model);
    }
}
