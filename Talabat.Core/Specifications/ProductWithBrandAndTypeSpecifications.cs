using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.Specifications
{
	public class ProductWithBrandAndTypeSpecifications : BaseSpecifications<Product>
	{
		public ProductWithBrandAndTypeSpecifications(string Sort) : base()
		{
			Includes.Add(P => P.ProductType);
			Includes.Add(P => P.ProductBrand);
			if (!string.IsNullOrEmpty(Sort))
			{
				switch (Sort)
				{
					case "PriceAsc":
						AddOrderBy(P => P.Price);
						break;
					case "PriceDesc":
						AddOrderByDescending(P => P.Price);
						break;
					default:
						AddOrderBy(P => P.Name);
						break;
				}
			}

		}
		public ProductWithBrandAndTypeSpecifications(int id) : base(P => P.Id == id)
		{
			Includes.Add(P => P.ProductType);
			Includes.Add(P => P.ProductBrand);
		}
	}
}
