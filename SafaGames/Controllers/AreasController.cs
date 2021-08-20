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

namespace SafaGames.Controllers
{
    [Authorize]
    public class AreasController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Areas
        public async Task<ActionResult> Index()
        {
            var areas = db.Areas.Include(a => a.citys);
            return View(await areas.ToListAsync());
        }

        // GET: Areas/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Areas areas = await db.Areas.FindAsync(id);
            if (areas == null)
            {
                return HttpNotFound();
            }
            return View(areas);
        }

        // GET: Areas/Create
        public ActionResult Create()
        {
            ViewBag.CityID = new SelectList(db.Citys, "CityID", "Title");
            return View();
        }

        // POST: Areas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AreaID,Title,imgpic,dt,tm,users,CityID")] Areas areas)
        {
            if (ModelState.IsValid)
            {
                db.Areas.Add(areas);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CityID = new SelectList(db.Citys, "CityID", "Title", areas.CityID);
            return View(areas);
        }

        // GET: Areas/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Areas areas = await db.Areas.FindAsync(id);
            if (areas == null)
            {
                return HttpNotFound();
            }
            ViewBag.CityID = new SelectList(db.Citys, "CityID", "Title", areas.CityID);
            return View(areas);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AreaID,Title,imgpic,dt,tm,users,CityID")] Areas areas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(areas).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CityID = new SelectList(db.Citys, "CityID", "Title", areas.CityID);
            return View(areas);
        }

        // GET: Areas/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Areas areas = await db.Areas.FindAsync(id);
            if (areas == null)
            {
                return HttpNotFound();
            }
            return View(areas);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Areas areas = await db.Areas.FindAsync(id);
            db.Areas.Remove(areas);
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
