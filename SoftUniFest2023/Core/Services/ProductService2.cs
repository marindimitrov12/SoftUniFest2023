using Core.Dtos.Requests;
using Core.Dtos.Responses;
using Core.Interfaces;

using Data.Model;
using Microsoft.EntityFrameworkCore;


namespace Services
{
    public class ProductService2 : IProductService
    {
        private readonly DbContext _context;
        public ProductService2(DbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddProduct(Guid companyId, CreateProductDto createProduct)
        {
            Product productToCreate = new Product
            {
                Name = createProduct.Name,
                Description = createProduct.Description,
                Price = createProduct.Price,
                CompanyId = companyId,
            };

            try
            {
                await _context.AddAsync(productToCreate);
                await _context.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public async Task AddProductToPrice(string priceId,Guid prodId)
        {
            var ptop = new ProductToPrice
            {
                PriceId = priceId,
                ProductId = prodId,
            };
            await _context.Set<ProductToPrice>().AddAsync(ptop);
            await _context.SaveChangesAsync();
        }
        public async Task<string> GetProductToPriceById(string prodId)
        {
            var result = await _context.Set<ProductToPrice>().FirstOrDefaultAsync(x => x.ProductId.ToString() == prodId);
            if (result==null)
            {
                throw new Exception("This product id doesn't exist");
            }

            return result.PriceId;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts(Guid companyId)
        {
            return await _context.Set<Product>()
                .Where(p => p.CompanyId == companyId)
                .Select(p => new ProductDto { Id = p.Id, Name = p.Name, Description = p.Description, Price = p.Price })
                .ToListAsync();
        }
        public async Task<Product> GetProductByName(string name)
        {
            return await _context.Set<Product>().FirstOrDefaultAsync(x => x.Name == name);
        }

        public async Task<ProductDto> GetProductById(Guid productId)
        {
            Product? product = await _context.Set<Product>()
                .Where(p => p.Id == productId)
                .FirstOrDefaultAsync();

            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public async Task<ProductDto> EditProduct(Guid productId, EditProductDto model)
        {
            Product? product = await _context.Set<Product>().Where(p => p.Id == productId).FirstOrDefaultAsync();

            product.Name = model.Name;
            product.Description = model.Description;
            product.Price = model.Price;

            await _context.SaveChangesAsync();

            return new ProductDto
            {
                Id = product.Id,
                Name = model.Name,
                Description = model.Description,
                Price = model.Price
            };
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts(string companyName)
        {
            var result= await _context.Set<Product>()
                .Include("Company")
                .Where(p => p.Company.Name == companyName)
                .Select(p => new ProductDto { Id = p.Id, Name = p.Name, Description = p.Description, Price = p.Price })
                .ToListAsync();
            return result;
        }
    }
}