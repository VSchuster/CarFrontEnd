using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace CarApplication.Models
{
    class Artist
    {
        public int id { get; set; }
        public BitmapImage photo { get; set; }
        public string name { get; set; }
    }
}
