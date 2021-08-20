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
    public class DevicesController : Controller
    {
        private MyContext db = new MyContext();

        // GET: Devices
        public async Task<ActionResult> Index()
        {
            return View(await db.Devices.ToListAsync());
        }

        // GET: Devices/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devices devices = await db.Devices.FindAsync(id);
            if (devices == null)
            {
                return HttpNotFound();
            }
            return View(devices);
        }

        // GET: Devices/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Devices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DeviceID,Title,imgpic,dt,tm,users")] Devices devices)
        {
            if (ModelState.IsValid)
            {
                db.Devices.Add(devices);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(devices);
        }

        // GET: Devices/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devices devices = await db.Devices.FindAsync(id);
            if (devices == null)
            {
                return HttpNotFound();
            }
            return View(devices);
        }

        // POST: Devices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "DeviceID,Title,imgpic,dt,tm,users")] Devices devices)
        {
            if (ModelState.IsValid)
            {
                db.Entry(devices).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(devices);
        }

        // GET: Devices/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Devices devices = await db.Devices.FindAsync(id);
            if (devices == null)
            {
                return HttpNotFound();
            }
            return View(devices);
        }

        // POST: Devices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Devices devices = await db.Devices.FindAsync(id);
            db.Devices.Remove(devices);
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
