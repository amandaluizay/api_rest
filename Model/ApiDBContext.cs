using Microsoft.EntityFrameworkCore;

namespace MinhaApi.Model
{
    public class ApiDbContext : DbContext
    {
        public ApiDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Fornecedor> Fornecedores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost,1433;Database=MinhaAPi;User ID=sa;Password=1q2w3e4r@#$");
        }
    }
}
