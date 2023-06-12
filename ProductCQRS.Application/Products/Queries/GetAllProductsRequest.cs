using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.Products.Queries
{
	public class GetAllProductsRequest : IRequest<IEnumerable<GetAllProductsResponse>>
	{

	}
}
