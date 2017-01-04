using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace CarApplication.Models
{
    public class Album
    {
        public string name { get; set; }
        public BitmapImage cover { get; set; }
        public Windows.Storage.StorageFolder folder { get; set; }
    }
}
