using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Discord_Data.DataModels
{
    public class DatabaseContext : DbContext
    {
        public DbSet<MessageItem> Messages { get; set; }
        public DbSet<UserItem> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(JsonConvert.DeserializeObject<Configs>(File.ReadAllText("configs.json"))?.Database.ConnectionString());
        }
    }
}
