using MeuToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace MeuToDo.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ToDo> Todos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString:"DataSource=app.db; Cache=Shared");
        }

    }
}