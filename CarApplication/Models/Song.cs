using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarApplication.Models
{
    class Song
    {
        int id { get; set; }
        string name { get; set; }
        Artist artist { get; set; }
        Album album { get; set; }
    }
}
