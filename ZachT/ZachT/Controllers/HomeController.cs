using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZachT.Models;
using System.Data.Entity;

namespace ZachT.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            DBManager dBManager = new DBManager();
            List<ClsPokemon> poke = dBManager.GetPokemons();
            ViewBag.poke = poke;

            return View();
        }

        public ActionResult CreatePoke()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreatePoke(ClsPokemon poke) {

            DBManager dBmanager = new DBManager();
            try
            {
                dBmanager.NewPoke(poke);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            //重新導入指定動作
            return RedirectToAction("Index");
        }
   
        public ActionResult EditPoke(int id) {
            DBManager dbmanager = new DBManager();
            ClsPokemon poke = dbmanager.GetPokeByID(id);
            return View(poke);
        }

        [HttpPost]
        public ActionResult EditPoke(ClsPokemon poke)
        {
            DBManager dbmanager = new DBManager();
            dbmanager.UpdatedPoke(poke);
            return RedirectToAction("Index");
        }

        public ActionResult DeletePoke(int id) {
            DBManager dbmanager = new DBManager();
            dbmanager.DeletedPokeByID(id);
            return RedirectToAction("Index");
        }

        public ActionResult Index2()
        {
            return Content("<html><body><h1>測試</h1></body></html>");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}