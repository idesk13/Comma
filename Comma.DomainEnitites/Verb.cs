using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comma.DomainEnitites
{
    public class Verb
    {
        public string Radacina { get; set; }
        public string Terminatie { get; set; }

        public override string ToString()
        {
            return Radacina + Terminatie;
        }
    }
}
