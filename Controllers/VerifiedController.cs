using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zipper.Models;
using Zipper.BLL;

namespace Zipper.Controllers
{
    public class VerifiedController : Controller
    {
        //
        // GET: /Verify/

        public ActionResult Verify()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Verify(VerifiedPersons model)
        {

            if (ModelState.IsValid)
            {
                VerifiedBLL.Add(model);
            }
            return View();
        }

    }
}
