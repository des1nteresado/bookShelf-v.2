using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FirstApp.Models;
using FirstApp.Util;

namespace FirstApp.Controllers
{
    public class HomeController : Controller
    {
        // создаем контекст данных
        BookContext db = new BookContext();

        public ActionResult Index()
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = db.Books;
            // передаем все объекты в динамическое свойство Books в ViewBag
            ViewBag.Books = books;
            // возвращаем представление
            return View();
        }


        [HttpGet]
        public ActionResult Buy(int id)
        {
            if (id > 1000) // Our bookshelf can save about 1000 books.
            {
                return Redirect("/Home/Index");
                //   return RedirectToRoute(new { controller="Home", action="Index"});
                //   return RedirectToAction("Square", "Home", new { a=10,h=12}); 
                //   временная переадрессация, постоянная с Permament
            }
            ViewBag.BookId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            // добавляем информацию о покупке в базу данных
            db.Purchases.Add(purchase);
            // сохраняем в бд все изменения
            db.SaveChanges();
            return getToday() + " Спасибо," + purchase.Person + ", за покупку!";
        }

        public ActionResult GetFile()
        {
            string filePath = Server.MapPath("~/Files/Download.js");
            string fileType = "application/js";
            return File(filePath, fileType);
        }

        public string Square()
        {
            int a = Int32.Parse(Request.Params["a"]);
            int h = Int32.Parse(Request.Params["h"]);
            double s = a * h / 2.0;
            return "<h2>Площадь треугольника с основанием " + a + " и высотой " + h + " равна " + s + "</h2>";
        }

        public ActionResult GetHtml()
        {
            return new HtmlResult("<h2> Hello World! </h2>");
        }
        private DateTime getToday()
        {
            return DateTime.Now;
        }
    }
}