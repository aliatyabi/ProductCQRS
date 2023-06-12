using AutoMapper;
using ProductCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.Products.Queries
{
	public class GetAllProductsMapper : Profile
	{
        public GetAllProductsMapper()
        {
			CreateMap<Product, GetAllProductsResponse>();
		}
    }
}
