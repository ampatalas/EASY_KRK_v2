using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EASY_KRK.Controllers
{
    [Authorize]
    public class KategoriaController : Controller
    {
        //
        // GET: /Kategoria/
        public ActionResult Index()
        {
            return View();
        }
	}
}