using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC__curso.Controllers
{
    public class AnimalController : Controller
    {
        // GET: Animal
        public ActionResult Index()
        {
            List<SelectListItem> lst = new List<SelectListItem>();

            //conexion a la bd
            using (Models.cursomvcEntities db = new Models.cursomvcEntities())
            {
                //consulta con Linq donde d es el alias
                lst = (from d in db.animal_class
                       select new SelectListItem
                       {
                           //valor que hace el match
                           //elemto value del select en html no sera visible
                           Value = d.id.ToString(),
                           //elemento que sera visible en el drop
                           Text = d.name
                       }).ToList();
            }
            return View(lst);
        }
        [HttpGet]
        public JsonResult Animal(int IdAnimalClass)
        {
            //contexto entity framework
            List<ElementJsonIntKey> lst = new List<ElementJsonIntKey>();
            
            //llenado
            using (Models.cursomvcEntities db = new Models.cursomvcEntities())
            {
                //consulta Linq
                lst = (from d in db.animal
                       where d.idAnimal_class == IdAnimalClass
                       select new ElementJsonIntKey //nombre de la clase con la que se llena la lista
                       {
                           //valor que hace el match
                           Value = d.id,
                           Text = d.name
                       }
                       ).ToList();
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }

        //se realiza de esta manera porque quizas se llenen mas elementos como por ejemplo los grid...
        public class ElementJsonIntKey{
            public int Value { get; set; }// valor que no se visualiza por ser el Id
            public string Text { get; set; }//valor que se visualiza
    }
    }
}