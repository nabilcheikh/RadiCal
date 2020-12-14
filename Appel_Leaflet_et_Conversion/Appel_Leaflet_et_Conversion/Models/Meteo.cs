using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Appel_Leaflet_et_Conversion.Models
{
    public class Meteo
    {
        public string condition { get; set; }

        public string date { get; set; }

        public string heure { get; set; }

        // humidité en %
        public int humidite { get; set; }

        public float pression { get; set; }

        public int temp { get; set; }

        // Direction du vent divisée en 16 (ex: "SSE" pour sud-sud-est)
        public string ventDirection { get; set; }

        public int ventVitesse { get; set; }

        public int ventVitesseRafales { get; set; }

        public Meteo()
        {
            condition = "";
            date = "";
            heure = "";
            humidite = 0;
            pression = 0;
            temp = 0;
            ventDirection = "";
            ventVitesse = 0;
            ventVitesseRafales = 0;
        }
    }
}