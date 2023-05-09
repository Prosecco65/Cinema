using Microsoft.EntityFrameworkCore;
using Cinema.Models;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection.Emit;

namespace Cinema.DataAccess
{
    public class CinemaDbContext : IdentityDbContext<IdentityUser>
    {
        public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options) { }
        public DbSet<Utente> Utente { get; set; }
        public DbSet<Biglietti> Biglietti { get; set; }
        public DbSet<Film> Film { get; set; }
        public DbSet<Posti> Posti { get; set; }
        public DbSet<Sala> Sala { get; set; }
        public DbSet<Spettacolo> Spettacolo { get; set; }
        public DbSet<Valuta> Valuta { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<Biglietti>().HasKey(t => new { t.Id_biglietti, t.Id, t.Id_posti });
            builder.Entity<Biglietti>()
            .HasOne(e => e.Spettacolo)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict); ;
        }

    }
}
