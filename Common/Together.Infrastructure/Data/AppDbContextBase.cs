using Microsoft.EntityFrameworkCore;

namespace Together.Infrastructure.Data
{
    public abstract class AppDbContextBase : DbContext
    {
        protected AppDbContextBase(DbContextOptions options) : base(options)
        {
        }
    }
}
