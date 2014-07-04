using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zipper.Models;
using Zipper.BLL;
using Zipper.Helpers;

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
                List<WPerson> persons;

                if(Convert.ToBoolean(Utils.GetConfigSetting("MockAPI")))  //use fake data for testing.
                {
                    persons = ZipperBLL.GetMockResults();
                }
                else //really hit the whitepages api
                {
                    NameSource namesToSearch = NamesBLL.GetAllNames();
                    persons = ZipperBLL.GetSearchResults(namesToSearch, model);
                }

                ViewBag.ZipCode = model.ZipCode;
                return View("Results", persons);
            }

            return View();
        }
    }
}
