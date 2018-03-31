using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Migrations;
using System.Data.SQLite;
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

        public List<Verb> GetAllVerbsWithoutTimpuri()
        {
            return WordsContext.Verbs.Where(x=>x.AreTimpuri==false).ToList();
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
            var vb = WordsContext.Verbs.FirstOrDefault(x => x.OriginalVerb == verb);

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
            if (vb.AreTimpuri != null && vb.AreTimpuri.Value) return;

            ExternalProcesor externalProcesor = new ExternalProcesor();

            var timpuriTimpuriRegulate = externalProcesor.TimpVerbalComplet(vb.OriginalVerb);

            if (timpuriTimpuriRegulate==null) return;

            vb.Infinitiv = timpuriTimpuriRegulate.Infinitiv;
            vb.InfinitivLung = timpuriTimpuriRegulate.InfinitivLung;
            vb.Gerunziu = timpuriTimpuriRegulate.Gerunziu;
            vb.Participiu = timpuriTimpuriRegulate.Participiu;
            vb.ImperativPlural = timpuriTimpuriRegulate.ImperativPlural;
            vb.ImperativSingular = timpuriTimpuriRegulate.ImperativSingular;
            vb.AreTimpuri = true;

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

                //WordsContext.TimpuriVerbales.AddOrUpdate(timpuriVerbale);
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

        public IEnumerable<Verb> FilterVerbs(string searchTbText)
        {
            return WordsContext.Verbs.Where(x => x.OriginalVerb.Contains(searchTbText));
        }
    }

    public class DexOnlineRepository
    {
        private readonly SQLiteConnection _sqlite;
        public static string VerbsQuery = "Select* from definition where definition like '%#vb.#%'";
        public static string SqlLiteLocation = @"C:\Program Files (x86)\Octavian Rasnita\Maestro DEX 3\dexDb.sqlite";

        public DexOnlineRepository()
        {
            _sqlite = new SQLiteConnection($"DataSource = {SqlLiteLocation} ;Version=3;");
        }

        public DataTable ExecuteQuery(string query)
        {
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                _sqlite.Open();  //Initiate connection to the db
                cmd = _sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                var ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                //Add your exception code here.
            }
            _sqlite.Close();
            return dt;
        }
    }
}