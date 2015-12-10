using EASY_KRK.App_Start;
using EASY_KRK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EASY_KRK.Controllers
{
    public class KierunekController : Controller
    {
        EASYKRKContext db = new EASYKRKContext();
        //
        // GET: /Kierunek/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult MacierzSladowania()
        {
            return View();
        }

        public ActionResult Programy()
        {
            ProgramyViewModel model = new ProgramyViewModel();

            model.Kierunki = db.Kierunki.Select(k => k.NazwaKierunku).Distinct().ToList();
            model.Programy = db.ProgramyKsztalcenia.ToList().FindAll(p => p.JezykStudiow.NazwaJezyka == "polski");
            model.IdProgramu = Settings.IdProgramu;

            return PartialView(model);

        }

        public ActionResult WybierzProgram(int id)
        {
            Settings.IdProgramu = id;
            return RedirectToAction("Programy");
        }
	}
}