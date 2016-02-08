
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
            var IdKierunku = Convert.ToInt32(this.HttpContext.Session["IdKierunku"]);
            var IdProgramu = Convert.ToInt32(this.HttpContext.Session["IdProgramu"]);
            MacierzSladowaniaViewModel model = new MacierzSladowaniaViewModel();
            if (this.HttpContext.Session["IdProgramu"] != null && this.HttpContext.Session["IdKierunku"] != null)
            {
                Kierunek k = db.Kierunki.Where(kierunek => kierunek.IdKierunku == IdKierunku).FirstOrDefault();
                model.Udzialy = db.UdzialyProcentowe.Where(u => u.IdKierunku == k.IdKierunku);
                model.KEKI = db.KEKI.Where(kek => kek.Kierunek.IdKierunku == IdKierunku);
                model.Sladowania = db.Sladowania.Where(s =>s.KEK.IdKierunku == k.IdKierunku);
                model.KEKIPrzedmiotow = db.KEKIPrzedmiotow.Where(kp => (db.Przedmioty.Where(
                                        p => p.Kategoria.ProgramStudiow.ProgramKsztalcenia.IdProgramuKsztalcenia
                                        == IdProgramu)).Contains(kp.Przedmiot));
                model.Pokrycia = new Dictionary<int,int>();

                foreach(UdzialProcentowy u in model.Udzialy)
                {
                    model.Pokrycia[u.IdObszaru] = (model.Sladowania.Where(
                                                 s => s.MEK.IdObszaru == u.IdObszaru).Select(s => s.MEK.IdMEK).Distinct().Count()*100)/
                                                 (db.MEKI.Where(m => m.IdObszaru == u.IdObszaru).Count());
                }

                model.Nieprzypisane = new Dictionary<int, IEnumerable<MEK>>();

                foreach(UdzialProcentowy u in model.Udzialy)
                {
                    if (model.Pokrycia[u.IdObszaru] < u.Wartosc)
                    {
                        model.Nieprzypisane[u.IdObszaru] = db.MEKI.Where(m => m.IdObszaru == u.IdObszaru && model.Sladowania.Where(s => s.IdMEK == m.IdMEK).FirstOrDefault() == null);
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
                model.Programy = db.ProgramyKsztalcenia.OrderByDescending(p => p.JezykStudiow.NazwaJezyka);
                model.IdProgramu = Convert.ToInt32(this.HttpContext.Session["IdProgramu"]);
                
                return PartialView(model);
            }

            return new EmptyResult();

        }

        [Authorize]
        public ActionResult WybierzProgram(int id)
        {
            this.HttpContext.Session["IdProgramu"] = id;
            this.HttpContext.Session["IdKierunku"] = db.ProgramyKsztalcenia.Where(p => p.IdProgramuKsztalcenia == id).First().IdKierunku;
            return RedirectToAction("Index", "Kategoria");
        }

        public ActionResult DodajMEK(int IdKEK)
        {
                var IdKierunku = Convert.ToInt32(this.HttpContext.Session["IdKierunku"]);
                Kierunek k = db.Kierunki.Where(kierunek => kierunek.IdKierunku == IdKierunku).FirstOrDefault();
                DodajMEKViewModel model = new DodajMEKViewModel();
                model.IdKEK = IdKEK;
                model.Filtr = "";
                IEnumerable<MEK> MEKI = db.MEKI.Where(m => (m.IdPoziomu == k.IdPoziomu && m.IdProfilu == k.IdProfilu &&
                                        db.UdzialyProcentowe.Where(u => u.IdKierunku == k.IdKierunku &&
                                            u.IdObszaru == m.IdObszaru).FirstOrDefault() != null)
                                             && (!db.Sladowania.Any(slad => slad.IdKEK == model.IdKEK
                                             && m.IdMEK == slad.IdMEK))).ToList();
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
            var IdKierunku = Convert.ToInt32(this.HttpContext.Session["IdKierunku"]);
            Kierunek k = db.Kierunki.Where(kierunek => kierunek.IdKierunku == IdKierunku).FirstOrDefault();
            IEnumerable<MEK> MEKI = db.MEKI.Where(m => ((m.IdPoziomu == k.IdPoziomu && m.IdProfilu == k.IdProfilu &&
                                                (db.UdzialyProcentowe.Where(u => u.IdKierunku == k.IdKierunku && u.IdObszaru == m.IdObszaru).FirstOrDefault() != null)) && (m.Kod.Contains(model.Filtr)
                                                || m.Opis.Contains(model.Filtr))
                                                && (!db.Sladowania.Any(slad => slad.IdKEK == model.IdKEK
                                                && m.IdMEK == slad.IdMEK)))).ToList(); 
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
            Sladowanie slad = db.Sladowania.Where(s => (s.IdMEK == IdMEK && s.IdKEK == IdKEK)).FirstOrDefault();

            if (slad != null)
            {
                db.Sladowania.Remove(slad);
                db.SaveChanges();
            }
            return RedirectToAction("MacierzSladowania");
        }



        public ActionResult DodajKEKPrzedmiotu(int IdKEK)
        {
            var IdKierunku = Convert.ToInt32(this.HttpContext.Session["IdKierunku"]);
            Kierunek k = db.Kierunki.Where(kierunek => kierunek.IdKierunku == IdKierunku).FirstOrDefault();
            DodajKEKPrzedmiotuViewModel model = new DodajKEKPrzedmiotuViewModel();
            model.IdKEK = IdKEK;
            model.Filtr = "";
            IEnumerable<Przedmiot> Przedmioty = db.Przedmioty.Where(p => (p.Kategoria.ProgramStudiow.ProgramKsztalcenia.Kierunek.IdKierunku == k.IdKierunku)
                && (!db.KEKIPrzedmiotow.Any(kekp => kekp.IdKEK == IdKEK
                && kekp.IdPrzedmiotu == p.IdPrzedmiotu)));
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
            var IdKierunku = Convert.ToInt32(this.HttpContext.Session["IdKierunku"]);
            Kierunek k = db.Kierunki.Where(kierunek => kierunek.IdKierunku == IdKierunku).FirstOrDefault();
            IEnumerable<Przedmiot> Przedmioty = db.Przedmioty.Where(p => (p.Kategoria.ProgramStudiow.ProgramKsztalcenia.Kierunek.IdKierunku == k.IdKierunku
                                                                                     && ((p.KodPrzedmiotu.Contains(model.Filtr) || p.NazwaPrzedmiotu.Contains(model.Filtr))
                                                                                     && (!db.KEKIPrzedmiotow.Any(kekp => kekp.IdKEK == model.IdKEK
                                                                                     && kekp.IdPrzedmiotu == p.IdPrzedmiotu)))));
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
            KEKPrzedmiotu kp = db.KEKIPrzedmiotow.Where(k => (k.IdPrzedmiotu == IdPrzedmiotu && k.IdKEK == IdKEK)).FirstOrDefault();

            if (kp != null)
            {
                db.KEKIPrzedmiotow.Remove(kp);
                db.SaveChanges();
            }
            return RedirectToAction("MacierzSladowania");
        }
	}
}