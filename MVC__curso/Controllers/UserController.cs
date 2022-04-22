using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC__curso.Models;
using MVC__curso.Models.TableViewModels;
using MVC__curso.Models.ViewModels;

namespace MVC__curso.Controllers
{
    public class UserController : Controller
    {
        // GET: User 
        //----------------Leer---------------------
        public ActionResult Index()
        {
            List<UserTableViewModel> lst = null;
            using (cursomvcEntities db = new cursomvcEntities())
            {
                lst = (from d in db.user
                       where d.idState == 1
                       orderby d.email
                       select new UserTableViewModel
                       {
                           Email = d.email,
                           Id = d.id,
                           Edad = d.edad
                       }).ToList();
            }
                return View(lst);
        }


        //----------------Agregar---------------------
        // POST: User/Create
        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(UserViewModel model)
        {
            //validar los data Notations
            if (!ModelState.IsValid) 
            {
                return View(model);
            }

            //guardar en la base de datos
            using (var db = new cursomvcEntities())
            {
                user oUser = new user();
                oUser.idState = 1;
                oUser.email = model.Email;
                oUser.edad = model.Edad;
                oUser.password = model.Password;
                //agregar al objeto
                db.user.Add(oUser);

                //guardar
                db.SaveChanges();
            }

            return Redirect(Url.Content("~/User/"));
        }

        //----------------Editar---------------------
        public ActionResult Edit(int Id)
        {
            EditUserViewModel model = new EditUserViewModel();

            using (var db = new cursomvcEntities())
            {
                //obtener el id
                var oUser = db.user.Find(Id);
                
                model.Edad = (int)oUser.edad;
                model.Email = oUser.email;
                model.Id = oUser.id;
            }

                return View(model);
        }

        [HttpPost]
        public ActionResult Edit(EditUserViewModel model)
        {
            //validar los data Notations
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            using (var db = new cursomvcEntities())
            {
                //obtener el id
                var oUser = db.user.Find(model.Id);
                //guardar en la base de datos
                oUser.email = model.Email;
                oUser.edad = model.Edad;

                //si se modifica el paswwor entra a este if
                if (model.Password != null && model.Password.Trim() != "")
                {
                    oUser.password = model.Password;
                }

                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
               
            }
            return Redirect(Url.Content("~/User/"));

        }

        //----------------Eliminar---------------------
        [HttpPost]
        public ActionResult Delete(int Id)
        {
            //como el metodo es eliminar no necesita validar los data Notations


            using (var db = new cursomvcEntities())
            {
                //obtener el id
                var oUser = db.user.Find(Id);
                oUser.idState = 3;//id = 3 es para cambiarlo a eliminado en la bd


                db.Entry(oUser).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

            }
            return Content("1");

        }

    }
}
