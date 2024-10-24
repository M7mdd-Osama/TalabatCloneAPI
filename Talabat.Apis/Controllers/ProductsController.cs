using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

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
			var Spec = new ProductWithBrandAndTypeSpecifications();
			var Products = await _productRepo.GetAllWithSpecAsync(Spec);
			return Ok(Products);
		}
		[HttpGet("{id}")]
		public async Task<ActionResult<Product>> GetProduct(int id)
		{
			var Spec = new ProductWithBrandAndTypeSpecifications(id);
			var Product = await _productRepo.GetAllWithSpecAsync(Spec);
			return Ok(Product);
		}
    }
}
