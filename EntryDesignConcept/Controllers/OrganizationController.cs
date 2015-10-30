using EntryDesignConcept.AppContext;
using EntryDesignConcept.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EntryDesignConcept.Controllers
{
    public class OrganizationController : Controller
    {
        private OrganizationContext db = new OrganizationContext();
        //
        // GET: /Organization/
        public ActionResult Index()
        {
            return View(db.Fetch);
        }       

        //
        // GET: /Organization/Create
        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /Organization/Create
        [HttpPost]
        public ActionResult Create(Organization collection)
        {
            try
            {
                // TODO: Add insert logic here
                db.Insert(collection);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Organization/Edit/5
        public ActionResult Edit(int id)
        {
            return View(db.Fetch.Single(o => o.ID == id));
        }

        //
        // POST: /Organization/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Organization collection)
        {
            try
            {
                // TODO: Add update logic here
                db.Update(collection, id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(collection);
            }
        }      
    }
}
