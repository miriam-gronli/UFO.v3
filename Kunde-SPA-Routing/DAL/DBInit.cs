//using Kunde_SPA_Routing.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kunde_SPA_Routing.DAL;

namespace Kunde_SPA_Routing.DAL
{
    public static class DBInit
    {
        public static void Initialize(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<ObservasjonContext>();

                // Disse forsikrer at databasen slettes og opprettes hver gang den skal initieres
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var observasjon1 = new Observasjoner //Dummy data #1
                {
                    Id = 1,
                    Postkode = "0001",
                    Beskrivelse = "Grønn UFO",
                    Navn = "Ola Nordmann",
                    Dato = "1 januar 2022",
                    Tid = "22:30"
                };

                var observasjon2 = new Observasjoner //Dummy data #2
                {
                    Id = 2,
                    Postkode = "0002",
                    Beskrivelse = "Blå UFO",
                    Navn = "Sam Møller",
                    Dato = "2 januar 2022",
                    Tid = "21:30"
                };
                var observasjon3 = new Observasjoner //Dummy data #2
                {
                    Id = 3,
                    Postkode = "0003",
                    Beskrivelse = "Rød UFO",
                    Navn = "Jonas Hansen",
                    Dato = "3 januar 2022",
                    Tid = "20:30"
                };

                // lag en påoggingsbruker
                //Koden under er hentet fra "DAL" mappen som igjen ligger under mappen "KundeApp2-med-hash-logginn" hentet fra canvas
                var bruker = new Brukere();
                bruker.Brukernavn = "Admin";
                var passord = "Test11";
                byte[] salt = ObservasjonRepository.LagSalt();
                byte[] hash = ObservasjonRepository.LagHash(passord, salt);
                bruker.Passord = hash;
                bruker.Salt = salt;

                //Legger til dummy dataen i databasen
                context.Observasjoner.Add(observasjon1);
                context.Observasjoner.Add(observasjon2);
                context.Observasjoner.Add(observasjon3);
                context.Brukere.Add(bruker);

                context.SaveChanges();
            }
        }
    }  
}
