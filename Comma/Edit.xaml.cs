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

            ExternalProcesor externalProcesor = new ExternalProcesor();
            var timpuri = externalProcesor.TimpVerbalComplet(verb);
            this.DataContext = timpuri;
            DgWords.ItemsSource = timpuri.TimpuriRegulate;
        }
    }
}
