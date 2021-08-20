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
    public class CitysController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Citys
        public async Task<ActionResult> Index()
        {
            var citys = db.Citys.Include(c => c.countrys);
            return View(await citys.ToListAsync());
        }

        // GET: Citys/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citys citys = await db.Citys.FindAsync(id);
            if (citys == null)
            {
                return HttpNotFound();
            }
            return View(citys);
        }

        // GET: Citys/Create
        public ActionResult Create()
        {
            ViewBag.CountryID = new SelectList(db.Countrys, "CountryID", "Title");
            return View();
        }

        // POST: Citys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CityID,Title,imgpic,dt,tm,users,CountryID")] Citys citys)
        {
            if (ModelState.IsValid)
            {
                db.Citys.Add(citys);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CountryID = new SelectList(db.Countrys, "CountryID", "Title", citys.CountryID);
            return View(citys);
        }

        // GET: Citys/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citys citys = await db.Citys.FindAsync(id);
            if (citys == null)
            {
                return HttpNotFound();
            }
            ViewBag.CountryID = new SelectList(db.Countrys, "CountryID", "Title", citys.CountryID);
            return View(citys);
        }

        // POST: Citys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CityID,Title,imgpic,dt,tm,users,CountryID")] Citys citys)
        {
            if (ModelState.IsValid)
            {
                db.Entry(citys).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CountryID = new SelectList(db.Countrys, "CountryID", "Title", citys.CountryID);
            return View(citys);
        }

        // GET: Citys/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Citys citys = await db.Citys.FindAsync(id);
            if (citys == null)
            {
                return HttpNotFound();
            }
            return View(citys);
        }

        // POST: Citys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Citys citys = await db.Citys.FindAsync(id);
            db.Citys.Remove(citys);
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
