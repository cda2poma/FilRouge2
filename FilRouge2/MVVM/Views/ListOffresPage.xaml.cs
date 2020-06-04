﻿using System;
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
    public sealed partial class ListOffresPage : Page
    {
        private ListOffresVM vm = new ListOffresVM();
        
        public ListOffresPage()
        {
            this.InitializeComponent();
            DataContext = vm;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        { vm.UpdateListOffres(); }

        private void ListOffres_SelectionChanged(object sender, SelectionChangedEventArgs e)
        { vm.SetSelectedOffre(); }

        private void Button_Click(object sender, RoutedEventArgs e)
        { Frame.Navigate(typeof(OffrePage)); }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        { Frame.Navigate(typeof(MainPage)); }
    }
}
