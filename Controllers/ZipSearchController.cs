using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zipper.Models;
using Zipper.BLL;

namespace Zipper.Controllers
{
    public class ZipSearchController : Controller
    {
        //
        // GET: /ZipSearch/Search

        public ActionResult Search()
        {
            return View();
        }

        //POST: /ZipSearch/Search
        [HttpPost]
        public ActionResult Search(ZipCodes model)
        {
            if (ModelState.IsValid)
            {
                NameSource namesToSearch = NamesBLL.GetAllNames();
                List<WPerson> persons = ZipperBLL.GetSearchResults(namesToSearch, model);
              
                ViewBag.ZipCode = model.ZipCode;
                return View("Results", persons);
            }

            return View();
        }
    }
}
