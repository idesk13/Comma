using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comma.DomainEnitites
{
    public class Conjugare
    {
        private ConjugareToStringHelper _stringHelper;

        public ModEnum Mod { get; set; }
        public NumarEnum Numar { get; set; }
        public PersoanaEnum? Persoana { get; set; }
        public TimpEnum Timp { get; set; }
        public Verb Verb { get; set; }
        
        private Conjugare() { }

        public static Conjugare MakeIndicativPrezent(PersoanaEnum persoana, NumarEnum numar, Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Indicativ,
                Numar = numar,
                Persoana = persoana,
                Timp = TimpEnum.Prezent,
                Verb = verb
            };
        }

        public static Conjugare MakeIndicativImperfect(PersoanaEnum persoana, NumarEnum numar, Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Indicativ,
                Numar = numar,
                Persoana = persoana,
                Timp = TimpEnum.Imperfect,
                Verb = verb
            };
        }

        public static Conjugare MakeIndicativPerfectCompus(PersoanaEnum persoana, NumarEnum numar, Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Indicativ,
                Numar = numar,
                Persoana = persoana,
                Timp = TimpEnum.PerfectCompus,
                Verb = verb
            };
        }

        public static Conjugare MakeIndicativMaiMultCaPerfect(PersoanaEnum persoana, NumarEnum numar, Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Indicativ,
                Numar = numar,
                Persoana = persoana,
                Timp = TimpEnum.MaiMultCaPerfect,
                Verb = verb
            };
        }

        public static Conjugare MakeIndicativPerfectSimplu(PersoanaEnum persoana, NumarEnum numar, Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Indicativ,
                Numar = numar,
                Persoana = persoana,
                Timp = TimpEnum.PerfectSimplu,
                Verb = verb
            };
        }

        public static Conjugare MakeIndicativViitor(PersoanaEnum persoana, NumarEnum numar, Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Indicativ,
                Numar = numar,
                Persoana = persoana,
                Timp = TimpEnum.Viitor,
                Verb = verb
            };
        }

        public static Conjugare MakeIndicativViitorAnterior(PersoanaEnum persoana, NumarEnum numar, Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Indicativ,
                Numar = numar,
                Persoana = persoana,
                Timp = TimpEnum.ViitorAnterior,
                Verb = verb
            };
        }

        public static Conjugare MakeConjunctivPrezent(PersoanaEnum persoana, NumarEnum numar, Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Conjunctiv,
                Numar = numar,
                Persoana = persoana,
                Timp = TimpEnum.Prezent,
                Verb = verb
            };
        }

        public static Conjugare MakeConjunctivPerfect(PersoanaEnum persoana, NumarEnum numar, Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Conjunctiv,
                Numar = numar,
                Persoana = persoana,
                Timp = TimpEnum.Perfect,
                Verb = verb
            };
        }

        public static Conjugare MakeConditionalPrezent(PersoanaEnum persoana, NumarEnum numar, Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.CondiționalOptativ,
                Numar = numar,
                Persoana = persoana,
                Timp = TimpEnum.Prezent,
                Verb = verb
            };
        }

        public static Conjugare MakeConditionalPerfect(PersoanaEnum persoana, NumarEnum numar, Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.CondiționalOptativ,
                Numar = numar,
                Persoana = persoana,
                Timp = TimpEnum.Perfect,
                Verb = verb
            };
        }

        public static Conjugare MakeImperativ(NumarEnum numar, Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Imperativ,
                Numar = numar,
                Persoana = PersoanaEnum.Adoua,
                Timp = TimpEnum.Prezent,
                Verb = verb
            };
        }

        public static Conjugare MakeInfinitiv(Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Infinitiv,
                Numar = NumarEnum.Undefined,
                Persoana = PersoanaEnum.Undefined,
                Timp = TimpEnum.Prezent,
                Verb = verb
            };
        }
        public static Conjugare MakeSupin(Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Supin,
                Numar = NumarEnum.Undefined,
                Persoana = PersoanaEnum.Undefined,
                Timp = TimpEnum.Undefined,
                Verb = verb
            };
        }
        public static Conjugare MakeParticipiu(Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Participiu,
                Numar = NumarEnum.Undefined,
                Persoana = PersoanaEnum.Undefined,
                Timp = TimpEnum.Undefined,
                Verb = verb
            };
        }
        public static Conjugare MakeGerunziu(Verb verb)
        {
            return new Conjugare
            {
                Mod = ModEnum.Gerunziu,
                Numar = NumarEnum.Undefined,
                Persoana = PersoanaEnum.Undefined,
                Timp = TimpEnum.Undefined,
                Verb = verb
            };
        }


        public override string ToString()
        {
            var stringHelper = new ConjugareToStringHelper(Mod,Persoana.Value, Numar, Timp);
            var pronume = stringHelper.GetPronoun();
            var auxiliar = stringHelper.GetAuxiliaryVerb();

            if(string.IsNullOrEmpty(pronume) && string.IsNullOrEmpty(auxiliar))
            {
                return Verb.ToString();
            }
            else if(string.IsNullOrEmpty(pronume) && !string.IsNullOrEmpty(auxiliar))
            {
                return $"{auxiliar} {Verb.ToString()}";
            }
            else if (string.IsNullOrEmpty(auxiliar))
            {
                return $"{pronume} {Verb.ToString()}";
            }
            else
            {
                return $"{pronume} {auxiliar} {Verb.ToString()}";
            }

        }
    }
}
