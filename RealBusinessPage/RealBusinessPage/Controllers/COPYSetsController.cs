using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealBusinessPage.Models;

namespace RealBusinessPage.Controllers
{
    public class COPYSetsController : Controller
    {
        private ServerSideEntities2 db = new ServerSideEntities2();

        // GET: COPYSets
        public async Task<ActionResult> Index()
        {
            var cOPYSet = db.COPYSet.Include(c => c.BOOKSet).Include(c => c.STATUSSet);
            return View(await cOPYSet.ToListAsync());
        }

        // GET: COPYSets/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COPYSet cOPYSet = await db.COPYSet.FindAsync(id);
            if (cOPYSet == null)
            {
                return HttpNotFound();
            }
            return View(cOPYSet);
        }

        // GET: COPYSets/Create
        public ActionResult Create()
        {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("NoAuthrization", "Error");
            }
            ViewBag.BOOKISBN = new SelectList(db.BOOKSet, "ISBN", "PublicationYear");
            ViewBag.STATUSStatusId = new SelectList(db.STATUSSet, "StatusId", "status");
            return View();
        }

        // POST: COPYSets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Barcode,Location,Library,STATUSStatusId,BOOKISBN")] COPYSet cOPYSet)
        {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("NoAuthrization", "Error");
            }
            if (ModelState.IsValid)
            {
                db.COPYSet.Add(cOPYSet);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BOOKISBN = new SelectList(db.BOOKSet, "ISBN", "PublicationYear", cOPYSet.BOOKISBN);
            ViewBag.STATUSStatusId = new SelectList(db.STATUSSet, "StatusId", "status", cOPYSet.STATUSStatusId);
            return View(cOPYSet);
        }

        // GET: COPYSets/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("NoAuthrization", "Error");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COPYSet cOPYSet = await db.COPYSet.FindAsync(id);
            if (cOPYSet == null)
            {
                return HttpNotFound();
            }
            ViewBag.BOOKISBN = new SelectList(db.BOOKSet, "ISBN", "PublicationYear", cOPYSet.BOOKISBN);
            ViewBag.STATUSStatusId = new SelectList(db.STATUSSet, "StatusId", "status", cOPYSet.STATUSStatusId);
            return View(cOPYSet);
        }

        // POST: COPYSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Barcode,Location,Library,STATUSStatusId,BOOKISBN")] COPYSet cOPYSet)
        {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("NoAuthrization", "Error");
            }
            if (ModelState.IsValid)
            {
                db.Entry(cOPYSet).State = System.Data.Entity.EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BOOKISBN = new SelectList(db.BOOKSet, "ISBN", "PublicationYear", cOPYSet.BOOKISBN);
            ViewBag.STATUSStatusId = new SelectList(db.STATUSSet, "StatusId", "status", cOPYSet.STATUSStatusId);
            return View(cOPYSet);
        }

        // GET: COPYSets/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("NoAuthrization", "Error");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            COPYSet cOPYSet = await db.COPYSet.FindAsync(id);
            if (cOPYSet == null)
            {
                return HttpNotFound();
            }
            return View(cOPYSet);
        }

        // POST: COPYSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            if (Session["level"].ToString() != "2")
            {
                return RedirectToAction("NoAuthrization", "Error");
            }
            COPYSet cOPYSet = await db.COPYSet.FindAsync(id);
            db.COPYSet.Remove(cOPYSet);
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
