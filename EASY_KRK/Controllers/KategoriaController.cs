using EASY_KRK.Models;
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
        EASYKRKContext db = new EASYKRKContext();
        //
        // GET: /Kategoria/
        public ActionResult Index()
        {
            KategorieViewModel Model = new KategorieViewModel();
            var IdProgramu = Convert.ToInt32(this.HttpContext.Session["IdProgramu"]);
            Model.Kategorie = db.Kategorie.Where(k => k.ProgramStudiow.IdProgramuKsztalcenia == IdProgramu
                                                        && k.KategoriaNadrzedna == null);
            return View(Model);
        }
	}
}