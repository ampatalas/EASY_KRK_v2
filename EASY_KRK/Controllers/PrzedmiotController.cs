using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EASY_KRK.Controllers
{
    [Authorize]
    public class PrzedmiotController : Controller
    {
        //
        // GET: /Przedmiot/
        public ActionResult Index()
        {
            return View();
        }
	}
}