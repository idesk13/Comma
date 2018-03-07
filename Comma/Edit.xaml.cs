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
            ExternalProcesor externalProcesor = new ExternalProcesor();

            VerbRepository verbRepository = new VerbRepository();
            var verbEntity = verbRepository.GetByName(verb);

            if (string.IsNullOrEmpty(verbEntity.Infinitiv))
            {
                var timpuri = externalProcesor.TimpVerbalComplet(verb);

                verbEntity.Infinitiv = timpuri.Infinitiv;
                verbEntity.Gerunziu = timpuri.Gerunziu;
                verbEntity.Participiu = timpuri.Participiu;
                verbEntity.InfinitivLung = timpuri.InfinitivLung;
                verbEntity.ImperativSingular = timpuri.ImperativSingular;
                verbEntity.ImperativPlural = timpuri.ImperativPlural;

                verbRepository.AddIntoContext(verbEntity);
                verbRepository.UpdateTimpuriVerbale(timpuri.TimpuriRegulate, verbEntity.ID);
               
            }
            this.DataContext = verbEntity;
            DgWords.ItemsSource = verbRepository.GetTimpuriByVerbId(verbEntity.ID);


            verbRepository.Commit();;
            
        }
    }
}
