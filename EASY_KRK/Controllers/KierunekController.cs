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
            MacierzSladowaniaViewModel model = new MacierzSladowaniaViewModel();
            if (Settings.IdProgramu != 0 && Settings.IdKierunku != 0)
            {
                Kierunek k = db.Kierunki.ToList().Find(kierunek => kierunek.IdKierunku == Settings.IdKierunku);
                model.Udzialy = db.UdzialyProcentowe.ToList().FindAll(u => u.IdKierunku == k.IdKierunku);
                model.KEKI = db.KEKI.ToList().FindAll(kek => kek.Kierunek.IdKierunku == Settings.IdKierunku);
                model.MEKI = db.MEKI.ToList().FindAll(m => (m.IdPoziomu == k.IdPoziomu && m.IdProfilu == k.IdProfilu &&
                                                                model.Udzialy.ToList().Find(u => u.IdObszaru == m.IdObszaru) != null));
                model.Sladowania = db.Sladowania;
                model.Przedmioty = db.Przedmioty.ToList().FindAll(p => p.Kategoria.ProgramStudiow.ProgramKsztalcenia.IdProgramuKsztalcenia == Settings.IdProgramu);
                model.KEKIPrzedmiotow = db.KEKIPrzedmiotow.ToList().FindAll(kp => model.Przedmioty.Contains(kp.Przedmiot));
            }


            return View(model);
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
            Settings.IdKierunku = db.ProgramyKsztalcenia.ToList().Find(p => p.IdProgramuKsztalcenia == id).IdKierunku;
            return RedirectToAction("Index", "Kategoria");
        }

        public ActionResult DodajMek(int idKek)
        {
            return RedirectToAction("MacierzSladowania");
        }

        public ActionResult UsunMek(int idKek, int idMek)
        {
            return RedirectToAction("MacierzSladowania");
        }

        public ActionResult UsunPrzedmiot(int idKek, int idPrzedm)
        {
            return RedirectToAction("MacierzSladowania");
        }

        public ActionResult DodajPrzedmiot(int idKek)
        {
            return RedirectToAction("MacierzSladowania");
        }
	}
}