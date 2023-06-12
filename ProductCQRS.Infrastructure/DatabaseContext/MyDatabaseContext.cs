using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
	public class MyDatabaseContext : IdentityDbContext<User>, IMyDatabaseContext
	{
		public MyDatabaseContext(DbContextOptions<MyDatabaseContext> options) : base(options)
		{
			Database.EnsureCreated();
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			//string adminId = "84DE3171-59CD-4A9F-B8A0-97507E2A7222";
			//string roleId = "E05D56ED-2D37-421D-985B-7E4DE3557754";

			////Seed admin role
			//builder.Entity<IdentityRole>().HasData(new IdentityRole
			//{
			//	Name = "admin",
			//	NormalizedName = "ADMIN",
			//	Id = roleId,
			//	ConcurrencyStamp = roleId
			//});

			////Create user
			//var user = new User
			//{
			//	Id = adminId,
			//	UserName = "aliatyabi",
			//	Firstname = "Ali",
			//	Lastname = "Atyabi",
			//	Email = "admin@gmail.com",
			//	EmailConfirmed = true,
			//};

			////Set user password
			//PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

			//user.PasswordHash = passwordHasher.HashPassword(user, "Password@1234");

			////Seed user
			//builder.Entity<User>().HasData(user);

			////Set user role to admin
			//builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			//{
			//	RoleId = roleId,
			//	UserId = adminId
			//});
		}

		public DbSet<Product> Products { get; set; }

		public async Task<int> SaveToDbAsync()
		{
			return await SaveChangesAsync();
		}
	}
}
