using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCQRS.Application.DatabaseContextInterface;
using ProductCQRS.Application.Products.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.Products.QueryHandlers
{
	public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsRequest, IEnumerable<GetAllProductsResponse>>
	{
		protected IMyDatabaseContext MyDbContext { get; }
		protected IMapper Mapper { get; }

		public GetAllProductsQueryHandler(IMyDatabaseContext myDbContext, IMapper mapper)
		{
			MyDbContext = myDbContext;
			Mapper = mapper;
		}

		public async Task<IEnumerable<GetAllProductsResponse>> Handle(GetAllProductsRequest request, CancellationToken cancellationToken)
		{
			return await MyDbContext.Products.ProjectTo<GetAllProductsResponse>(Mapper.ConfigurationProvider).ToListAsync();
		}
	}
}
