
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
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult MacierzSladowania()
        {
            MacierzSladowaniaViewModel model = new MacierzSladowaniaViewModel();
            if (this.HttpContext.Session["IdProgramu"] != null && this.HttpContext.Session["IdKierunku"] != null)
            {
                Kierunek k = db.Kierunki.ToList().Find(kierunek => kierunek.IdKierunku == Convert.ToInt32(this.HttpContext.Session["IdKierunku"]));
                model.Udzialy = db.UdzialyProcentowe.ToList().FindAll(u => u.IdKierunku == k.IdKierunku);
                model.KEKI = db.KEKI.ToList().FindAll(kek => kek.Kierunek.IdKierunku == Convert.ToInt32(this.HttpContext.Session["IdKierunku"]));
                model.Sladowania = db.Sladowania.ToList().FindAll(s =>s.KEK.IdKierunku == k.IdKierunku);
                model.KEKIPrzedmiotow = db.KEKIPrzedmiotow.ToList().FindAll(kp => (db.Przedmioty.ToList().FindAll(
                                        p => p.Kategoria.ProgramStudiow.ProgramKsztalcenia.IdProgramuKsztalcenia
                                        == Convert.ToInt32(this.HttpContext.Session["IdProgramu"]))).Contains(kp.Przedmiot));
                model.Pokrycia = new Dictionary<int,int>();

                foreach(UdzialProcentowy u in model.Udzialy)
                {
                    model.Pokrycia[u.IdObszaru] = (model.Sladowania.ToList().FindAll(
                                                 s => s.MEK.IdObszaru == u.IdObszaru).Select(s => s.MEK.IdMEK).Distinct().Count()*100)/
                                                 (db.MEKI.ToList().FindAll(m => m.IdObszaru == u.IdObszaru).Count());
                }

                model.Nieprzypisane = new Dictionary<int, IEnumerable<MEK>>();

                foreach(UdzialProcentowy u in model.Udzialy)
                {
                    if (model.Pokrycia[u.IdObszaru] < u.Wartosc)
                    {
                        model.Nieprzypisane[u.IdObszaru] = db.MEKI.ToList().FindAll(m => m.IdObszaru == u.IdObszaru && model.Sladowania.ToList().Find(s => s.IdMEK == m.IdMEK) == null);
                    }
                }
            }


            return View(model);
        }

        public ActionResult Programy()
        {
            if (User.Identity.IsAuthenticated) {

                ProgramyViewModel model = new ProgramyViewModel();
                model.Kierunki = db.Kierunki.Select(k => k.NazwaKierunku).Distinct().ToList();
                model.Programy = db.ProgramyKsztalcenia.ToList().FindAll(p => p.JezykStudiow.NazwaJezyka == "polski");
                model.IdProgramu = Convert.ToInt32(this.HttpContext.Session["IdProgramu"]);
                
                return PartialView(model);
            }

            return new EmptyResult();

        }

        [Authorize]
        public ActionResult WybierzProgram(int id)
        {
            this.HttpContext.Session["IdProgramu"] = id;
            this.HttpContext.Session["IdKierunku"] = db.ProgramyKsztalcenia.ToList().Find(p => p.IdProgramuKsztalcenia == id).IdKierunku;
            return RedirectToAction("Index", "Kategoria");
        }

        public ActionResult DodajMEK(int IdKEK)
        {
            Kierunek k = db.Kierunki.ToList().Find(kierunek => kierunek.IdKierunku == Convert.ToInt32(this.HttpContext.Session["IdKierunku"]));
                DodajMEKViewModel model = new DodajMEKViewModel();
                model.IdKEK = IdKEK;
                model.Filtr = "";
                IEnumerable<MEK> MEKI = db.MEKI.ToList().FindAll(m => (m.IdPoziomu == k.IdPoziomu && m.IdProfilu == k.IdProfilu &&
                                        db.UdzialyProcentowe.ToList().FindAll(u => u.IdKierunku == k.IdKierunku).Find(
                                        u => u.IdObszaru == m.IdObszaru) != null)).FindAll((m => m.Kod.Contains(model.Filtr)
                                    || m.Opis.Contains(model.Filtr)));
                List<object> newMEKI = new List<Object>();
                foreach (MEK m in MEKI)
                {
                    newMEKI.Add(new
                    {
                        Id = m.IdMEK,
                        Nazwa = m.Kod + " " + m.Opis
                    });
                }
                model.MEKI = new SelectList(newMEKI, "Id", "Nazwa");
          
                return PartialView(model);
            }


        public ActionResult DodajMEKForm(DodajMEKViewModel modelDodaj)
        {

                if (ModelState.IsValid)
                {
                    Sladowanie slad = new Sladowanie();
                    slad.IdKEK = modelDodaj.IdKEK;
                    slad.IdMEK = modelDodaj.SelectedMEK;
                    db.Sladowania.Add(slad);
                    db.SaveChanges();
                }


            return RedirectToAction("MacierzSladowania");
        }

        [HttpPost]
        public ActionResult FiltrujMEK(DodajMEKViewModel model)
        {
            Kierunek k = db.Kierunki.ToList().Find(kierunek => kierunek.IdKierunku == Convert.ToInt32(this.HttpContext.Session["IdKierunku"]));
            IEnumerable<MEK> MEKI = db.MEKI.ToList().FindAll(m => ((m.IdPoziomu == k.IdPoziomu && m.IdProfilu == k.IdProfilu &&
                                                (db.UdzialyProcentowe.ToList().FindAll(u => u.IdKierunku == k.IdKierunku).Find(
                                                u => u.IdObszaru == m.IdObszaru) != null)) && (m.Kod.Contains(model.Filtr)
                                                || m.Opis.Contains(model.Filtr)))); 
            List<object> MEKILista = new List<Object>();
            foreach( MEK m in MEKI)
            {
                MEKILista.Add( new {
                    Id = m.IdMEK,
                    Nazwa = m.Kod+" "+ m.Opis
                });
            }
            model.MEKI = new SelectList(MEKILista, "Id", "Nazwa");
            model.Filtr = "";

            return PartialView("ListaMEK", model);
        }


        public ActionResult UsunMEK(int IdKEK, int IdMEK)
        {
            Sladowanie slad = db.Sladowania.ToList().Find(s => (s.IdMEK == IdMEK && s.IdKEK == IdKEK));

            if (slad != null)
            {
                db.Sladowania.Remove(slad);
                db.SaveChanges();
            }
            return RedirectToAction("MacierzSladowania");
        }



        public ActionResult DodajKEKPrzedmiotu(int IdKEK)
        {
            Kierunek k = db.Kierunki.ToList().Find(kierunek => kierunek.IdKierunku == Convert.ToInt32(this.HttpContext.Session["IdKierunku"]));
            DodajKEKPrzedmiotuViewModel model = new DodajKEKPrzedmiotuViewModel();
            model.IdKEK = IdKEK;
            model.Filtr = "";
            IEnumerable<Przedmiot> Przedmioty = db.Przedmioty.ToList().FindAll(p => (p.Kategoria.ProgramStudiow.ProgramKsztalcenia.Kierunek.IdKierunku == k.IdKierunku));
            List<object> PrzedmiotyLista = new List<Object>();
            foreach (Przedmiot p in Przedmioty)
            {
                PrzedmiotyLista.Add(new
                {
                    Id = p.IdPrzedmiotu,
                    Nazwa = p.KodPrzedmiotu + " " + p.NazwaPrzedmiotu
                });
            }
            model.Przedmioty = new SelectList(PrzedmiotyLista, "Id", "Nazwa");

            return PartialView(model);
        }


        public ActionResult DodajKEKPrzedmiotuForm(DodajKEKPrzedmiotuViewModel modelDodaj)
        {

            if (ModelState.IsValid)
            {
                KEKPrzedmiotu kp = new KEKPrzedmiotu();
                kp.IdKEK = modelDodaj.IdKEK;
                kp.IdPrzedmiotu = modelDodaj.IdPrzedmiotu;
                db.KEKIPrzedmiotow.Add(kp);
                db.SaveChanges();
            }


            return RedirectToAction("MacierzSladowania");
        }

        [HttpPost]
        public ActionResult FiltrujPrzedmioty(DodajKEKPrzedmiotuViewModel model)
        {
            Kierunek k = db.Kierunki.ToList().Find(kierunek => kierunek.IdKierunku == Convert.ToInt32(this.HttpContext.Session["IdKierunku"]));
            IEnumerable<Przedmiot> Przedmioty = db.Przedmioty.ToList().FindAll(p => (p.Kategoria.ProgramStudiow.ProgramKsztalcenia.Kierunek.IdKierunku == k.IdKierunku
                                                                                     && (p.KodPrzedmiotu.Contains(model.Filtr) || p.NazwaPrzedmiotu.Contains(model.Filtr))));
            List<object> PrzedmiotyLista = new List<Object>();
            foreach (Przedmiot p in Przedmioty)
            {
                PrzedmiotyLista.Add(new
                {
                    Id = p.IdPrzedmiotu,
                    Nazwa = p.KodPrzedmiotu + " " + p.NazwaPrzedmiotu
                });
            }
            model.Przedmioty = new SelectList(PrzedmiotyLista, "Id", "Nazwa");
            model.Filtr = "";

            return PartialView("ListaPrzedmiotow", model);
        }

        public ActionResult UsunPrzedmiot(int IdKEK, int IdPrzedmiotu)
        {
            KEKPrzedmiotu kp = db.KEKIPrzedmiotow.ToList().Find(k => (k.IdPrzedmiotu == IdPrzedmiotu && k.IdKEK == IdKEK));

            if (kp != null)
            {
                db.KEKIPrzedmiotow.Remove(kp);
                db.SaveChanges();
            }
            return RedirectToAction("MacierzSladowania");
        }
	}
}