using Microsoft.EntityFrameworkCore;

namespace DataBaseSqlLiteFor_Is_19_03.DB
{
    public class MySqlLiteContext : DbContext
    {
        private string conectionString 
            = "Filename=MySqlLitePhone.db"; 
        // она  может быть  вне  проекта 

        public MySqlLiteContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder
            optionsBuilder)
        {
            optionsBuilder.UseSqlite(conectionString);
        }

        public DbSet<Model.Phone> Phones { get; set; }
        public DbSet<Model.Company> Companies { get; set; }
    }
}
