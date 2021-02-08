using HPlusSport.API.Models;
using HPlusSupport.API.Classes;
using HPlusSupport.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HPlusSupport.API.Controllers{
    [Route("[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase{

        private readonly ShopContext _context;

        public ProductsController(ShopContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        //[HttpGet]return list of products 
        //public IEnumerable<Product> GetAllProducts()
        //{
        //    return _context.Products.ToList();
        //}

        //[HttpGet] //IActionResult: returns http status code plus products 
        //public IActionResult GetAllProducts()
        //{
        //    return Ok(_context.Products.ToList());
        //}

        //returning an Item using ActionResult
        //[HttpGet, Route("/products/{id}")]
        [HttpGet("{id:int}")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        ////returning an Item 
        //[HttpGet, Route("/products/{id}")]
        //public IActionResult GetProduct(int id)
        //{
        //    var product = _context.Products.Find(id);
        //    return product;
        //}
        //async version
        //[HttpGet("{id:int}")]
        //public async Task<IActionResult> GetProduct(int id)
        //{
        //    var product = await _context.Products.FindAsync(id);
        //    if (product == null)
        //        return NotFound();
        //    return Ok(product);
        //}

        [HttpGet] //this info comes from URL so [FromQuery]
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductQueryParameters queryParameters)
        {   //paginating items 
            IQueryable<Product> products = _context.Products;
            if(queryParameters.MinPrice != null &&
                queryParameters.MaxPrice != null)
            {
                products = products.Where(
                    p => p.Price >= queryParameters.MinPrice.Value &&
                        p.Price <= queryParameters.MaxPrice.Value);
            }
            if (!string.IsNullOrEmpty(queryParameters.Sku))
            {
                products = products.Where(
                   p => p.Sku == queryParameters.Sku);
            }
            if (!string.IsNullOrEmpty(queryParameters.Name))
            {
                products = products.Where(
                  p => p.Name.ToLower().Contains(queryParameters.Name.ToLower()));
            }
            if (!string.IsNullOrEmpty(queryParameters.SortBy))
            {
                if(typeof(Product).GetProperty(queryParameters.SortBy) != null)
                {
                    products = products.OrderByCustom(queryParameters.SortBy, queryParameters.SortOrder);
                }
            }

            products = products
                .Skip(queryParameters.Size * (queryParameters.Page - 1))
                .Take(queryParameters.Size);

            return Ok(await products.ToArrayAsync());
        }

    }
}
