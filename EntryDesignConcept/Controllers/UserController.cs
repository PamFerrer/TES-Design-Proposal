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
        private PositionContext dbPosition = new PositionContext();
        private OrganizationContext dbOrganization = new OrganizationContext();
        private TeamContext dbTeam = new TeamContext();
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View(db.GetAllUsers);
        }
        public ActionResult Create()
        {
            CreateDropDowns();
            return View();
        }
        [HttpPost]
        public ActionResult Create(User newData)
        {
            CreateDropDowns();
            db.Insert(newData);
            return RedirectToAction("Index");
        }
        public ActionResult Edit(int id)
        {
            
            User user = db.GetAllUsers.Single(a => a.ID == id);
            SetDropDowns(user);
            return View(user);
        }
        [HttpPost]
        public ActionResult Edit(User updatedData, int id)
        {
            SetDropDowns(updatedData);
            db.Update(updatedData, id);
            return RedirectToAction("Index");
        }
        public void CreateDropDowns()
        {
            ViewBag.Position = new SelectList(dbPosition.GetAllActivities, "ID", "Name");
            ViewBag.BusinessUnit = new SelectList(dbOrganization.Fetch, "ID", "Name");
            ViewBag.Team = new SelectList(dbTeam.Fetch, "ID", "Name");
        }

        public void SetDropDowns(User user)
        {
            ViewBag.Position = new SelectList(dbPosition.GetAllActivities, "ID", "Name", user.PositionID);
            ViewBag.BusinessUnit = new SelectList(dbOrganization.Fetch, "ID", "Name", user.OrganizationID);
            ViewBag.Team = new SelectList(dbTeam.Fetch, "ID", "Name", user.TeamID);
        }
	}
}