using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProductCQRS.Application.Products.Queries;

namespace ProductCQRS.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductController : ControllerBase
	{
		protected IMediator Mediator { get; }

		public ProductController(IMediator mediator)
		{
			Mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAsync()
		{
			var response = await Mediator.Send(new GetAllProductsRequest());

			return Ok(response);
		}
	}
}
