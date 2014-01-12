using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zipper.Models;
using Zipper.BLL;

namespace Zipper.Controllers
{
    public class NamesController : Controller
    {
        //
        // GET: /Names/

        public ActionResult ViewAll()
        {
            NameSource namesToSearch = ZipperBLL.GetAllNames();
            return View(namesToSearch);
        }

    }
}
