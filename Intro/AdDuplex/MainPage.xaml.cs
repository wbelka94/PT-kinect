﻿using AdDuplex.Interstitials.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AdDuplex
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    /// 
    public class Ad {

    }

    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
            //reklama();
        }

        void onClickButton(object sender, RoutedEventArgs e) {
            
        }

        async void reklama() {
            AdDuplex.InterstitialAd interstitialAd = new AdDuplex.InterstitialAd("206243");
            await interstitialAd.ShowAdAsync();
            interstitialAd.AdClosed += Close;
        }

        async void Close(object sender, InterstitialAdLoadedEventArgs e) {
            var launchUri = new Uri("fbkp:");
            var success = await Launcher.LaunchUriAsync(launchUri);
            CoreApplication.Exit();
        }

        void LaunchWpfApp_Click(object sender, RoutedEventArgs e) {
            reklama();            
        }

        private void tb_SelectionChanged(object sender, RoutedEventArgs e) {

        }
    }
}