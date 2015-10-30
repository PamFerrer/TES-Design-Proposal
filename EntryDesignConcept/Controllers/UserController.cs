using EntryDesignConcept.AppContext;
using EntryDesignConcept.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntryDesignConcept.Controllers
{
    public class UserController : Controller
    {
        private UserContext db = new UserContext();
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View(db.GetAllUsers);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(User newData)
        {
            db.Insert(newData);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            return View(db.GetAllUsers.Single(a => a.ID == id));
        }
        [HttpPost]
        public ActionResult Edit(User updatedData, int id)
        {
            db.Update(updatedData, id);
            return RedirectToAction("Index");
        }
	}
}