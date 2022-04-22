using System.Web;
using System.Web.Mvc;

namespace MVC__curso
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            //llamado del filtro
            filters.Add(new Filters.VerifySession());
        }
    }
}
