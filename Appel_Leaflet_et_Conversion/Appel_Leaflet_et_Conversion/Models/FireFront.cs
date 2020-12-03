using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appel_Leaflet_et_Conversion.Models
{
    public class FireFront
    {
        //public float lat { get; set; }
        public string listePointsJSON { get; set; }

        public List<Point> listePoints { get; set; }

        public FireFront()
        {
            listePointsJSON = null;
            listePoints = new List<Point>();
        }
    }
}