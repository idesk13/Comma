using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Text.RegularExpressions;
using Comma.Repository;
using Comma.BusinessLogic;

namespace Comma
{
    public partial class MainWindow
    {

        private readonly VerbRepository _verbRepository;
        private readonly DexOnlineRepository _dexOnlineRepository;

        public MainWindow()
        {
            InitializeComponent();

            _verbRepository = new VerbRepository();
            _dexOnlineRepository = new DexOnlineRepository();

            UpdateGrid();
        }

        private void UpdateGrid(List<Verb> allVerbs = null)
        {
            if (allVerbs == null)
            {
                allVerbs = _verbRepository.GetAllVerbsWithoutTimpuri().OrderBy(x => x.OriginalVerb).ToList();
            }

            var list = new List<Word>();
            
            foreach (var verb in allVerbs)
            {
                _verbRepository.UpdateTimpuriVerbale(verb);
                var word = new Word()
                {
                    ID = verb.ID,
                    Verb = verb.RawVerb,
                    Original = verb.OriginalVerb
                };

                list.Add(word);
            }

            DgWords.ItemsSource = list;
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

        public string GetVerb(string input)
        {
            input.RemoveDiacritis();
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

        private void SyncButton_Click(object sender, RoutedEventArgs e)
        {
            var verbs = _verbRepository.FilterVerbs(SearchTB.Text).OrderBy(x => x.OriginalVerb).ToList();

            UpdateGrid(verbs);
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            Word customer = (Word)DgWords.SelectedItem;
            Edit editVerb = new Edit(customer.Original);
            editVerb.Show();
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            Word customer = (Word)DgWords.SelectedItem;
            VerbRepository vp = new VerbRepository();
            vp.DeletVerb(customer.ID);
            UpdateGrid();
        }

    }
}
