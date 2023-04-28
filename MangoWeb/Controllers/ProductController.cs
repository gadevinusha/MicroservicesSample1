using MangoWeb.Models;
using MangoWeb.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MangoWeb.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new List<ProductDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response =await  _productService.GetAllProductsAsync<ResponseDto>(accessToken);
            if(response!=null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> CreateProduct()
        {
            //List<ProductDto> list = new List<ProductDto>();
            //var response = await _productService.CreateProductAsync<ResponseDto>(productDto);
            //if (response != null && response.IsSuccess)
            //{
            //    list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            //}
            return  View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateProduct(ProductDto productDto)
        {
            //List<ProductDto> list = new List<ProductDto>();
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.CreateProductAsync<ResponseDto>(productDto,accessToken);
                if (response != null && response.IsSuccess)
                {
                    //list = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(productDto);
        }

        [HttpGet]
        public async Task<IActionResult> EditProduct(int productId)
        {
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, accessToken);
                if (response != null && response.IsSuccess)
                {
                    ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                    return View(model);
                }
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditProduct(ProductDto productDto)
        {
            //List<ProductDto> list = new List<ProductDto>();
            if (ModelState.IsValid)
            {
                var accessToken = await HttpContext.GetTokenAsync("access_token");
                var response = await _productService.CreateProductAsync<ResponseDto>(productDto, accessToken);
                if (response != null && response.IsSuccess)
                {
                    //list = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(productDto);
        }
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            List<ProductDto> list = new List<ProductDto>();
            var accessToken = await HttpContext.GetTokenAsync("access_token");
            var response = await _productService.DeleteProductAsync<ResponseDto>(productId, accessToken);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
                //list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
