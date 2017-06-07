using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApproveProject.Repository;
using ApproveProject.Models;
using Newtonsoft.Json;

namespace ApproveProject.Controllers
{
    public class ApproveCreditsController : Controller
    {
        private ApproveCreditContext db = new ApproveCreditContext();

        // GET: ApproveCredits
        public ActionResult Index()
        {
            return View(db.ApproveCredit.ToList());
        }

        // GET: ApproveCredits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApproveCredit approveCredit = db.ApproveCredit.Find(id);
            if (approveCredit == null)
            {
                return HttpNotFound();
            }
            return View(approveCredit);
        }

        // GET: ApproveCredits/Create
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Seq,Name,Credit")] ApproveCredit approveCredit)
        {
            if (ModelState.IsValid)
            {
                db.ApproveCredit.Add(approveCredit);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(approveCredit);
        }

        // GET: ApproveCredits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApproveCredit approveCredit = db.ApproveCredit.Find(id);
            if (approveCredit == null)
            {
                return HttpNotFound();
            }
            return View(approveCredit);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Seq,Name,Credit")] ApproveCredit approveCredit)
        {
            if (ModelState.IsValid)
            {
                db.Entry(approveCredit).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(approveCredit);
        }

        // GET: ApproveCredits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApproveCredit approveCredit = db.ApproveCredit.Find(id);
            if (approveCredit == null)
            {
                return HttpNotFound();
            }
            return View(approveCredit);
        }

        // POST: ApproveCredits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApproveCredit approveCredit = db.ApproveCredit.Find(id);
            db.ApproveCredit.Remove(approveCredit);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // GET: ApproveCredits/Send
        public ActionResult Send()
        {
            return View();
        }

        // POST: ApproveCredits/Send
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Send([Bind(Include = "Id,Name,Credit")] ApproveCredit approveCredit)
        {
            List<string> transactionLog = new List<string>();

            
            ManagementPerson manager = new Manager();
            ManagementPerson gm = new GM();

            manager.SetSuccessor(gm);
            var data = db.ApproveCredit.OrderBy(f => f.Seq);

            foreach (var item in data)
            {
                manager.CreditLine = item.Credit;
                manager.Name = item.Name;
                gm.CreditLine = item.Credit;
                gm.Name = item.Name;
                if (manager.Approve(approveCredit.Credit))
                {
                    transactionLog.Add(string.Format("{0} approved request of {1}", item.Name, approveCredit.Credit));
                    break;
                }
                else
                {
                    transactionLog.Add(string.Format("Send to Successor {0}", item.Name));
                }
            }
            if (transactionLog.Any())
            {
                ViewBag.Items = transactionLog;
                db.HistoryTransaction.Add(new HistoryTransaction {MoneyApprove= approveCredit.Credit, Log = JsonConvert.SerializeObject(transactionLog) });
                db.SaveChanges();
            }
            return View(approveCredit);
        }

        public ActionResult Log()
        {
            return View(db.HistoryTransaction.ToList());
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
