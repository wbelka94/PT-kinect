using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdDuplex {
    class Settings {
        private String hand, path;
        public Settings(String hand, String path) {
            this.hand = hand;
            this.path = path;
        }

        public void saveToFile() {
            Windows.Storage.StorageFolder storageFolder =  Windows.Storage.ApplicationData.Current.LocalFolder;
        }

        public void loadFromFile() {
            Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
        }
    }
}
