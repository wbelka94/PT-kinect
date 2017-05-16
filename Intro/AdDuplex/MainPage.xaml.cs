using AdDuplex.Interstitials.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
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
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        async void onClickButton(object sender, RoutedEventArgs e) {
            AdDuplex.InterstitialAd interstitialAd = new AdDuplex.InterstitialAd("206243");
            await interstitialAd.ShowAdAsync();
            interstitialAd.AdClosed += Close;
        }

        void Close(object sender, InterstitialAdLoadedEventArgs e) {
            //CoreApplication.Exit();
        }

        async void LaunchWpfApp_Click(object sender, RoutedEventArgs e) {
            var launchUri = new Uri("fbkp:");
            var success = await Launcher.LaunchUriAsync(launchUri);
            if (success) {
                tb.Text = ":)";
            } else {
                tb.Text = ":(";
            }
        }

        private void tb_SelectionChanged(object sender, RoutedEventArgs e) {

        }
    }
}
