using AutoMapper;
using Talabat.Apis.DTOs;
using Talabat.Core.Entities;

namespace Talabat.Apis.Helpers
{
	public class MappingProfiles : Profile
	{
		public MappingProfiles()
		{
			CreateMap<Product, ProductToReturnDto>()
					 .ForMember(D => D.ProductType, O => O.MapFrom(S => S.ProductType.Name))
					 .ForMember(D => D.ProductBrand, O => O.MapFrom(S => S.ProductBrand.Name))
					 .ForMember(D => D.PictureUrl, O => O.MapFrom<ProductPictureUrlResolver>());
					
		}
	}
}
