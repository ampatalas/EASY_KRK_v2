using EASY_KRK.App_Start;
using EASY_KRK.Models;
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
        EASYKRKContext db = new EASYKRKContext();
        //
        // GET: /Przedmiot/
        public ActionResult Index()
        {
            PrzedmiotyViewModel Model = new PrzedmiotyViewModel();
            Model.Kategorie = db.Kategorie.ToList().FindAll(k => k.ProgramStudiow.IdProgramuKsztalcenia == Settings.IdProgramu 
                                                                 && k.KategoriaNadrzedna == null);
            Model.IdPrzedmiotu = 0;
            return View(Model);
        }

        public ActionResult DodajPrzedmiot(int IdKategorii)
        {
            DodajPrzedmiotViewModel Model = new DodajPrzedmiotViewModel();
            Model.Kategorie = new SelectList(db.Kategorie.ToList().FindAll(k => k.ProgramStudiow.IdProgramuKsztalcenia == Settings.IdProgramu 
                                                                 && k.Kategorie.Count() == 0), "IdKategorii", "NazwaKategorii", IdKategorii);
            Model.FormyPrzedmiotu = new SelectList(db.FormyPrzedmiotu, "IdFormyPrzedmiotu", "NazwaFormy");
            Model.FormyZaliczenia = new SelectList(db.FormyZal, "IdFormyZal", "NazwaFormyZal");
            Model.Rodzaje = new SelectList(db.RodzajePrzedmioty, "IdRodzaju", "NazwaRodzaju");
            Model.Typy = new SelectList(db.TypyPrzedmiotu, "IdTypu", "NazwaTypu");

            return View(Model);
        }
	}
}