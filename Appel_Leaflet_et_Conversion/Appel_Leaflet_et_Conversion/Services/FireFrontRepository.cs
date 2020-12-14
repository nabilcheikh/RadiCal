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
                // Décodage de listePointsJSON
                fireFront = ff;
                string[] temp = fireFront.listePointsJSON.Split('{','}');
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].Length > 2)
                    {
                        string[] coords = temp[i].Split(',');
                        string latitude = coords[0].Substring(6).Replace('.', ',');
                        string longitude = coords[1].Substring(6).Replace('.', ',');
                        string altitude = coords[2].Substring(6).Replace('.', ',');
                        Point p = new Point();
                        p.lat = double.Parse(latitude);
                        p.lng = double.Parse(longitude);
                        p.alt = double.Parse(altitude);
                        fireFront.listePoints.Add(p);
                    }
                }
                // Décodage de rectangleJSON
                temp = fireFront.rectangleJSON.Split('{', '}');
                for (int i = 0; i < temp.Length; i++)
                {
                    if (temp[i].Length > 2)
                    {
                        string[] coords = temp[i].Split(',');
                        string latitude = coords[0].Substring(6).Replace('.', ',');
                        string longitude = coords[1].Substring(6).Replace('.', ',');
                        string altitude = coords[2].Substring(6).Replace('.', ',');
                        Point p = new Point();
                        p.lat = double.Parse(latitude);
                        p.lng = double.Parse(longitude);
                        p.alt = double.Parse(altitude);
                        fireFront.rectangle.Add(p);
                    }
                }
                // Conversion des coordonnées latitude et longitude (WGS84) des points du front de feu en coordonnées X et Y (Lambert93)
                foreach (Point p in fireFront.listePoints)
                {
                    WGS84ToLambert93(p);
                }
                // Conversion des coordonnées latitude et longitude (WGS84) des points du rectangle en coordonnées X et Y (Lambert93)
                foreach (Point p in fireFront.rectangle)
                {
                    WGS84ToLambert93(p);
                }
                // Insertion dans une BDD
                return true;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        // Conversion latitude,longitude en coordonée lambert 93
        public bool WGS84ToLambert93(Point point)
        {
            // Algo détaillé ici : https://geodesie.ign.fr/contenu/fichiers/documentation/algorithmes/notice/NTG_71.pdf
            try
            {
                //variables:
                double a = 6378137; //demi grand axe de l'ellipsoide (m)
                double e = 0.08181919106; //première excentricité de l'ellipsoide
                double l0 = (Math.PI / 180) * 3;
                double lc = l0;
                double phi0 = (Math.PI / 180) * 46.5; //latitude d'origine en radian
                double phi1 = (Math.PI / 180) * 44; //1er parallele automécoïque
                double phi2 = (Math.PI / 180) * 49; //2eme parallele automécoïque

                double x0 = 700000; //coordonnées à l'origine
                double y0 = 6600000; //coordonnées à l'origine

                double phi = (Math.PI / 180) * point.lat;
                double l = (Math.PI / 180) * point.lng;

                //calcul des grandes normales
                double gN1 = a / Math.Sqrt(1 - e * e * Math.Sin(phi1) * Math.Sin(phi1));
                double gN2 = a / Math.Sqrt(1 - e * e * Math.Sin(phi2) * Math.Sin(phi2));

                //calculs des latitudes isométriques
                double gl1 = Math.Log(Math.Tan(Math.PI / 4 + phi1 / 2) * Math.Pow((1 - e * Math.Sin(phi1)) / (1 + e * Math.Sin(phi1)), e / 2));
                double gl2 = Math.Log(Math.Tan(Math.PI / 4 + phi2 / 2) * Math.Pow((1 - e * Math.Sin(phi2)) / (1 + e * Math.Sin(phi2)), e / 2));
                double gl0 = Math.Log(Math.Tan(Math.PI / 4 + phi0 / 2) * Math.Pow((1 - e * Math.Sin(phi0)) / (1 + e * Math.Sin(phi0)), e / 2));
                double gl = Math.Log(Math.Tan(Math.PI / 4 + phi / 2) * Math.Pow((1 - e * Math.Sin(phi)) / (1 + e * Math.Sin(phi)), e / 2));

                //calcul de l'exposant de la projection
                double n = (Math.Log((gN2 * Math.Cos(phi2)) / (gN1 * Math.Cos(phi1)))) / (gl1 - gl2);

                //calcul de la constante de projection
                double c = ((gN1 * Math.Cos(phi1)) / n) * Math.Exp(n * gl1);

                //calcul des coordonnées
                double ys = y0 + c * Math.Exp(-1 * n * gl0);
                double x93 = x0 + c * Math.Exp(-1 * n * gl) * Math.Sin(n * (l - lc));
                double y93 = ys - c * Math.Exp(-1 * n * gl) * Math.Cos(n * (l - lc));

                point.X = x93;
                point.Y = y93;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }

        }
    }
}