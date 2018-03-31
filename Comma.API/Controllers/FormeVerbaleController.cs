using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Comma.DomainEnitites;
using Comma.Repository;

namespace Comma.API.Controllers
{
    public class FormeVerbaleController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public VerbApi Get(string id)
        {
            VerbRepository verbRepository = new VerbRepository();
            //return new string[] { id, id};
            var verb = verbRepository.GetByName(id, true);
            var verApi = new VerbApi();
            verApi.Infinitiv = verb.Infinitiv;
            verApi.Gerunziu = verb.Gerunziu;
            verApi.Participiu = verb.Participiu;
            verApi.InfinitivLung = verb.InfinitivLung;
            verApi.ImperativSingular = verb.ImperativSingular;
            verApi.ImperativPlural = verb.ImperativPlural;

            verApi.TimpuriVerbale = new List<TimpVerbalApi>();

            foreach (var timpuriVerbal in verb.TimpuriVerbales)
            {
                var timpVerbalApi = new TimpVerbalApi();
                timpVerbalApi.TimpVerbal = EnumConververt.TimpVerbalToString((TimpEnum) timpuriVerbal.TimpID);
                timpVerbalApi.Eu = timpuriVerbal.Eu;
                timpVerbalApi.Tu = timpuriVerbal.Tu;
                timpVerbalApi.Ea = timpuriVerbal.Ea;
                timpVerbalApi.Noi = timpuriVerbal.Noi;
                timpVerbalApi.Voi = timpuriVerbal.Voi;
                timpVerbalApi.Ele = timpuriVerbal.Ele;

                verApi.TimpuriVerbale.Add(timpVerbalApi);
            }

            return verApi;

        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
