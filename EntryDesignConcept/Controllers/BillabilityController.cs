using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EntryDesignConcept.Models;
using EntryDesignConcept.AppContext;

namespace EntryDesignConcept.Controllers
{
    public class BillabilityController : Controller
    {
        private BillabilityContext db = new BillabilityContext();
        private PositionContext dbPosition = new PositionContext();
        private ActivityContext dbActivity = new ActivityContext();
        //
        // GET: /Billability/
        public ActionResult Index()
        {
            return View(db.Fetch);
        }
        public ActionResult Create()
        {
            CreateDropDown();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Billability newData)
        {
            CreateDropDown();
            db.Insert(newData);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            Billability record = db.Fetch.Single(a => a.ID == id);
            SetDropDown(record);
            return View(record);
        }
        [HttpPost]
        public ActionResult Edit(Billability updatedData, int id)
        {
            SetDropDown(updatedData);
            db.Update(updatedData, id);
            return RedirectToAction("Index");
        }

        private void CreateDropDown()
        {
            ViewBag.Position = new SelectList(dbPosition.GetAllActivities, "ID", "Name");
            ViewBag.Activity = new SelectList(dbActivity.GetAllActivities, "ID", "Name");
        }

        private void SetDropDown(Billability data)
        {
            ViewBag.Position = new SelectList(dbPosition.GetAllActivities, "ID", "Name", data.PositionID.ID);
            ViewBag.Activity = new SelectList(dbActivity.GetAllActivities, "ID", "Name", data.ActivityID.ID);
        }
	}
}