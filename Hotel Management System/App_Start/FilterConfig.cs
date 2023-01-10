using System.Web;
using System.Web.Mvc;
using Hotel_Management_System.Controllers;

namespace Hotel_Management_System
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
