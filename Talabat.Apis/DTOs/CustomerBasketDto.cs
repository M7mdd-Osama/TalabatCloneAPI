﻿using System.ComponentModel.DataAnnotations;

namespace Talabat.Apis.DTOs
{
	public class CustomerBasketDto
	{
        [Required]
        public string Id { get; set; }
        public List<BasketItemDto> Items { get; set; }
    }
}
