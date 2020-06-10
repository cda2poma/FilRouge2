using BO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=234238

namespace FilRouge2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class OffrePage : Page
    {
        private OffreVM vm = new OffreVM();
        
        public OffrePage()
        {
            this.InitializeComponent();
            DataContext = vm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        { Frame.Navigate(typeof(ListOffresPage)); }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vm.DeletedOffre_Event += DeletedOffre_Event;
            vm.EditedOffre_Event += EditedOffre_Event;
            vm.Suscribe();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            vm.DeletedOffre_Event -= DeletedOffre_Event;
            vm.EditedOffre_Event -= EditedOffre_Event;
            vm.Unsuscribe();
        }

        private async void DeletedOffre_Event(object sender, EventArgs e)
        {
            await new ContentDialog()
            {
                Title = "Offre supprimée",
                Content = "L'offre que vous étiez en train de consulter a été supprimée.",
                CloseButtonText = "Ok"
            }.ShowAsync();
            Frame.Navigate(typeof(ListOffresPage));
        }

        private async void EditedOffre_Event(object sender, Offre e)
        {
            await new ContentDialog()
            {
                Title = "Offre modifiée",
                Content = FilterDataM.Instance.OffreMatchesFilter(e) ?
                "L'offre que vous étiez en train de consulter a été modifiée." :
                "L'offre que vous étiez en train de consulter a été modifiée et ne correspond plus à vos critères de recherche.",
                CloseButtonText = "Ok"
            }.ShowAsync();
        }
    }
}
