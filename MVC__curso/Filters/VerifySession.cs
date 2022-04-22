using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC__curso.Controllers;
using MVC__curso.Models;

namespace MVC__curso.Filters
{
    public class VerifySession : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            
            //asignar la session
            var  oUser = (user)HttpContext.Current.Session["User"];
            
            
            //si tengo sesion
            if (oUser == null)//si la session esta null
            {
                //                              y el controlador no es Access redirige siempre a Accesess
                if (filterContext.Controller is AccessController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Access/Index");
                }
            }
            //si no tengo sesion
            else // si la session existe 
            {
                //                              y nos queremos dirigir a Access no va redirigir a Home/Index
                if (filterContext.Controller is AccessController == true)
                {
                    filterContext.HttpContext.Response.Redirect("~/Home/Index");
                }
            }

            base.OnActionExecuting(filterContext);
        }
    }
}