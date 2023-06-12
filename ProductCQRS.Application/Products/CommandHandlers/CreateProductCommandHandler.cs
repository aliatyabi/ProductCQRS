using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ProductCQRS.Application.DatabaseContextInterface;
using ProductCQRS.Application.Products.Commands;
using ProductCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.Products.CommandHandlers
{
	public class CreateProductCommandHandler : IRequestHandler<CreateProductRequest, int>
	{
		protected IMyDatabaseContext MyDbContext { get; }
		protected IMapper Mapper { get; }

		public CreateProductCommandHandler(IMyDatabaseContext myDbContext, IMapper mapper)
		{
			MyDbContext = myDbContext;
			Mapper = mapper;
		}

		public async Task<int> Handle(CreateProductRequest request, CancellationToken cancellationToken)
		{
			var newProduct = Mapper.Map<Product>(request);

			await MyDbContext.Products.AddAsync(newProduct);

			await MyDbContext.SaveToDbAsync();

			return newProduct.Id;
		}
	}
}
