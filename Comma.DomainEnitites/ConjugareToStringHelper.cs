using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comma.DomainEnitites
{
    public class ConjugareToStringHelper
    {
        private PersoanaEnum _persoana;
        private NumarEnum _numar;
        private TimpEnum _timp;
        private ModEnum _mod;

        public ConjugareToStringHelper(ModEnum mod, PersoanaEnum persoana, NumarEnum numar, TimpEnum timp)
        {
            _persoana = persoana;
            _numar = numar;
            _timp = timp;
            _mod = mod;
        }


        public string GetPronoun()
        {
            return ApplyComputationOnNumberAndPerson(
                 () => "Eu"
                , () => "Tu"
                , () => "El/Ea"
                , () => "Noi"
                , () => "Voi"
                , () => "Ei/Ele");
        }

        public string GetAuxiliaryVerb()
        {
            if (_mod == ModEnum.Indicativ && _timp == TimpEnum.Viitor)
            {
                return GetViitorAuxiliaryVerb();
            }
            else if (_mod == ModEnum.Indicativ && _timp == TimpEnum.ViitorAnterior)
            {
                return GetViitorAnteriorAuxiliaryVerb();
            }
            else if (_mod == ModEnum.Indicativ && _timp == TimpEnum.PerfectCompus)
            {
                return GetPerfectCompusAuxiliaryVerb();
            }
            else if (_mod == ModEnum.CondiționalOptativ && _timp == TimpEnum.Prezent)
            {
                return GetConditionalPrezentAuxiliaryVerb();
            }
            else if (_mod == ModEnum.CondiționalOptativ && _timp == TimpEnum.Perfect)
            {
                return GetConditionalPerfectAuxiliaryVerb();
            }
            else if (_mod == ModEnum.Conjunctiv && _timp == TimpEnum.Prezent)
            {
                return GetConjunctivAuxiliaryVerb();
            }
            else if (_mod == ModEnum.Conjunctiv && _timp == TimpEnum.Perfect)
            {
                return GetConjunctivPerfectAuxiliaryVerb();
            }
            else if (_mod == ModEnum.Supin && _timp == TimpEnum.Prezent)
            {
                return GetSupinParticipiu();
            }
            else if(_mod == ModEnum.Infinitiv && _timp == TimpEnum.Prezent)
            {
                return GetInfinitivePrefix();
            }
            else
            {
                return string.Empty;
            }
        }

        private string GetInfinitivePrefix()
        {
            return "a";
        }

        private string GetSupinParticipiu()
        {
            return "de";
        }

        private string GetViitorAuxiliaryVerb()
        {
            return ApplyComputationOnNumberAndPerson(
                 () => "voi"
                , () => "vei"
                , () => "va"
                , () => "vom"
                , () => "veti"
                , () => "vor");
        }

        private string GetViitorAnteriorAuxiliaryVerb()
        {
            return ApplyComputationOnNumberAndPerson(
                 () => "voi fi"
                , () => "vei fi"
                , () => "va fi"
                , () => "vom fi"
                , () => "veti fi"
                , () => "vor fi");
        }

        private string GetConditionalPrezentAuxiliaryVerb()
        {
            return ApplyComputationOnNumberAndPerson(
                 () => "as"
                , () => "ai"
                , () => "ar"
                , () => "am"
                , () => "ati"
                , () => "ar");
        }

        private string GetConditionalPerfectAuxiliaryVerb()
        {
            return ApplyComputationOnNumberAndPerson(
                 () => "as fi"
                , () => "ai fi"
                , () => "ar fi"
                , () => "am fi"
                , () => "ati fi"
                , () => "ar fi");
        }

        private string GetPerfectCompusAuxiliaryVerb()
        {
            return ApplyComputationOnNumberAndPerson(
                 () => "am"
                , () => "ai"
                , () => "a"
                , () => "am"
                , () => "ati"
                , () => "au");
        }

        private string GetConjunctivAuxiliaryVerb()
        {
            return "sa";
        }

        private string GetConjunctivPerfectAuxiliaryVerb()
        {
            return "sa fi";
        }

        private string ApplyComputationOnNumberAndPerson(
                       Func<string> intaiSingular,
                       Func<string> adouaSingular,
                       Func<string> atreiaSingular,
                       Func<string> intaiPlural,
                       Func<string> adouaPlural,
                       Func<string> atreiaPlural)
        {
            if (_numar == NumarEnum.Singular && _persoana == PersoanaEnum.Intai)
            {
                return intaiSingular();
            }
            else if (_numar == NumarEnum.Singular && _persoana == PersoanaEnum.Adoua)
            {
                return adouaSingular();
            }
            else if (_numar == NumarEnum.Singular && _persoana == PersoanaEnum.ATreia)
            {
                return atreiaSingular();
            }
            else if (_numar == NumarEnum.Plural && _persoana == PersoanaEnum.Intai)
            {
                return intaiPlural();
            }
            else if (_numar == NumarEnum.Plural && _persoana == PersoanaEnum.Adoua)
            {
                return adouaPlural();
            }
            else if (_numar == NumarEnum.Plural && _persoana == PersoanaEnum.ATreia)
            {
                return atreiaPlural();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
