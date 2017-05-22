using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace AdDuplex {
    class Settings {
        private string hand, path;
        public Settings(String hand, String path) {
            this.hand = hand;
            this.path = path;
        }

        async public void saveToFile() {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.CreateFileAsync("settings.conf", Windows.Storage.CreationCollisionOption.OpenIfExists);
            await Windows.Storage.FileIO.WriteTextAsync(sampleFile, this.hand+ "\n"
                                                                    + this.path + "\n");
        }

        async public void loadFromFile() {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync("settings.conf");
            IList<string> line = await Windows.Storage.FileIO.ReadLinesAsync(sampleFile);
            this.hand = line.First();
            this.path = line.Last();
        }

        public String getHand() {
            return this.hand;
        }

        public String getPath() {
            return this.path;
        }

        /* public static void setHand(String hand) {
             this.hand = hand;
         }

         public static void setPath(String path) {
             .path = path;
         }*/
    }
}
