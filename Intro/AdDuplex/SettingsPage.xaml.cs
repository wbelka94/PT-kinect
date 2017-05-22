using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace AdDuplex {
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page {
        public SettingsPage() {
            this.InitializeComponent();
            var _enumval = Enum.GetValues(typeof(HandComboBoxValues)).Cast<HandComboBoxValues>();
            HandChooser.ItemsSource = _enumval.ToList();
            loadFromFile();
            
        }

        async public void loadFromFile() {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("settings.conf");
            IList<string> line = await Windows.Storage.FileIO.ReadLinesAsync(sampleFile);
            if (line.First() == "Prawa")
                HandChooser.SelectedIndex = 1;
            else {
                HandChooser.SelectedIndex = 0;
            }
            tb.Text = line.Last();
        }

        void onClickBackButton(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(MainPage), null);
        }

        void onClickSaveButton(object sender, RoutedEventArgs e) {
            Settings settings = new Settings(HandChooser.SelectedValue.ToString(), tb.Text);
            settings.saveToFile();     
        }

        async void onClickBrowseButton(object sender, RoutedEventArgs e) {
            var picker = new Windows.Storage.Pickers.FolderPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.FileTypeFilter.Add("*");
            Windows.Storage.StorageFolder storageFolder = await picker.PickSingleFolderAsync();

            tb.Text = storageFolder.Path.ToString();
        }

        public enum HandComboBoxValues {
            Lewa,
            Prawa
        }
    }
}
