﻿using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductService
    {
        Task<bool> AddProduct(Guid companyId, CreateProductDto createProduct);

        Task<IEnumerable<ProductDto>> GetAllProducts(Guid companyId);

        Task<IEnumerable<ProductDto>> GetAllProducts(string companyName);

        Task<ProductDto> GetProductById(Guid productId);

        Task<ProductDto> EditProduct(Guid productId, EditProductDto model);
        Task<Product> GetProductByName(string name);
        Task AddProductToPrice(string priceId, Guid prodId);
        Task<string> GetProductToPriceById(string prodId);
    }
}
