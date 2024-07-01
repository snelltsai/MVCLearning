using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZachT.Models;
using System.Data.Entity;
using System.Drawing;
using System.IO;

namespace ZachT.Controllers
{
    public class HomeController : Controller
    {
        dbToDoEntities db = new dbToDoEntities();

        //JLearningEntities2 db = new JLearningEntities2();


        public string  ShowArrSum()
        {
            string result = "";
            int[] arr = {1,2,3,4};
            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {
                result += arr[i] + ",";
                sum += arr[i];
            }
            result += "<br/>";
            result += "sum is : " + sum;

            return result;
        }
        [ActionName("ToDo")]
        public ActionResult Index()
        {

            //DBManager dBManager = new DBManager();
            //List<ClsPokemon> poke = dBManager.GetPokemons();
            //ViewBag.poke = poke;
            var todos = db.tToDo.OrderByDescending(m => m.fLevel).ToList();

            ViewBag.TODO = todos;
            return View("Upload");
        }
        
        public ActionResult ToDo1()
        {
            dbToDoEntities db = new dbToDoEntities();
            var todos = db.tToDo.OrderByDescending(m => m.fLevel).ToList();
            return View(todos);
        }

        public ActionResult Create()
        {
            return View("NewCreate");
        }

        [HttpPost]
        public ActionResult CreateImg(HttpPostedFileBase photo)
        {
            if (photo != null)
            {
                string fileName = Path.GetFileName(photo.FileName);
                var path = Path.Combine(Server.MapPath("~/Photos"), fileName);
                photo.SaveAs(path);
            }
            return View("Upload");

        }

        [HttpPost]
        public ActionResult Create(Todolsit todoo)
        {
            tToDo todo = new tToDo();

            //todo.fId = todoo.fId;
            //todo.fLevel = todoo.fTitle;
            //todo.fTitle = todoo.fLevel;

            ViewBag.fId = todoo.fId;
            ViewBag.fLevel = todoo.fLevel;
            ViewBag.fTitle = todoo.fTitle;


            //db.tToDo.Add(todo);
            //db.SaveChanges();

            return View("NewCreate");

        }

        public ActionResult Delete(int id)
        {
            var todo = db.tToDo.Where(m => m.fId == id).FirstOrDefault();
            db.tToDo.Remove(todo);
            db.SaveChanges();
            return RedirectToAction("ToDo");
        }
        //public ActionResult ShowName()
        //{
        //    var result = db.Pokemon;
        //    string show = "";

        //    foreach (var m in result)
        //    {
        //        show += m.PokeName.ToString();
        //    }
        //    ViewBag.show = show;
        //    return View();
        //}
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