using Microsoft.EntityFrameworkCore;
using ProductCQRS.Application.DatabaseContextInterface;
using ProductCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Infrastructure.DatabaseContext
{
	public class MyDatabaseContext : DbContext, IMyDatabaseContext
	{
		public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
		}

		public DbSet<Product> Products { get; set; }

		public async Task<int> SaveToDbAsync()
		{
			return await SaveChangesAsync();
		}
	}
}
