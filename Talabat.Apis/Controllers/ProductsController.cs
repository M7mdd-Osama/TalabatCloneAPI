using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Apis.Controllers
{
	public class ProductsController : APIBaseController
	{
		private readonly IGenericRepository<Product> _productRepo;

		public ProductsController(IGenericRepository<Product> ProductRepo)
		{
			_productRepo = ProductRepo;
		}
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
		{
			var Products = await _productRepo.GetAllAsync();
			return Ok(Products);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var Product = await _productRepo.GetByIdAsync(id);
			return Ok(Product);
		}
    }
}
