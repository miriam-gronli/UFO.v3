using System;
using System.ComponentModel.DataAnnotations;

namespace Kunde_SPA_Routing.Model
{
    public class Observasjon //Vanlig POJO klasse for observasjonene
    {
        public int Id { get; set; }  // Id blir brukt som auto-increment i databasen

        [RegularExpression(@"[0-9a-zA-ZøæåØÆÅ\\:_@#/,'()-. ]{2,30}")]
        public string Navn { get; set; }
        [RegularExpression(@"[0-9]{4}")]
        public string Postkode { get; set; }
        [RegularExpression(@"[0-9a-zA-ZøæåØÆÅ\\:_!@#/,'()-. ]{2,5000}")]
        public string Beskrivelse { get; set; }
        [RegularExpression(@"[0-9a-zA-ZøæåØÆÅ\\:/,-. ]{2,20}")]
        public string Dato { get; set; }
        [RegularExpression(@"([01]?[0-9]|2[0-3]):[0-5][0-9]")]
        public string Tid { get; set; }

    }
}
