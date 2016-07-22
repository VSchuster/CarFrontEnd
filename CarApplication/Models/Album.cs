using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace CarApplication.Models
{
    class Album
    {
        int id { get; set; }
        string name { get; set; }
        int date { get; set; }
        BitmapImage cover { get; set; }
    }
}
