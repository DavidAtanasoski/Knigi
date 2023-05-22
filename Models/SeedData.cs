using Knigi.Data;
using Microsoft.EntityFrameworkCore;

namespace Knigi.Models
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new KnigiContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<KnigiContext>>()))
            {
                if(context.Kniga.Any() || context.Avtor.Any())
                {
                    return;
                }

                context.Avtor.AddRange(
                    new Avtor
                    {
                        Ime = "Petko",
                        Prezime = "Petkovski",
                        Nacionalnost = "Marsovec",
                        DatumRagjanje = DateTime.Parse("1919-5-5")
                    },
                    new Avtor
                    {
                        Ime = "Ivan",
                        Prezime = "Ivanovski",
                        Nacionalnost = "Belgiec",
                        DatumRagjanje = DateTime.Parse("2020-3-3")
                    },
                    new Avtor
                    {
                        Ime = "Marko",
                        Prezime = "Markovski",
                        Nacionalnost = "Amerikanec",
                        DatumRagjanje = DateTime.Parse("2023-9-9")
                    }
                    );
                context.SaveChanges();

                context.Kniga.AddRange(
                    new Kniga 
                    { 
                        Naslov = "Art of War",
                        Godina = 1998,
                        Opis = "Ova e kratok opis",
                        SlikaUrl = "/covers/art_of_war.jpg",
                        AvtorId = context.Avtor.Single(a => a.Ime == "Ivan" && a.Prezime == "Ivanovski").Id
                    },
                    new Kniga
                    {
                        Naslov = "Moby Dick",
                        Godina = 1851,
                        Opis = "Ova e kratok opis za moby dick",
                        SlikaUrl = "/covers/moby_dick.jpg",
                        AvtorId = context.Avtor.Single(a => a.Ime == "Marko" && a.Prezime == "Markovski").Id
                    },
                    new Kniga
                    {
                        Naslov = "Pride and Prejudice",
                        Godina = 1813,
                        Opis = "Ova e kratok opis za Pride and Prejudice",
                        SlikaUrl = "/covers/pride_prejudice.jpg",
                        AvtorId = context.Avtor.Single(a => a.Ime == "Ivan" && a.Prezime == "Ivanovski").Id
                    },
                    new Kniga
                    {
                        Naslov = "Crime and Punishment",
                        Godina = 1866,
                        Opis = "Ova e kratok opis za Crime and Punishment",
                        SlikaUrl = "/covers/crime_punishment.jpg",
                        AvtorId = context.Avtor.Single(a => a.Ime == "Petko" && a.Prezime == "Petkovski").Id
                    },
                    new Kniga
                    {
                        Naslov = "The Hitchhiker's Guide to the Galaxy",
                        Godina = 1979,
                        Opis = "Ova e kratok opis za The Hitchhiker's Guide to the Galaxy",
                        SlikaUrl = "/covers/guide_galaxy.jpg",
                        AvtorId = context.Avtor.Single(a => a.Ime == "Marko" && a.Prezime == "Markovski").Id
                    }
                    );
                context.SaveChanges();

                context.Zanr.AddRange(
                    new Zanr
                    {
                        Ime = "Fiction"
                    },
                    new Zanr
                    {
                        Ime = "Classics"
                    },
                    new Zanr
                    {
                        Ime = "Romance"
                    },
                    new Zanr
                    {
                        Ime = "Drama"
                    }
                    );
                context.SaveChanges();

                context.KnigaZanr.AddRange(
                    new KnigaZanr { KnigaId = 1, ZanrId = 1 },
                    new KnigaZanr { KnigaId = 2, ZanrId = 2 },
                    new KnigaZanr { KnigaId = 3, ZanrId = 3 },
                    new KnigaZanr { KnigaId = 4, ZanrId = 4 },
                    new KnigaZanr { KnigaId = 5, ZanrId = 1 }
                    );
                context.SaveChanges();
            }
        }
    }
}
