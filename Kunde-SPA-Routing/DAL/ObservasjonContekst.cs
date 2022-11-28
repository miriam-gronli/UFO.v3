using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Kunde_SPA_Routing.DAL
{
    //Denne klassen er hentet fra KundeApp2-med-DB filen i KundeApp2-med-DAL mappen fra canvas
    public class Observasjoner //Denne klassen blir referert til dersom man oppretter data som skal bli lagt til i databasen
    {
        public int Id { get; set; } //auto-increment i databasen
        public string Navn { get; set; }
        public string Postkode { get; set; }
        public string Beskrivelse { get; set; }
        public string Dato { get; set; }
        public string Tid { get; set; }
    }

    //Denne koden er hentet fra "DAL" mappen som igjen ligger under mappen "KundeApp2-med-hash-logginn" hentet fra canvas
    public class Brukere
    {
        public int Id { get; set; }
        public string Brukernavn { get; set; }
        public byte[] Passord { get; set; }
        public byte[] Salt { get; set; }
    }


    public class ObservasjonContext : DbContext //Klasse som oppretter databasen fysisk dersom databasen ikke er opprettet
    {
        public ObservasjonContext(DbContextOptions<ObservasjonContext> options)
                : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Observasjoner> Observasjoner { get; set; }
        public DbSet<Brukere> Brukere { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

    }
}
