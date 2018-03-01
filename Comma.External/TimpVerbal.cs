namespace Comma.External
{
    public class TimpVerbal
    {
        public TimpVerbal(string timpVerbalNume)
        {
            this.Timp = timpVerbalNume;
        }

        public string Timp { get; set; }
        public string EuPronume { get; set; }
        public string Eu { get; set; }
        public string EuComplete => EuPronume + " " + Eu;


        public string TuPronume { get; set; }
        public string Tu { get; set; }
        public string TuComplete => TuPronume + " " + Tu;
        public string EaPronume { get; set; }
        public string Ea { get; set; }
        public string EaComplete => EaPronume + " " + Ea;
        public string NoiPronume { get; set; }
        public string Noi { get; set; }
        public string NoiComplete => NoiPronume + " " + Noi;
        public string VoiPronume { get; set; }
        public string Voi { get; set; }
        public string VoiComplete => VoiPronume + " " + Voi;
        public string ElePronume { get; set; }
        public string Ele { get; set; }
        public string EleComplete => ElePronume + " " + Ele;
    }
}