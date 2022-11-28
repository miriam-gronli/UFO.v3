using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kunde_SPA_Routing.Model;

namespace Kunde_SPA_Routing.DAL
{
    public interface IObservasjonRepository
    {
        Task<bool> Lagre(Observasjon innObservasjon);
        Task<List<Observasjon>> HentAlle();
        Task<bool> Slett(int id);
        Task<Observasjon> HentEn(int id);
        Task<bool> Endre(Observasjon endreObservasjon);
        Task<bool> LoggInn(Bruker bruker);
    }
}
