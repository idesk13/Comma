using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using Comma.DomainEnitites;
using Comma.External;

namespace Comma.Repository
{
    public class VerbRepository : BaseRepository
    {
        public void AddIntoContext(Verb verb)
        {
            WordsContext.Verbs.AddOrUpdate(verb);
        }

        public List<Verb> GetAllVerbs()
        {
            return WordsContext.Verbs.ToList();
        }

        public Verb GetVerbById(int verbId)
        {
            return WordsContext.Verbs.FirstOrDefault(x => x.ID == verbId);
        }

        public void Commit()
        {
            WordsContext.SaveChanges();
        }

        public Verb GetByName(string verb)
        {
            return WordsContext.Verbs.FirstOrDefault(x => x.RawVerb == verb);
        }

        public Verb GetByName(string verb, bool includeTimpuri)
        {
            var vb = WordsContext.Verbs.FirstOrDefault(x => x.RawVerb == verb);

            if (includeTimpuri)
            {
                if (vb != null)
                {
                    var timpuriVerble = GetTimpuriByVerbId(vb.ID);

                    if (timpuriVerble == null || timpuriVerble.Count == 0)
                    {
                        UpdateTimpuriVerbale(vb);
                    }
                }

            }

            return vb;

        }

        public void UpdateTimpuriVerbale(Verb vb)
        {
            ExternalProcesor externalProcesor = new ExternalProcesor();

            var timpuriTimpuriRegulate = externalProcesor.TimpVerbalComplet(vb.OriginalVerb);
            foreach (var timpRegulat in timpuriTimpuriRegulate.TimpuriRegulate)
            {
                TimpuriVerbale timpuriVerbale = new TimpuriVerbale()
                {
                    Eu = timpRegulat.Eu,
                    Tu = timpRegulat.Tu,
                    VerbID = vb.ID,
                    Ea = timpRegulat.Ea,
                    Ele = timpRegulat.Ele,
                    Noi = timpRegulat.Noi,
                    Voi = timpRegulat.Voi,
                    TimpID = GetTimpId(timpRegulat.Timp)
                };

                WordsContext.TimpuriVerbales.AddOrUpdate(timpuriVerbale);
                vb.TimpuriVerbales.Add(timpuriVerbale);
            }

            this.Commit();
            
        }

        private int? GetTimpId(string timpRegulatTimp)
        {
            if (timpRegulatTimp == "Prezent")
            {
                return (int?) TimpEnum.Prezent;
            }

            if (timpRegulatTimp == "Conjunctiv Prezent")
            {
                return (int?)TimpEnum.ConjuctivPrezent;
            }

            if (timpRegulatTimp == "Imperfect")
            {
                return (int?) TimpEnum.Imperfect;
            }

            if (timpRegulatTimp == "Perfect Simplu")
            {
                return (int?) TimpEnum.PerfectSimplu;
            }

            if (timpRegulatTimp == "Mai Mult Ca Perfectul")
            {
                return (int?) TimpEnum.MaiMultCaPerfect;
            }

            if (timpRegulatTimp == "Conjunctiv Perfect")
            {
                return (int?) TimpEnum.ConjuctivPerfect;
            }

            if (timpRegulatTimp == "Condițional Prezent")
            {
                return (int?) TimpEnum.ConditionalPrezent;
            }

            if (timpRegulatTimp == "Condițional Perfect")
            {
                return (int?)TimpEnum.CondiționalPerfect;
            }

            if (timpRegulatTimp == "Perfectul Compus")
            {
                return (int?) TimpEnum.PerfectCompus;
            }

            if (timpRegulatTimp == "Perfect Compus")
            {
                return (int?)TimpEnum.PerfectCompus;
            }

            if (timpRegulatTimp == "Viitor Anterior")
            {
                return (int?)TimpEnum.ViitorAnterior;
            }

            if (timpRegulatTimp == "Viitor")
            {
                return (int?)TimpEnum.Viitor;
            }

            return null;
        }

        public List<TimpuriVerbale> GetTimpuriByVerbId(int verbEntityId)
        {
            var vbs = WordsContext.TimpuriVerbales.Where(x => x.VerbID == verbEntityId).ToList();

            return vbs;
        }

        public void DeletVerb(int customer)
        {
            var verb = GetVerbById(customer);
            WordsContext.Verbs.Remove(verb);
            Commit();
        }
    }
}