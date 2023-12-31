﻿using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCQRS.Application.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application
{
	public static class RegisterService
	{
		public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMediatR(_ => _.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));
		}
	}
}
