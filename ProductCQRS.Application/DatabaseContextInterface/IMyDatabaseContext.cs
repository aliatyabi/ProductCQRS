using Microsoft.EntityFrameworkCore;
using ProductCQRS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductCQRS.Application.DatabaseContextInterface
{
	public interface IMyDatabaseContext
	{
		DbSet<Product> Products { get; }

		Task<int> SaveToDbAsync();
	}
}
