using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Zipper.Models;
using Zipper.BLL;
using System.IO;

namespace Zipper.Controllers
{
    public class NamesController : Controller
    {
        //
        // GET: /Names/

        public ActionResult ViewAll()
        {
            NameSource namesToSearch = NamesBLL.GetAllNames();
            return View(namesToSearch);
        }

        public ActionResult Upload()
        {
            return View();
        }


       [HttpPost]
       public ActionResult Upload(HttpPostedFileBase file)
       {
           if (NamesBLL.IsValidFile(file))
           {
               NamesBLL.ProcessNamesFile(file);
               return View();
           }
           else
           {
               SetErrorMsg();
               return View();
           }

        }


       public void SetErrorMsg()
       {
           ViewBag.ErrorMsg = "Please upload a valid CSV";
       }
    
    }
}
