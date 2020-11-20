using System.Web;
using System.Web.Mvc;

namespace Appel_Leaflet_et_Conversion
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
