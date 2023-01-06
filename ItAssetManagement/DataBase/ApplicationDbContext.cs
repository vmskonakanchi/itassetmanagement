
using ItAssetManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace ItAssetManagement.DataBase
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<User> Users { get; private set; }

        public DbSet<Asset> Assets { get; private set; }

        public DbSet<AssetRequest> AssetRequests { get; private set; }
    }
}
