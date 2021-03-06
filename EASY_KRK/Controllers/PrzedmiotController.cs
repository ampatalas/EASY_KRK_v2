﻿using EASY_KRK.App_Start;
using EASY_KRK.Controllers.Utils;
using EASY_KRK.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var IdProgramu =  Convert.ToInt32(this.HttpContext.Session["IdProgramu"]);
            Model.Kategorie = db.Kategorie.Where(k => k.ProgramStudiow.IdProgramuKsztalcenia == IdProgramu
                                                        && k.KategoriaNadrzedna == null);
            Model.IdPrzedmiotu = 0;
            return View(Model);
        }

        [HttpPost]
        public ActionResult Index(PrzedmiotyViewModel Model, string Edytuj, string Przypisz, string Usun)
        {
            if (Edytuj != null && Model.IdPrzedmiotu != 0)
            {
                return RedirectToAction("EdytujPrzedmiot", new { IdPrzedmiotu = Model.IdPrzedmiotu });
            }
            else if (Usun != null && Model.IdPrzedmiotu != 0)
            {
                return RedirectToAction("UsunPrzedmiot", new { IdPrzedmiotu = Model.IdPrzedmiotu });
            }
            else if (Przypisz != null && Model.IdPrzedmiotu != 0)
            {
                return RedirectToAction("PrzypiszKEK", new { IdPrzedmiotu = Model.IdPrzedmiotu });
            }

            return Index();
        }

        public ActionResult PrzypiszKEK(int IdPrzedmiotu, bool Edycja)
        {
            var IdKierunku = Convert.ToInt32(this.HttpContext.Session["IdKierunku"]);
            Kierunek k = db.Kierunki.Where(kierunek => kierunek.IdKierunku == IdKierunku).First();
            PrzypiszKEKViewModel model = new PrzypiszKEKViewModel();
            model.IdPrzedmiotu = IdPrzedmiotu;
            model.NazwaPrzedmiotu = db.Przedmioty.Find(IdPrzedmiotu).NazwaPrzedmiotu;
            model.Filtr = "";
            model.Edycja = Edycja;
            IEnumerable<KEK> KEKI = db.KEKI.Where(kek => (kek.IdKierunku == k.IdKierunku)
                && (!db.KEKIPrzedmiotow.Any(kekp => kekp.IdKEK == kek.IdKEK
                     && kekp.IdPrzedmiotu == IdPrzedmiotu)));
            List<object> newKEKI = new List<Object>();
            foreach (KEK m in KEKI)
            {
                newKEKI.Add(new
                {
                    Id = m.IdKEK,
                    Nazwa = m.Kod + " " + m.Opis
                });
            }
            model.KEKI = new SelectList(newKEKI, "Id", "Nazwa");

            return PartialView(model);
        }

        [HttpPost]
        public ActionResult FiltrujKEK(PrzypiszKEKViewModel model)
        {
            var IdKierunku = Convert.ToInt32(this.HttpContext.Session["IdKierunku"]);
            Kierunek k = db.Kierunki.Where(kierunek => kierunek.IdKierunku == IdKierunku).First();
            IEnumerable<KEK> KEKI = db.KEKI.Where(kek => (kek.IdKierunku == k.IdKierunku) && ((kek.Kod.Contains(model.Filtr)
                                                || kek.Opis.Contains(model.Filtr))
                                                 && (!db.KEKIPrzedmiotow.Any(kekp => kekp.IdKEK == kek.IdKEK
                                                     && kekp.IdPrzedmiotu == model.IdPrzedmiotu))));
            List<object> KEKILista = new List<Object>();
            foreach (KEK m in KEKI)
            {
                KEKILista.Add(new
                {
                    Id = m.IdKEK,
                    Nazwa = m.Kod + " " + m.Opis
                });
            }
            model.KEKI = new SelectList(KEKILista, "Id", "Nazwa");
            model.Filtr = "";

            return PartialView("ListaKEK", model);
        }

        public ActionResult PrzypiszKEKForm(PrzypiszKEKViewModel Model)
        {

            if (ModelState.IsValid)
            {
                KEKPrzedmiotu kekp = new KEKPrzedmiotu();
                kekp.IdKEK = Model.SelectedKEK;
                kekp.IdPrzedmiotu = Model.IdPrzedmiotu;
                db.KEKIPrzedmiotow.Add(kekp);
                db.SaveChanges();
            }

            if (Model.Edycja)
            {
                return RedirectToAction("EdytujPrzedmiot", new { IdPrzedmiotu = Model.IdPrzedmiotu });
            }
            return RedirectToAction("Index");
        }

        public ActionResult UsunKEK(int IdPrzedmiotu, int IdKEK)
        {

            if (ModelState.IsValid)
            {
                KEKPrzedmiotu kekp = db.KEKIPrzedmiotow.Where(k => k.IdPrzedmiotu == IdPrzedmiotu
                                                              && k.IdKEK == IdKEK).First();
                db.KEKIPrzedmiotow.Remove(kekp);
                db.SaveChanges();
            }

            return RedirectToAction("EdytujPrzedmiot", new { IdPrzedmiotu = IdPrzedmiotu });
        }

        public ActionResult DodajPrzedmiot(int IdKategorii)
        {
            var IdProgramu = Convert.ToInt32(this.HttpContext.Session["IdProgramu"]);
            DodajPrzedmiotViewModel Model = new DodajPrzedmiotViewModel();
            Model.Kategorie = new SelectList(db.Kategorie.Where(k => k.ProgramStudiow.IdProgramuKsztalcenia == IdProgramu
                                                                 && k.MinECTS.HasValue), "IdKategorii", "NazwaKategorii", IdKategorii);
            Model.FormyPrzedmiotu = new SelectList(db.FormyPrzedmiotu, "IdFormyPrzedmiotu", "NazwaFormy");
            Model.FormyZaliczenia = new SelectList(db.FormyZal, "IdFormyZal", "NazwaFormyZal");
            Model.Rodzaje = new SelectList(db.RodzajePrzedmioty, "IdRodzaju", "NazwaRodzaju");
            Model.Typy = new SelectList(db.TypyPrzedmiotu, "IdTypu", "NazwaTypu");
            Model.Kursy = new List<Kurs>();
            
            
            foreach(FormaZajec f in db.FormyZajec.Where(forma => forma.NazwaFormy != "Praktyka"))
            {
                Kurs k = new Kurs();
                k.IdFormyZajec = f.IdFormyZajec;
                k.FormaZajec = f;
                k.CzyNalezyDoPrzedmiotu = false;
                k.CzyGrupa = false;
                Model.Kursy.Add(k);
            }

            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult DodajPrzedmiot(DodajPrzedmiotViewModel Model, string Dodaj, string Anuluj)
        {
            GrupaKursow Grupa = null;
            if (Anuluj != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (ModelState.IsValid)
                {

                    db.Przedmioty.Add(Model.Przedmiot);
                    db.SaveChanges();

                    if (Model.Kursy.Exists(k => k.CzyGrupa))
                    {
                        Grupa = new GrupaKursow();
                        Grupa.KodGrupyKursow = Model.Przedmiot.KodPrzedmiotu;
                        Grupa.IdPrzedmiotu = Model.Przedmiot.IdPrzedmiotu;
                    }

                    for (var i = 0; i < Model.Kursy.Count(); i++ )
                    {
                        if (Model.Kursy[i].CzyNalezyDoPrzedmiotu)
                        {
                            if (Model.Kursy[i].CzyGrupa)
                            {
                                Grupa.ZZU += Model.Kursy[i].ZZU;
                                Grupa.CNPS += Model.Kursy[i].CNPS;
                                Grupa.PunktyECTS += Model.Kursy[i].PunktyECTS;
                                Grupa.ECTS_BK += Model.Kursy[i].ECTS_BK;
                                Grupa.ECTS_P += Model.Kursy[i].ECTS_P;
                                Grupa.KodGrupyKursow += Util.IgnorujPolskieZnaki(db.FormyZajec.Find(Model.Kursy[i].IdFormyZajec).NazwaFormy[0].ToString());

                                if (Model.Kursy[i].Praktyczny)
                                {
                                    Grupa.Praktyczny = true;
                                }

                            }
                            Model.Kursy[i].KodKursu = Model.Przedmiot.KodPrzedmiotu + Util.IgnorujPolskieZnaki(db.FormyZajec.Find(Model.Kursy[i].IdFormyZajec).NazwaFormy[0].ToString());
                            Model.Kursy[i].IdPrzedmiotu = Model.Przedmiot.IdPrzedmiotu;                           
                        }

                    }
                    if (Grupa != null)
                    {
                        db.GrupyKursow.Add(Grupa);
                        db.SaveChanges();
                    }

                    for (var i = 0; i < Model.Kursy.Count() && Model.Kursy[i].CzyNalezyDoPrzedmiotu; i++)
                        {
                            if (Model.Kursy[i].CzyGrupa)
                            {
                                Model.Kursy[i].IdGrupyKursow = Grupa.IdGrupyKursow;
                            }

                            db.Kursy.Add(Model.Kursy[i]);
                        }

                    db.SaveChanges();
                    
                    return RedirectToAction("Index");
                }
                else
                {
                    var IdProgramu = Convert.ToInt32(this.HttpContext.Session["IdProgramu"]);
                    foreach(var k in Model.Kursy)
                    {
                        k.FormaZajec = db.FormyZajec.Find(k.IdFormyZajec);
                    }
                    Model.Kategorie = new SelectList(db.Kategorie.Where(k => k.ProgramStudiow.IdProgramuKsztalcenia == IdProgramu
                                                     && k.MinECTS.HasValue), "IdKategorii", "NazwaKategorii", Model.Przedmiot.IdKategorii);
                    Model.FormyPrzedmiotu = new SelectList(db.FormyPrzedmiotu, "IdFormyPrzedmiotu", "NazwaFormy");
                    Model.FormyZaliczenia = new SelectList(db.FormyZal, "IdFormyZal", "NazwaFormyZal");
                    Model.Rodzaje = new SelectList(db.RodzajePrzedmioty, "IdRodzaju", "NazwaRodzaju");
                    Model.Typy = new SelectList(db.TypyPrzedmiotu, "IdTypu", "NazwaTypu");
                    return View(Model);
                }
            }
           
        }

        public ActionResult EdytujPrzedmiot(int IdPrzedmiotu)
        {
            var IdProgramu = Convert.ToInt32(this.HttpContext.Session["IdProgramu"]);
            DodajPrzedmiotViewModel Model = new DodajPrzedmiotViewModel();
            Kurs Kurs = null;
            Model.Przedmiot = db.Przedmioty.Find(IdPrzedmiotu);
            Model.Kategorie = new SelectList(db.Kategorie.Where(k => k.ProgramStudiow.IdProgramuKsztalcenia == IdProgramu
                                                                 && k.Kategorie.Count() == 0), "IdKategorii", "NazwaKategorii", Model.Przedmiot.IdKategorii);
            Model.FormyPrzedmiotu = new SelectList(db.FormyPrzedmiotu, "IdFormyPrzedmiotu", "NazwaFormy", Model.Przedmiot.IdFormyPrzedmiotu);
            Model.FormyZaliczenia = new SelectList(db.FormyZal, "IdFormyZal", "NazwaFormyZal");
            Model.Rodzaje = new SelectList(db.RodzajePrzedmioty, "IdRodzaju", "NazwaRodzaju", Model.Przedmiot.IdRodzajuPrzedmiotu);
            Model.Typy = new SelectList(db.TypyPrzedmiotu, "IdTypu", "NazwaTypu", Model.Przedmiot.IdTypuPrzedmiotu);
            Model.Kursy = new List<Kurs>();
            Model.KEKI = db.KEKIPrzedmiotow.Where(k => k.IdPrzedmiotu == IdPrzedmiotu).Select(k => k.KEK).ToList();

            foreach (FormaZajec f in db.FormyZajec.Where(forma => forma.NazwaFormy != "Praktyka"))
            {
                Kurs = db.Kursy.Where(k => k.IdPrzedmiotu == IdPrzedmiotu && k.IdFormyZajec == f.IdFormyZajec).FirstOrDefault();
                if (Kurs != null)
                {
                    Kurs.CzyNalezyDoPrzedmiotu = true;
                    Kurs.CzyGrupa = Kurs.IdGrupyKursow != null;
                }
                else
                {
                    Kurs = new Kurs();
                    Kurs.IdFormyZajec = f.IdFormyZajec;
                    Kurs.FormaZajec = f;
                    Kurs.CzyNalezyDoPrzedmiotu = false;
                    Kurs.CzyGrupa = false;
                }

                Model.Kursy.Add(Kurs);
            }

            return View(Model);
        }

        [HttpPost]
        public ActionResult EdytujPrzedmiot(DodajPrzedmiotViewModel Model, string Edytuj, string Anuluj)
        {
            
            if (Anuluj != null)
            {
                return RedirectToAction("Index");
            }
            else if (Edytuj != null)
            {
                GrupaKursow Grupa = null;
                if (ModelState.IsValid)
                {
                    Grupa = db.GrupyKursow.Where(g => g.IdPrzedmiotu == Model.Przedmiot.IdPrzedmiotu).FirstOrDefault();

                    if (Model.Kursy.Exists(k => k.CzyGrupa))
                    {
                        if (Grupa != null)
                        {
                            Grupa.ZZU = 0;
                            Grupa.CNPS = 0;
                            Grupa.PunktyECTS = 0;
                            Grupa.ECTS_BK = 0;
                            Grupa.ECTS_P = 0;
                        }
                        else
                        {                          
                            Grupa = new GrupaKursow();                            
                            Grupa.IdPrzedmiotu = Model.Przedmiot.IdPrzedmiotu;
                        }

                        Grupa.KodGrupyKursow = Model.Przedmiot.KodPrzedmiotu;
                    }

                    for (var i = 0; i < Model.Kursy.Count(); i++)
                    {
                        if (Model.Kursy[i].CzyNalezyDoPrzedmiotu)
                        {
                            if (Model.Kursy[i].CzyGrupa)
                            {
                                Grupa.ZZU += Model.Kursy[i].ZZU;
                                Grupa.CNPS += Model.Kursy[i].CNPS;
                                Grupa.PunktyECTS += Model.Kursy[i].PunktyECTS;
                                Grupa.ECTS_BK += Model.Kursy[i].ECTS_BK;
                                Grupa.ECTS_P += Model.Kursy[i].ECTS_P;
                                Grupa.KodGrupyKursow += Util.IgnorujPolskieZnaki(db.FormyZajec.Find(Model.Kursy[i].IdFormyZajec).NazwaFormy[0].ToString());

                                if (Model.Kursy[i].Praktyczny)
                                {
                                    Grupa.Praktyczny = true;
                                }

                            }
                            Model.Kursy[i].KodKursu = Model.Przedmiot.KodPrzedmiotu + Util.IgnorujPolskieZnaki(db.FormyZajec.Find(Model.Kursy[i].IdFormyZajec).NazwaFormy[0].ToString());
                            Model.Kursy[i].IdPrzedmiotu = Model.Przedmiot.IdPrzedmiotu;
                        }
                        else
                        {
                            if (Model.Kursy[i].IdKursu != 0)
                            {
                                db.Entry(Model.Kursy[i]).State = EntityState.Modified;
                                db.Kursy.Remove(Model.Kursy[i]);
                            }
                        }
                        
                    }
                    if (Grupa != null && Grupa.IdGrupyKursow == 0)
                    {
                        db.GrupyKursow.Add(Grupa);
                        db.SaveChanges();   
                    }

                    for (var i = 0; i < Model.Kursy.Count() && Model.Kursy[i].CzyNalezyDoPrzedmiotu; i++)
                    {
                        if (Model.Kursy[i].CzyGrupa)
                        {
                            Model.Kursy[i].IdGrupyKursow = Grupa.IdGrupyKursow;
                        }
                        else
                        {
                            Model.Kursy[i].IdGrupyKursow = null;
                        }

                        if (Model.Kursy[i].IdKursu == 0)
                        {
                            db.Kursy.Add(Model.Kursy[i]);
                        }
                        else
                        {
                            db.Entry(Model.Kursy[i]).State = EntityState.Modified;
                        }
                        
                    }
                    db.Entry(Model.Przedmiot).State = EntityState.Modified;
                    db.SaveChanges();

                    if (Grupa != null && Grupa.IdGrupyKursow != 0)
                    {
                        if (Model.Kursy.Exists(k => k.CzyGrupa))
                        {
                            db.Entry(Grupa).State = EntityState.Modified;
                        }
                        else
                        {
                            db.GrupyKursow.Remove(Grupa);
                        }    
                    }

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }                  
                              
            }

            Model.KEKI = db.KEKIPrzedmiotow.Where(k => k.IdPrzedmiotu == Model.Przedmiot.IdPrzedmiotu).Select(k => k.KEK).ToList();
            var IdProgramu = Convert.ToInt32(this.HttpContext.Session["IdProgramu"]);
            foreach (var k in Model.Kursy)
            {
                k.FormaZajec = db.FormyZajec.Find(k.IdFormyZajec);
            }
            Model.Kategorie = new SelectList(db.Kategorie.Where(k => k.ProgramStudiow.IdProgramuKsztalcenia == IdProgramu
                                             && k.MinECTS.HasValue), "IdKategorii", "NazwaKategorii", Model.Przedmiot.IdKategorii);
            Model.FormyPrzedmiotu = new SelectList(db.FormyPrzedmiotu, "IdFormyPrzedmiotu", "NazwaFormy");
            Model.FormyZaliczenia = new SelectList(db.FormyZal, "IdFormyZal", "NazwaFormyZal");
            Model.Rodzaje = new SelectList(db.RodzajePrzedmioty, "IdRodzaju", "NazwaRodzaju");
            Model.Typy = new SelectList(db.TypyPrzedmiotu, "IdTypu", "NazwaTypu");

            return View(Model);
        }

        public ActionResult UsunPrzedmiot(int IdPrzedmiotu)
        {
            DeleteUtils.UsunPrzedmiotDb(db, IdPrzedmiotu);
            return RedirectToAction("Index");
        }
	}
}