using CitizenTech.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CitizenTech.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<PontoOnibus> Pontos => Set<PontoOnibus>();
    public DbSet<ChegadaOnibus> Chegadas => Set<ChegadaOnibus>();

    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.Entity<PontoOnibus>().ToTable("NET_PONTO_ONIBUS");
        mb.Entity<ChegadaOnibus>(e =>
        {
            e.ToTable("NET_CHEGADA_ONIBUS");
            e.HasOne(c => c.Ponto)
                .WithMany(p => p.Chegadas)
                .HasForeignKey(c => c.PontoOnibusId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}