//Denne koden er hentet fra "Model" mappen som igjen ligger under mappen "KundeApp2-med-hash-logginn" hentet fra canvas

using System;
using System.ComponentModel.DataAnnotations;

namespace Kunde_SPA_Routing.Model
{
    public class Bruker
    {
        [RegularExpression(@"[a-zA-Z0-9\-_]{3,15}")]
        public String Brukernavn { get; set; }
        [RegularExpression(@"[0-9A-Za-z]{4,64}")]
        public String Passord { get; set; }
    }
}

