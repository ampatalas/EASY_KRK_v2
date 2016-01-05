
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
            Model.Kategorie = db.Kategorie.ToList().FindAll(k => k.ProgramStudiow.IdProgramuKsztalcenia == Convert.ToInt32(this.HttpContext.Session["IdProgramu"])
                                                                 && k.KategoriaNadrzedna == null);
            Model.IdPrzedmiotu = 0;
            return View(Model);
        }

        public ActionResult DodajPrzedmiot(int IdKategorii)
        {
            DodajPrzedmiotViewModel Model = new DodajPrzedmiotViewModel();
            Model.Kategorie = new SelectList(db.Kategorie.ToList().FindAll(k => k.ProgramStudiow.IdProgramuKsztalcenia == Convert.ToInt32(this.HttpContext.Session["IdProgramu"])
                                                                 && k.Kategorie.Count() == 0), "IdKategorii", "NazwaKategorii", IdKategorii);
            Model.FormyPrzedmiotu = new SelectList(db.FormyPrzedmiotu, "IdFormyPrzedmiotu", "NazwaFormy");
            Model.FormyZaliczenia = new SelectList(db.FormyZal, "IdFormyZal", "NazwaFormyZal");
            Model.Rodzaje = new SelectList(db.RodzajePrzedmioty, "IdRodzaju", "NazwaRodzaju");
            Model.Typy = new SelectList(db.TypyPrzedmiotu, "IdTypu", "NazwaTypu");
            Model.Kursy = new List<Kurs>();
            Model.Grupa = new List<Boolean>();
            
            foreach(FormaZajec f in db.FormyZajec.ToList().FindAll(forma => forma.NazwaFormy != "Praktyka"))
            {
                Kurs k = new Kurs();
                k.IdFormyZajec = f.IdFormyZajec;
                k.FormaZajec = f;
                Model.Kursy.Add(k);
                Model.Grupa.Add(false);
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

                    if (Model.Grupa.Contains(true))
                    {
                        Grupa = new GrupaKursow();
                        Grupa.KodGrupyKursow = Model.Przedmiot.KodPrzedmiotu;
                        Grupa.IdPrzedmiotu = Model.Przedmiot.IdPrzedmiotu;
                    }

                    for (var i = 0; i < Model.Kursy.Count(); i++ )
                    {
                        if (Model.Kursy[i].ZZU > 0)
                        {
                            if (Model.Grupa[i])
                            {
                                Grupa.ZZU += Model.Kursy[i].ZZU;
                                Grupa.CNPS += Model.Kursy[i].CNPS;
                                Grupa.PunktyECTS += Model.Kursy[i].PunktyECTS;
                                Grupa.ECTS_BK += Model.Kursy[i].ECTS_BK;
                                Grupa.ECTS_P += Model.Kursy[i].ECTS_P;
                                Grupa.KodGrupyKursow += db.FormyZajec.Find(Model.Kursy[i].IdFormyZajec).NazwaFormy[0];

                                if (Model.Kursy[i].Praktyczny)
                                {
                                    Grupa.Praktyczny = true;
                                }

                            }
                            Model.Kursy[i].KodKursu = Model.Przedmiot.KodPrzedmiotu + db.FormyZajec.Find(Model.Kursy[i].IdFormyZajec).NazwaFormy[0];
                            Model.Kursy[i].IdPrzedmiotu = Model.Przedmiot.IdPrzedmiotu;                           
                        }


                    }
                    if (Grupa != null)
                    {
                        db.GrupyKursow.Add(Grupa);
                        db.SaveChanges();
                    }


                        for (var i = 0; i < Model.Kursy.Count() && Model.Kursy[i].ZZU > 0; i++)
                        {
                            if (Model.Grupa[i])
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
                    return View(Model);
                }

                
            }

            
        }
	}
}