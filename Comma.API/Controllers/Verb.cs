using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;

namespace Comma.API.Controllers
{
    [Serializable]
    [DataContract]
    public class VerbApi
    {
        [DataMember]
        public string Participiu { get; set; }
        [DataMember]
        public string Gerunziu { get; set; }
        [DataMember]
        public string Infinitiv { get; set; }
        [DataMember]
        public string InfinitivLung { get; set; }
        [DataMember]
        public string ImperativSingular { get; set; }
        [DataMember]
        public string ImperativPlural { get; set; }
        [DataMember]
        public List<TimpVerbalApi> TimpuriVerbale { get; set; }
    }

    [Serializable]
    [DataContract]
    public class TimpVerbalApi
    {
        [DataMember]
        public string TimpVerbal { get; set; }
        [DataMember(Name = "Eu")]
        public string Eu { get; set; }

        [DataMember(Name = "Tu")]
        public string Tu { get; set; }
        [DataMember(Name = "Ea")]
        public string Ea { get; set; }
        [DataMember(Name = "Noi")]
        public string Noi { get; set; }
        [DataMember(Name = "Voi")]
        public string Voi { get; set; }
        [DataMember(Name = "Ele")]
        public string Ele { get; set; }
    }
}