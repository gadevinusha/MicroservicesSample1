using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mango.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _db;
        private IMapper _mapper;
        public ProductRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto,Product>(productDto);
            if(product.ProductId>0)
            {
                _db.Products.Update(product);
            }
            else
            {
                _db.Products.Add(product);
            }
            await _db.SaveChangesAsync();
            return _mapper.Map<Product, ProductDto>(product);
            //throw new NotImplementedException();
        }

        public async Task<bool> DeleteProduct(int ProductId)
        {
            try
            {
                var productdto = await _db.Products.Where(x => x.ProductId == ProductId).FirstOrDefaultAsync();
                if (productdto != null)
                {
                    _db.Products.Remove(productdto);
                    await _db.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
           // throw new NotImplementedException();
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            var productDto = await _db.Products.Where(x => x.ProductId == productId).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(productDto);
            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var dummylist =new List<ProductDto>();
            return dummylist;
            //var productDtoList = await _db.Products.ToListAsync();//uncomment for orginal code
            //return _mapper.Map<List<ProductDto>>(productDtoList);//uncomment for orginal code
            //throw new NotImplementedException();
        }
    }
}
