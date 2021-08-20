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
    public class AvdTypesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: AvdTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.AvdTypes.ToListAsync());
        }

        // GET: AvdTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvdTypes avdTypes = await db.AvdTypes.FindAsync(id);
            if (avdTypes == null)
            {
                return HttpNotFound();
            }
            return View(avdTypes);
        }

        // GET: AvdTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AvdTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "AvdTypeID,Title,imgpic,dt,tm,users")] AvdTypes avdTypes)
        {
            if (ModelState.IsValid)
            {
                db.AvdTypes.Add(avdTypes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(avdTypes);
        }

        // GET: AvdTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvdTypes avdTypes = await db.AvdTypes.FindAsync(id);
            if (avdTypes == null)
            {
                return HttpNotFound();
            }
            return View(avdTypes);
        }

        // POST: AvdTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "AvdTypeID,Title,imgpic,dt,tm,users")] AvdTypes avdTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(avdTypes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(avdTypes);
        }

        // GET: AvdTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AvdTypes avdTypes = await db.AvdTypes.FindAsync(id);
            if (avdTypes == null)
            {
                return HttpNotFound();
            }
            return View(avdTypes);
        }

        // POST: AvdTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            AvdTypes avdTypes = await db.AvdTypes.FindAsync(id);
            db.AvdTypes.Remove(avdTypes);
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
