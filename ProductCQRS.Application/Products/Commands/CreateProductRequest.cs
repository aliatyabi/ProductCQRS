using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.Products.Commands
{
	public class CreateProductRequest : IRequest<int>
	{
		public string? Name { get; set; }

		public string? ManufacturePhone { get; set; }

		public string? ManufactureEmail { get; set; }

		public bool IsAvailable { get; set; }
	}
}
