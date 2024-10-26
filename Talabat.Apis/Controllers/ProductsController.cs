using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.DTOs;
using Talabat.Apis.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;
using Talabat.Core.Specifications;

namespace Talabat.Apis.Controllers
{
	public class ProductsController : APIBaseController
	{
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IMapper _mapper;
		private readonly IGenericRepository<ProductType> _typeRepo;
		private readonly IGenericRepository<ProductBrand> _brandRepo;

		public ProductsController(IGenericRepository<Product> ProductRepo,
			IMapper mapper,
			IGenericRepository<ProductType> TypeRepo,
			IGenericRepository<ProductBrand> BrandRepo)
		{
			_productRepo = ProductRepo;
			_mapper = mapper;
			_typeRepo = TypeRepo;
			_brandRepo = BrandRepo;
		}
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts(string? Sort)
		{
			var Spec = new ProductWithBrandAndTypeSpecifications(Sort);
			var Products = await _productRepo.GetAllWithSpecAsync(Spec);
			var MappedProducts = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(Products);
			return Ok(MappedProducts);
		}

		[HttpGet("{id}")]
		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
		public async Task<ActionResult<ProductToReturnDto>> GetProductById(int id)
		{
			var Spec = new ProductWithBrandAndTypeSpecifications(id);
			var Product = await _productRepo.GetByIdWithSpecAsync(Spec);
			if (Product is null) return NotFound(new ApiResponse(404));
			var MappedProduct = _mapper.Map<Product, ProductToReturnDto>(Product);
			return Ok(MappedProduct);
		}

		[HttpGet("Types")]
		public async Task<ActionResult<IReadOnlyList<ProductType>>> GetTypes()
		{
			var Types = await _typeRepo.GetAllAsync();
			return Ok(Types);
		}

		[HttpGet("Brands")]
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var Brands = await _brandRepo.GetAllAsync();
			return Ok(Brands);
		}
	}
}
		