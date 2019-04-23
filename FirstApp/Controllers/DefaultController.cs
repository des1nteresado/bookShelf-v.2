using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FirstApp.Models;

namespace FirstApp.Controllers
{
    public class DefaultController : Controller
    {
        BookContext db = new BookContext();

        // GET: Default
        public async Task<ActionResult> Index()
        {
            IEnumerable<Book> books = await db.Books.ToListAsync();
            return View(books.First());
        }
    }
}