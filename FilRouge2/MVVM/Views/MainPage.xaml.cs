using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Pour plus d'informations sur le modèle d'élément Page vierge, consultez la page https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FilRouge2
{
    /// <summary>
    /// Une page vide peut être utilisée seule ou constituer une page de destination au sein d'un frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private MainVM vm = new MainVM();
        
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = vm;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            vm.ConnectionState = 1;
            if (await ConnectionDataM.Instance.ConnectAsync("http://user20.2isa.org/api/myhub/"))
            {
                if (await vm.LoadData())
                { 
                    vm.ConnectionState = 0;
                }
            }
            if (vm.ConnectionState != 0)
            {
                IAsyncOperation<ContentDialogResult> ShowContentDialog = new ContentDialog()
                {
                    Title = "Échec de la connexion au serveur",
                    Content = "La connexion au serveur a échoué.",
                    CloseButtonText = "Ok"
                }.ShowAsync();
                vm.ConnectionState = -1;
                await ShowContentDialog;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RadioButton_AllWords.IsChecked = true;
            vm.ResetData();
        }

        private void RadioButton_Checked_ExactExpression(object sender, RoutedEventArgs e)
        { vm.DescConfig = 1; }

        private void RadioButton_Checked_AllWords(object sender, RoutedEventArgs e)
        { vm.DescConfig = 0; }

        private void RadioButton_Checked_AnyWord(object sender, RoutedEventArgs e)
        { vm.DescConfig = -1; }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        { RegionPreferenceM.Instance.Save(vm.SelectedRegion); }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            if (!ConnectionDataM.Instance.ConnectionFailed)
            {
                vm.ConnectionState = 1;
                if (ConnectionDataM.Instance.ConnectionEstablished)
                {
                    if (ConnectionDataM.Instance.ConnectionEstablishedButDataLoadingFailed)
                    {
                        if (await vm.LoadData())
                        {
                            ConnectionDataM.Instance.ConnectionEstablishedButDataLoadingFailed = false;
                            vm.ConnectionState = 0;
                        }
                        else
                        { vm.ConnectionState = -1; }
                    }
                    else
                    {
                        await vm.FilterData();
                        Frame.Navigate(typeof(ListOffresPage));
                        vm.ConnectionState = 0;
                    }
                }
                else
                { 
                    if (await ConnectionDataM.Instance.ConnectAsync("http://user20.2isa.org/api/myhub/"))
                    {
                        if (await vm.LoadData())
                        { vm.ConnectionState = 0; }
                        else
                        { vm.ConnectionState = -1; }
                    }
                }
                if (vm.ConnectionState != 0)
                {
                    ConnectionDataM.Instance.ConnectionFailed = true;
                    IAsyncOperation<ContentDialogResult> ShowContentDialog = new ContentDialog()
                    {
                        Title = "Échec de la connexion au serveur",
                        Content = "La connexion au serveur a échoué.",
                        CloseButtonText = "Ok"
                    }.ShowAsync();
                    vm.ConnectionState = -1;
                    await ShowContentDialog;
                }
                ConnectionDataM.Instance.ConnectionFailed = false;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { vm.UpdateDateTimePickerEnabling(); }
    }
}
