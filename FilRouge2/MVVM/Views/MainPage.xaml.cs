﻿using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
            try
            { await vm.ConnectAsync(); }
            catch (Exception)
            {
                TryReconnectContentDialog tryReconnectDialog = new TryReconnectContentDialog();
                await tryReconnectDialog.ShowAsync();
                if (tryReconnectDialog.TryReconnect == true)
                { TryReconnect(tryReconnectDialog); }
                else
                {
                    //close
                }
            }
        }

        private async void TryReconnect(TryReconnectContentDialog tryReconnectDialog)
        {
            try
            { await vm.ConnectAsync(); }
            catch (Exception)
            {
                await tryReconnectDialog.ShowAsync();
                if (tryReconnectDialog.TryReconnect == true)
                { TryReconnect(tryReconnectDialog); }
                else
                {
                    //close
                }
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
        {
            //Enregistrement des préférences
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        { vm.FilterData(); }
    }
}
