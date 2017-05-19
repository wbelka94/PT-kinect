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
            HandChooser.SelectedIndex = 1;
        }

        void onClickBackButton(object sender, RoutedEventArgs e) {
            this.Frame.Navigate(typeof(MainPage), null);
        }

        async void onClickSaveButton(object sender, RoutedEventArgs e) {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("settings.conf", Windows.Storage.CreationCollisionOption.OpenIfExists);
            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, HandChooser.SelectedValue.ToString()+"\n"
                                                                    + tb.Text+"\n");
            tb.Text = sampleFile.Path.ToString();
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
