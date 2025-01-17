using Microsoft.EntityFrameworkCore;

namespace FirstSite.Models
{
	public class MyDbContext: DbContext
	{
		public MyDbContext(DbContextOptions<MyDbContext> options): base(options)
		{ }

		public DbSet<Car> Cars { get; set; }
	}
}
