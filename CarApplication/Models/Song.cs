using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

namespace CarApplication.Models
{
    public class Song
    {
        public string name { get; set; }
        public string artist { get; set; }
        public string album { get; set; }
        public TimeSpan duration { get; set; }
        public uint bitrate { get; set; }
        public BitmapImage albumCover { get; set; }
        public StorageFile Songfile { get; set; }
              
    }
}
