using System.Web;
using System.Web.Mvc;

namespace _54_LTUDDN_PhamTuanVu_DHTI15A14HN
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
