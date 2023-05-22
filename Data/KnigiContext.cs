using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Knigi.Models;

namespace Knigi.Data
{
    public class KnigiContext : DbContext
    {
        public KnigiContext (DbContextOptions<KnigiContext> options)
            : base(options)
        {
        }

        public DbSet<Avtor> Avtor { get; set; } = default!;

        public DbSet<Kniga> Kniga { get; set; } = default!;

        public DbSet<Zanr> Zanr { get; set; } = default!;

        public DbSet<KnigaZanr> KnigaZanr { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<KnigaZanr>()
        //        .HasOne<Kniga>(x => x.Kniga)
        //        .WithMany(x => x.Zanrovi)
        //        .HasForeignKey(x => x.KnigaId);

        //    modelBuilder.Entity<KnigaZanr>()
        //        .HasOne<Zanr>(x => x.Zanr)
        //        .WithMany(x => x.Knigi)
        //        .HasForeignKey(x => x.ZanrId);

        //    modelBuilder.Entity<Kniga>()
        //        .HasOne<Avtor>(x => x.Avtor)
        //        .WithMany(x => x.Knigi)
        //        .HasForeignKey(x => x.AvtorId);
        //}
    }
}
