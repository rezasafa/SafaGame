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
    public class CategorysController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Categorys
        public async Task<ActionResult> Index()
        {
            return View(await db.Categorys.ToListAsync());
        }

        // GET: Categorys/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorys categorys = await db.Categorys.FindAsync(id);
            if (categorys == null)
            {
                return HttpNotFound();
            }
            return View(categorys);
        }

        // GET: Categorys/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categorys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "CategoryID,Title,imgpic,dt,tm,users")] Categorys categorys)
        {
            if (ModelState.IsValid)
            {
                db.Categorys.Add(categorys);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(categorys);
        }

        // GET: Categorys/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorys categorys = await db.Categorys.FindAsync(id);
            if (categorys == null)
            {
                return HttpNotFound();
            }
            return View(categorys);
        }

        // POST: Categorys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "CategoryID,Title,imgpic,dt,tm,users")] Categorys categorys)
        {
            if (ModelState.IsValid)
            {
                db.Entry(categorys).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(categorys);
        }

        // GET: Categorys/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Categorys categorys = await db.Categorys.FindAsync(id);
            if (categorys == null)
            {
                return HttpNotFound();
            }
            return View(categorys);
        }

        // POST: Categorys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Categorys categorys = await db.Categorys.FindAsync(id);
            db.Categorys.Remove(categorys);
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
