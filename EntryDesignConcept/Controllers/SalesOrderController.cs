using EntryDesignConcept.AppContext;
using EntryDesignConcept.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntryDesignConcept.Controllers
{
    public class SalesOrderController : Controller
    {
        private SalesOrderContext db = new SalesOrderContext();
        public ActionResult Index()
        {
            return View(db.GetAllUsers);
        }
        public ActionResult Create()
        {
            //CreateDropDowns();
            return View();
        }
        [HttpPost]
        public ActionResult Create(SalesOrder newData)
        {
            //CreateDropDowns();
            db.Insert(newData);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {

            //User user = db.GetAllUsers.Single(a => a.ID == id);
            //SetDropDowns(user);
            return View(db.GetAllUsers.Single(s => s.ID == id));
        }
        [HttpPost]
        public ActionResult Edit(SalesOrder updatedData, int id)
        {
            //SetDropDowns(updatedData);
            db.Update(updatedData, id);
            return RedirectToAction("Index");
        }
	}
}