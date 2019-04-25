using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using Antlr.Runtime.Misc;
using FirstApp.Models.Football;

namespace FirstApp.Controllers
{
    public class SoccerController : Controller
    {
        SoccerContext db = new SoccerContext();
        public ActionResult Index()
        {
            var players = db.Players.Include(p => p.Team);
            return View(players.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            SelectList teams = new SelectList(db.Teams, "Id", "Name");
            ViewBag.Teams = teams;
            return View();
        }

        [HttpPost]
        public ActionResult Create(Player player)
        {
            if (player != null)
            {
                db.Players.Add(player);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            // Находим в бд футболиста
            Player player = db.Players.Find(id);
            if (player != null)
            {
                // Создаем список команд для передачи в представление
                SelectList teams = new SelectList(db.Teams, "Id", "Name", player.TeamId);
                ViewBag.Teams = teams;
                return View(player);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(Player player)
        {
            db.Entry(player).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id != null)
            {
                var player = db.Players.Find(id);
                if (player != null)
                {
                    return View(player);
                }
            }

            return HttpNotFound();
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id != null)
            {
                var player = db.Players.Find(id);
                if (player != null)
                {
                    db.Players.Remove(player);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

            return HttpNotFound();
        }

    }
}