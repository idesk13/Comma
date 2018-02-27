using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;

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

        public void Commit()
        {
            WordsContext.SaveChanges();
        }
    }
}