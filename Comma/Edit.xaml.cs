using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Comma.External;
using Comma.Repository;

namespace Comma
{
    /// <summary>
    /// Interaction logic for Edit.xaml
    /// </summary>
    public partial class Edit : Window
    {
        public Edit(string verb )
        {
            InitializeComponent();

            VerbRepository verbRepository = new VerbRepository();
            var verbEntity = verbRepository.GetByName(verb, true);

            this.DataContext = verbEntity;
            DgWords.ItemsSource = verbEntity.TimpuriVerbales;

        }
    }
}
