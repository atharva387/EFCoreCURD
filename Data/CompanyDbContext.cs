	using EFCoreCURD.Models;
	using Microsoft.EntityFrameworkCore;

	namespace EFCoreCURD.Data
	{
		public class CompanyDbContext : DbContext
		{
			public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options) { }

			public DbSet<Employee> Employees => Set<Employee>();
			 public DbSet<Department> Departments => Set<Department>();
    }
	}
