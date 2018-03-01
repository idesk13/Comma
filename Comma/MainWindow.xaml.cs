using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Data.SQLite;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Comma.External;
using Comma.Repository;

namespace Comma
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SQLiteConnection sqlite;


        private readonly VerbRepository _verbRepository;
        public MainWindow()
        {
            InitializeComponent();
            sqlite = new SQLiteConnection(@"DataSource = C:\Program Files (x86)\Octavian Rasnita\Maestro DEX 3\dexDb.sqlite;Version=3;");
            ExternalProcesor externalProcesor = new ExternalProcesor();
            externalProcesor.GetIndicativPrezent("scrie");
            var words = SelectQuery("Select  * from definition where definition like '%#vb.#%'");
            var list = new List<Word>();
            int emptyVerbs = 0;
            int NotemptyVerbs = 0;


            _verbRepository = new VerbRepository();

            var allVerbs = _verbRepository.GetAllVerbs();

            foreach (var verb in allVerbs)
            {

                var word = new Word()
                {
                    ID = verb.ID,
                    Verb = verb.RawVerb,
                    Original = verb.OriginalVerb
                };

                list.Add(word);
            }


            var listdd = list.OrderBy(x => x.Verb).Distinct().ToList();

            listdd = (
                from o in listdd
                orderby o.Verb
                group o by o.Verb into g
                select g.First()
            ).ToList();

            DgWords.ItemsSource = listdd;
            this.DataContext = new MainModel()
            {
                EmptyWords = emptyVerbs + "-------" + listdd.Count

            };

        }

        public static void DistinctValues<T>(List<T> list)
        {
            list.Sort();

            int src = 0;
            int dst = 0;
            while (src < list.Count)
            {
                var val = list[src];
                list[dst] = val;

                ++dst;
                while (++src < list.Count && list[src].Equals(val)) ;
            }
            if (dst < list.Count)
            {
                list.RemoveRange(dst, list.Count - dst);
            }
        }

        public string RemoveCharctaer(string input)
        {

            var charsToRemove = new string[] { "@", ",", "^", "1", "2", "3", "!", "*", " " };
            foreach (var c in charsToRemove)
            {
                input = input.Replace(c, string.Empty);
            }

            return input;
        }

        private async void PRocess(Word word)
        {
            var verb = new Verb
            {
                OriginalID = word.ID,
                OriginalVerb = word.Verb,
                RawVerb = word.Original
            };
            _verbRepository.AddIntoContext(verb);

            _verbRepository.Commit();

        }

        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        public string GetVerb(string input)
        {
            Regex regex = new Regex($@"@([a-zA-Z() ó\-À-ÿȚ*ȘĂéấ!ăắẮIẤÎ́îíșț^1-9\s,~{{}}])*@");
            Match match = regex.Match(input);

            if (match.Success)
            {
                return match.Groups[0].Value.Remove(0, 1);

            }

            return string.Empty;
        }

        public class Word : IComparable<Word>
        {
            public int ID { get; set; }
            public string Definition { get; set; }
            public string Verb { get; set; }
            public string Original { get; internal set; }
            public bool NotSaved { get; set; }

            protected bool Equals(Word other)
            {
                return string.Equals(Verb, other.Verb);
            }


            public int CompareTo(Word other)
            {
                if (ReferenceEquals(this, other)) return 0;
                if (ReferenceEquals(null, other)) return 1;
                return string.Compare(Verb, other.Verb, StringComparison.Ordinal);
            }
        }


        public DataTable SelectQuery(string query)
        {
            SQLiteDataAdapter ad;
            DataTable dt = new DataTable();

            try
            {
                SQLiteCommand cmd;
                sqlite.Open();  //Initiate connection to the db
                cmd = sqlite.CreateCommand();
                cmd.CommandText = query;  //set the passed query
                ad = new SQLiteDataAdapter(cmd);
                ad.Fill(dt); //fill the datasource
            }
            catch (SQLiteException ex)
            {
                MessageBox.Show(ex.Message);
                //Add your exception code here.
            }
            sqlite.Close();
            return dt;
        }


        private void SyncButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            Word customer = (Word)DgWords.SelectedItem;
            PRocess(customer);

            MessageBox.Show($"{customer.Verb} saved into DB");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Word customer = (Word)DgWords.SelectedItem;
            Edit editVerb = new Edit(customer.Verb);
            editVerb.Show();
        }
    }

    public class MainModel
    {
        private const string emptyWords = "Hello World";
        public string EmptyWords { get; set; }
    }
}
