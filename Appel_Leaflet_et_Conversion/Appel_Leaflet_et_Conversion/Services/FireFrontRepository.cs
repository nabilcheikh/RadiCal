using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Appel_Leaflet_et_Conversion.Models;

namespace Appel_Leaflet_et_Conversion.Services
{
    public class FireFrontRepository
    {
        private FireFront fireFront;

        public FireFrontRepository()
        {
            fireFront = null;
        }

        public bool SaveFireFront(FireFront ff)
        {
            try
            {
                fireFront = ff;
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}