using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SafaGames.Models;
using System.IO;

namespace SafaGames.Controllers
{
    public class GamesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Games
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var games = db.Games.Include(g => g.areas).Include(g => g.avdtypes).Include(g => g.categorys).Include(g => g.devices).Include(g => g.styles);
            return View(await games.ToListAsync());
        }

        // GET: Games/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Games games = await db.Games.FindAsync(id);
            if (games == null)
            {
                return HttpNotFound();
            }
            return View(games);
        }

        // GET: Games/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Title");
            ViewBag.AvdTypeID = new SelectList(db.AvdTypes, "AvdTypeID", "Title");
            ViewBag.categoryID = new SelectList(db.Categorys, "CategoryID", "Title");
            ViewBag.DeviceID = new SelectList(db.Devices, "DeviceID", "Title");
            ViewBag.StyleID = new SelectList(db.Styles, "StyleID", "Title");
            return View();
        }

        // POST: Games/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "GameID,imgpic,Title,Body,Tags,Price,Tells,StyleID,AreaID,categoryID,DeviceID,AvdTypeID,Status,dt,tm,users")] Games games, HttpPostedFileBase upfile)
        {
            if (ModelState.IsValid)
            {
                var dt = DateTime.Now.Year.ToString() + "/" +
                        DateTime.Now.Month.ToString() + "/" +
                        DateTime.Now.Day.ToString();
                var tm = DateTime.Now.Hour.ToString() + ":" +
                        DateTime.Now.Minute.ToString();
                var dtname = dt.Replace("/","") + tm.Replace(":","") +
                        DateTime.Now.Second.ToString() +
                        DateTime.Now.Millisecond.ToString();

                if (upfile != null && upfile.ContentLength > 0)
                {
                    string pic = System.IO.Path.GetFileName(upfile.FileName);
                    string path = System.IO.Path.Combine(
                                           Server.MapPath("~/img"),
                                           dtname +
                                           pic);

                    upfile.SaveAs(path);
                    
                    using (MemoryStream ms = new MemoryStream())
                    {
                        upfile.InputStream.CopyTo(ms);
                        byte[] array = ms.GetBuffer();
                    }

                    games.imgpic = dtname + pic;
                }
                games.dt = dt;
                games.tm = tm;
                games.users = User.Identity.Name;
                db.Games.Add(games);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Title", games.AreaID);
            ViewBag.AvdTypeID = new SelectList(db.AvdTypes, "AvdTypeID", "Title", games.AvdTypeID);
            ViewBag.categoryID = new SelectList(db.Categorys, "CategoryID", "Title", games.categoryID);
            ViewBag.DeviceID = new SelectList(db.Devices, "DeviceID", "Title", games.DeviceID);
            ViewBag.StyleID = new SelectList(db.Styles, "StyleID", "Title", games.StyleID);
            return View(games);
        }

        // GET: Games/Edit/5
        [Authorize]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Games games = await db.Games.FindAsync(id);
            if (games == null)
            {
                return HttpNotFound();
            }
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Title", games.AreaID);
            ViewBag.AvdTypeID = new SelectList(db.AvdTypes, "AvdTypeID", "Title", games.AvdTypeID);
            ViewBag.categoryID = new SelectList(db.Categorys, "CategoryID", "Title", games.categoryID);
            ViewBag.DeviceID = new SelectList(db.Devices, "DeviceID", "Title", games.DeviceID);
            ViewBag.StyleID = new SelectList(db.Styles, "StyleID", "Title", games.StyleID);
            return View(games);
        }

        // POST: Games/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "GameID,imgpic,Title,Body,Tags,Price,Tells,StyleID,AreaID,categoryID,DeviceID,AvdTypeID,Status,dt,tm,users")] Games games)
        {
            if (ModelState.IsValid)
            {
                db.Entry(games).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.AreaID = new SelectList(db.Areas, "AreaID", "Title", games.AreaID);
            ViewBag.AvdTypeID = new SelectList(db.AvdTypes, "AvdTypeID", "Title", games.AvdTypeID);
            ViewBag.categoryID = new SelectList(db.Categorys, "CategoryID", "Title", games.categoryID);
            ViewBag.DeviceID = new SelectList(db.Devices, "DeviceID", "Title", games.DeviceID);
            ViewBag.StyleID = new SelectList(db.Styles, "StyleID", "Title", games.StyleID);
            return View(games);
        }

        // GET: Games/Delete/5
        [Authorize]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Games games = await db.Games.FindAsync(id);
            if (games == null)
            {
                return HttpNotFound();
            }
            return View(games);
        }

        // POST: Games/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Games games = await db.Games.FindAsync(id);
            db.Games.Remove(games);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
