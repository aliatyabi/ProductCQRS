using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCQRS.Application.DatabaseContextInterface;
using ProductCQRS.Application.Products.Commands;
using ProductCQRS.Domain.Entities;
using ProductCQRS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.Products.CommandHandlers
{
	public class UpdateProductCommandHandler : IRequestHandler<UpdateProductRequest, int>
	{
		protected IMyDatabaseContext MyDbContext { get; }
		protected IMapper Mapper { get; }

		public UpdateProductCommandHandler(IMyDatabaseContext myDbContext, IMapper mapper)
		{
			MyDbContext = myDbContext;
			Mapper = mapper;
		}

		public async Task<int> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
		{
			var product = await MyDbContext.Products.FirstOrDefaultAsync(x => x.Id == request.Id);

			Mapper.Map(request, product);

			if (product == null)
			{
				return -1;
			}
			else
			{
				await Task.Run(() => MyDbContext.Products.Update(product)); 

				await MyDbContext.SaveToDbAsync();

				return product.Id;
			}
		}
	}
}
