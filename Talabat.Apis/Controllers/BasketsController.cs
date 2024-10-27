using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Apis.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories;

namespace Talabat.Apis.Controllers
{
	public class BasketsController : APIBaseController
	{
		private readonly IBasketRepository _basketRepository;

		public BasketsController(IBasketRepository basketRepository)
		{
			_basketRepository = basketRepository;
		}

		[HttpGet]
		public async Task<ActionResult<CustomerBasket>> GetCustomerBasket(string BasketId)
		{
			var Basket = await _basketRepository.GetBasketAsync(BasketId);
			return Basket is null ? new CustomerBasket(BasketId) : Basket;
		}
		[HttpPost]
		public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasket Basket)
		{
			var CreatedOrUpdatedBasket = await _basketRepository.UpdateBasketAsync(Basket);
			if (CreatedOrUpdatedBasket is null) return BadRequest(new ApiResponse(400));
			return Ok(CreatedOrUpdatedBasket);
		}
		[HttpDelete]
		public async Task<ActionResult<bool>> DeleteBasket(string BasketId)
		{
			return await _basketRepository.DeleteBasketAsync(BasketId);
		}
	}
}
