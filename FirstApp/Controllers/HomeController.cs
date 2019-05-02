using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;
using FirstApp.Models;
using FirstApp.Util;

namespace FirstApp.Controllers
{
    public class HomeController : Controller
    {
        // создаем контекст данных
        BookContext db = new BookContext();

        public async Task<ActionResult> Index(int? status)
        {
            // получаем из бд все объекты Book
            IEnumerable<Book> books = await db.Books.ToListAsync();
            // передаем все объекты в динамическое свойство Books в ViewBag
            //ViewBag.Books = books;
            // возвращаем представление
            if (status == 1)
            {
                ViewBag.Message =
                    "Ok. Book was edited. This is message from partial view! But this message create in Index action method! Think about it..";
            }
            //используем строго типизированное представление
            return View(books);
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
            Purchase prhs = db.Purchases.Find(id);
            return View(prhs);
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(Book book)
        {
            db.Books.Add(book);
            db.SaveChanges();
            return RedirectToAction("Index");
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

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        [HttpPost]
        public ActionResult Edit(Book book)
        {
            db.Entry(book).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index", "Home", new { status = 1 });
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            return View(b);
        }
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Book b = db.Books.Find(id);
            if (b == null)
            {
                return HttpNotFound();
            }
            db.Books.Remove(b);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Partial()
        {
            ViewBag.Message = "This is partial view!";
            return PartialView();
        }

        public ActionResult GetFile()
        {
            string filePath = Server.MapPath("~/Files/Download.js");
            string fileType = "application/js";
            return File(filePath, fileType);
        }

        public ActionResult GetUrl()
        {
            return View();
        }

        public string Square()
        {
            int a = Int32.Parse(Request.Params["a"]);
            int h = Int32.Parse(Request.Params["h"]);
            double s = a * h / 2.0;
            return "<h2>Площадь треугольника с основанием " + a + " и высотой " + h + " равна " + s + "</h2>";
        }

        public string GetInfo()
        {
            HttpContext.Response.Write("hello from response");
            HttpContext.Response.Cookies["id"].Value = "ca-4353w";

            string browser = HttpContext.Request.Browser.Browser;
            string user_agent = HttpContext.Request.UserAgent;
            string url = HttpContext.Request.RawUrl;
            string ip = HttpContext.Request.UserHostAddress;
            string id = HttpContext.Request.Cookies["id"].Value;
            string referrer = HttpContext.Request.UrlReferrer == null ? "" : HttpContext.Request.UrlReferrer.AbsoluteUri;
            return "<p> Cookie id: " + id + "</p><p> SessionID: " + HttpContext.Session.SessionID +
                   "<p>Browser: " + browser + "</p><p>User-Agent: " + user_agent + "</p><p>Url запроса: " + url +
                   "</p><p>Реферер: " + referrer + "</p><p>IP-адрес: " + ip + "</p>";
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