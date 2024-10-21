using LatihanJWT.Models;
using Microsoft.EntityFrameworkCore;

namespace LatihanJWT.Data
{
    public class DBText : DbContext
    {
        public DBText(DbContextOptions<DBText> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Data Source=.\\sqlexpress;Initial Catalog=books;Integrated Security=True;Trust Server Certificate=True");
        }
        public DbSet<Users> users { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<Books> books { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Users>()
                .HasOne(s => s.roles)
                .WithMany(s => s.users)
                .HasForeignKey(s => s.roles_id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Roles_Users");
        }
    }
}
