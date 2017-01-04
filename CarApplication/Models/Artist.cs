using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace CarApplication.Models
{
    public class Artist
    {
        public BitmapImage photo { get; set; }
        public string name { get; set; }
        public Windows.Storage.StorageFolder folder { get; set; }
    }
}
