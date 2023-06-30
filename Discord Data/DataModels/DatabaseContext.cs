using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Discord_Data.DataModels
{
    public class DatabaseContext : DbContext
    {
        /*

        To automatically migrate content in the Teminal of this Discord Data folder enter

        dotnet ef migrations add {{Migration Name}}

        then

        dotnet ef database update

        */

        public DbSet<MessageItem> Messages { get; set; }
        public DbSet<UserItem> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(JsonConvert.DeserializeObject<Configs>(File.ReadAllText("configs.json"))?.Database.ConnectionString());
        }
    }
}
