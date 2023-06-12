using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProductCQRS.Api.ViewModels;
using ProductCQRS.Application.Products.Commands;
using ProductCQRS.Application.Products.Queries;
using ProductCQRS.Domain.Entities;
using ProductCQRS.Infrastructure.DatabaseContext;
using System.Security.Claims;

namespace ProductCQRS.Api.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ProductController : ControllerBase
	{
		protected ClaimsPrincipal User { get; }

		protected UserManager<User> UserManager { get; }

		protected IMediator Mediator { get; }

		public ProductController(IHttpContextAccessor httpContextAccessor, UserManager<User> userManager, IMediator mediator)
		{
			User = httpContextAccessor.HttpContext.User;
			UserManager = userManager;
			Mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetAsync()
		{
			var response = await Mediator.Send(new GetAllProductsRequest());

			return Ok(response);
		}

		[Authorize]
		[HttpPost]
		public async Task<IActionResult> PostAsync(CreateProductRequest request)
		{
			var user = await UserManager.FindByNameAsync(User.Identity.Name);

			request.UserId = user.Id;

			var newProductId = await Mediator.Send(request);

			return Ok(newProductId);
		}

		[HttpPut]
		public async Task<IActionResult> PutAsync(UpdateProductRequest request)
		{
			var productId = await Mediator.Send(request);

			return Ok(productId);
		}
	}
}
