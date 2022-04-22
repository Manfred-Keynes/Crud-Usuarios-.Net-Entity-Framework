using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC__curso.Controllers
{
    public class CerrarController : Controller
    {
        public ActionResult Logoff()
        {
            Session["User"] = null;
            //funcion 
            return RedirectToAction("Index", "Access");
        }
    }
}
