using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comma.Repository
{
    public class BaseRepository
    {
        protected Under33_ProdEntities WordsContext { get; }

        public BaseRepository()
        {
            WordsContext = new Under33_ProdEntities();
        }
    }
}
