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
               bool success = NamesBLL.ProcessNamesFile(file);

               if (success)
               {
                   ShowSuccess();
               }
               else
               {
                   ShowErrorProcessing();
               }
               return View();
           }
           else
           {
               ShowInvalidFormat();
               return View();
           }

        }

       [HttpPost]
       public ActionResult Remove(string id)
       {
           NamesBLL.DeleteById(id);
           return ViewAll();
       }

       public void ShowInvalidFormat()
       {
           ViewBag.ErrorMsg = "Please upload a valid CSV";
       }

       public void ShowErrorProcessing()
       {
           ViewBag.ErrorMsg = "There was a problem reading the CSV or it was empty.  No names were updated.";
       }

       

       public void ShowSuccess()
       {
           ViewBag.ErrorMsg = string.Empty;
           ViewBag.SuccessMsg = "Names uploaded successfully!";
       }

    }
}
