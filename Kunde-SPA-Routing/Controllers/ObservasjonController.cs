using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kunde_SPA_Routing.DAL;
using Kunde_SPA_Routing.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kunde_SPA_Routing.Controllers
{

    //Denne klassen er hentet fra KundeApp2-med-DB filen i KundeApp2-med-DAL mappen fra canvas
    //Alle linjer med logging (ILogger) og feilhåndtering (BadRequest(), Ok(), NotFound(), Unauthorized() osv.)
    //er hentet fra modul videoene "Feilhåndtering" og "Logging til fil" på Canvas
    [ApiController]
    [Route("api/[controller]")]
    public class ObservasjonController : ControllerBase
    {
        private readonly IObservasjonRepository _db; //Initierer IObservasjoRepository db variabel

        private ILogger<ObservasjonController> _log; //Initierer IILoggerFactory i controllern

        private const string _loggetInn = "loggetInn";
      

        //Dependency Injection av IObservasjonRepository
        //ILogger blir tatt inn i controllern
        public ObservasjonController(IObservasjonRepository db, ILogger<ObservasjonController> log)
        {
            _db = db;
            _log = log;
        }

        [HttpPost]
        public async Task<ActionResult> Lagre(Observasjon innObservasjon)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                HttpContext.Session.SetString(_loggetInn, "");
                return Unauthorized("Ikke logget inn");                
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Lagre(innObservasjon);
                if (!returOK)
                {
                    _log.LogInformation("Observasjon kunne ikke lagres");
                    return BadRequest("Observasjon kunne ikke lagres");
                }
                return Ok();
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpGet]
        public async Task<ActionResult> HentAlle()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                HttpContext.Session.SetString(_loggetInn, "");
                return Unauthorized("Ikke logget inn");                
            }
            List<Observasjon> alleObservasjoner = await _db.HentAlle();

            if (alleObservasjoner == null)
            {
                _log.LogInformation("Ingen observasjoner ble funnet");
                return NotFound("Ingen observasjoner ble funnet");
            }
            return Ok(alleObservasjoner);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Slett(int id)
        {

            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                HttpContext.Session.SetString(_loggetInn, "");
                return Unauthorized("Ikke logget inn");                
            }
            bool returOK = await _db.Slett(id);
            if (!returOK)
            {
                _log.LogInformation("Sletting av observasjon ble ikke utført");
                return NotFound("Sletting av observasjon ble ikke utført");
            }
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> HentEn(int id)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                HttpContext.Session.SetString(_loggetInn, "");
                return Unauthorized("Ikke logget inn");                
            }
            if (ModelState.IsValid)
            {
                Observasjon observasjonen = await _db.HentEn(id);
                if (observasjonen == null)
                {
                    _log.LogInformation("Fant ikke observasjonen");
                    return NotFound("Fant ikke observasjonen");
                }
                return Ok(observasjonen);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        [HttpPut]
        public async Task<ActionResult> Endre(Observasjon endreObservasjon)
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(_loggetInn)))
            {
                HttpContext.Session.SetString(_loggetInn, "");
                return Unauthorized("Ikke logget inn");
            }
            if (ModelState.IsValid)
            {
                bool returOK = await _db.Endre(endreObservasjon);
                if (!returOK)
                {
                    _log.LogInformation("Endringen kunne ikke utføres");
                    return NotFound("Endringen av observasjonen kunne ikke utføres");
                }
                return Ok();
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        
        [HttpPost("logginn")]
        public async Task<ActionResult> LoggInn(Bruker bruker)
        {
            if (ModelState.IsValid)
            {
                bool returnOK = await _db.LoggInn(bruker);
                if (!returnOK)
                {
                    _log.LogInformation("Innloggingen feilet for bruker " + bruker.Brukernavn);
                    HttpContext.Session.SetString(_loggetInn, "");                   
                    return Unauthorized("Logget ikke inn");
                }
                HttpContext.Session.SetString(_loggetInn, "LoggetInn");
                return Ok(true);
            }
            _log.LogInformation("Feil i inputvalidering");
            return BadRequest("Feil i inputvalidering på server");
        }

        
        [HttpPost("loggut")]        
        public async Task<ActionResult> LoggUt()
        {
            HttpContext.Session.SetString(_loggetInn, "");
            return Ok(true);
        }
    }
}
