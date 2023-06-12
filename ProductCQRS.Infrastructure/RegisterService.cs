using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductCQRS.Application.DatabaseContextInterface;
using ProductCQRS.Infrastructure.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Infrastructure
{
	public static class RegisterService
	{
		public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<MyDatabaseContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
			});

			services.AddScoped<IMyDatabaseContext>(option => {
				return option.GetService<MyDatabaseContext>();
			});
		}
	}
}
