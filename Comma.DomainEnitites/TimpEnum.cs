using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comma.DomainEnitites
{
    public enum TimpEnum
    {
        Prezent = 0,
        ConjuctivPrezent,
        Imperfect,
        PerfectSimplu,
        MaiMultCaPerfect,
        ConjuctivPerfect,
        ConditionalPrezent,
        CondiționalPerfect,
        PerfectCompus,
        Viitor,
        ViitorAnterior,
        Perfect,
        Undefined,
    }

    public class EnumConververt
    {
        public static string TimpVerbalToString(TimpEnum timpEnum)
        {
            switch (timpEnum)
            {
                case TimpEnum.Prezent:
                {
                    return "Prezent";
                }

                case TimpEnum.ConditionalPrezent:
                {
                    return "Conditional Prezent";
                }

                case TimpEnum.ConjuctivPrezent:
                {
                    return "Conjuctiv Prezent";
                }

                case TimpEnum.ConjuctivPerfect:
                {
                    return "Conjuctiv Perfect";
                }

                case TimpEnum.CondiționalPerfect:
                {
                    return "ConjuctivPerfect";
                }

                case TimpEnum.Imperfect:
                {
                    return "Imperfect";
                }

                case TimpEnum.MaiMultCaPerfect:
                {
                    return "MaiMultCaPerfect";
                }

                case TimpEnum.Perfect:
                {
                    return "Perfect";
                }

                case TimpEnum.PerfectCompus:
                {
                    return "PerfectCompus";
                }

                case TimpEnum.PerfectSimplu:
                {
                    return "PerfectSimplu";
                }

                case TimpEnum.Viitor:
                {
                    return "Viitor";
                }

                case TimpEnum.ViitorAnterior:
                {
                    return "ViitorAnterior";
                }


                default: return "Timp verbal necunoscut";
            }
         }
    }
}
