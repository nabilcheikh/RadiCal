using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appel_Leaflet_et_Conversion.Models
{
    public class Point
    {
        public double lat { get; set; }

        public double X { get; set; }

        public double lng { get; set; }

        public double Y { get; set; }

        public double alt { get; set; }

        public Point()
        {
            lat = 0;
            lng = 0;
            alt = 0;
            X = 0;
            Y = 0;
        }
    }
}