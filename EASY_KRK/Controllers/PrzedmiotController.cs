using EASY_KRK.App_Start;
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
            Model.Kategorie = db.Kategorie.ToList().FindAll(k => k.ProgramStudiow.IdProgramuKsztalcenia == Convert.ToInt32(this.HttpContext.Session["IdProgramu"])
                                                                 && k.KategoriaNadrzedna == null);
            Model.IdPrzedmiotu = 0;
            return View(Model);
        }

        [HttpPost]
        public ActionResult Index(PrzedmiotyViewModel Model, string Edytuj, string Przypisz, string Usun)
        {
            if (Edytuj != null)
            {
                return RedirectToAction("EdytujPrzedmiot", new { IdPrzedmiotu = Model.IdPrzedmiotu });
            }
            else if (Usun != null)
            {
                return RedirectToAction("UsunPrzedmiot", new { IdPrzedmiotu = Model.IdPrzedmiotu });
            }
            else if (Przypisz != null)
            {
                return RedirectToAction("PrzypiszPrzedmiot", new { IdPrzedmiotu = Model.IdPrzedmiotu });
            }

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

        public ActionResult EdytujPrzedmiot(int IdPrzedmiotu)
        {
            DodajPrzedmiotViewModel Model = new DodajPrzedmiotViewModel();
            Kurs Kurs = null;
            Model.Przedmiot = db.Przedmioty.Find(IdPrzedmiotu);
            Model.Kategorie = new SelectList(db.Kategorie.ToList().FindAll(k => k.ProgramStudiow.IdProgramuKsztalcenia == Convert.ToInt32(this.HttpContext.Session["IdProgramu"])
                                                                 && k.Kategorie.Count() == 0), "IdKategorii", "NazwaKategorii", Model.Przedmiot.IdKategorii);
            Model.FormyPrzedmiotu = new SelectList(db.FormyPrzedmiotu, "IdFormyPrzedmiotu", "NazwaFormy", Model.Przedmiot.IdFormyPrzedmiotu);
            Model.FormyZaliczenia = new SelectList(db.FormyZal, "IdFormyZal", "NazwaFormyZal");
            Model.Rodzaje = new SelectList(db.RodzajePrzedmioty, "IdRodzaju", "NazwaRodzaju", Model.Przedmiot.IdRodzajuPrzedmiotu);
            Model.Typy = new SelectList(db.TypyPrzedmiotu, "IdTypu", "NazwaTypu", Model.Przedmiot.IdTypuPrzedmiotu);
            Model.Kursy = new List<Kurs>();
            Model.Grupa = new List<Boolean>();

            foreach (FormaZajec f in db.FormyZajec.ToList().FindAll(forma => forma.NazwaFormy != "Praktyka"))
            {
                Kurs = db.Kursy.ToList().Find(k => k.IdPrzedmiotu == IdPrzedmiotu && k.IdFormyZajec == f.IdFormyZajec);
                if (Kurs != null)
                {
                    Model.Grupa.Add(Kurs.IdGrupyKursow != null);
                }
                else
                {
                    Kurs = new Kurs();
                    Kurs.IdFormyZajec = f.IdFormyZajec;
                    Kurs.FormaZajec = f;
                    Model.Grupa.Add(false);
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
                    Grupa = db.GrupyKursow.ToList().Find(g => g.IdPrzedmiotu == Model.Przedmiot.IdPrzedmiotu);

                    if (Model.Grupa.Contains(true))
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
                        if (Model.Kursy[i].ZZU > 0)
                        {
                            if (Model.Grupa[i])
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


                    for (var i = 0; i < Model.Kursy.Count() && Model.Kursy[i].ZZU > 0; i++)
                    {
                        if (Model.Grupa[i])
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
                        if (Model.Grupa.Contains(true))
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

            return View(Model);
        }

        public ActionResult UsunPrzedmiot(int IdPrzedmiotu)
        {
            Przedmiot p = db.Przedmioty.Find(IdPrzedmiotu);
            List<Kurs> Kursy = p.Kursy.ToList();
            List<GrupaKursow> Grupy = p.GrupyKursow.ToList();

            for (var i = 0; i < Kursy.Count - 1; i++ )
            {
                db.Kursy.Remove(Kursy[i]);
            }

            for (var j = 0; j < Kursy.Count - 1; j++)
            {
                db.GrupyKursow.Remove(Grupy[j]);
            }

            db.Przedmioty.Remove(db.Przedmioty.Find(IdPrzedmiotu));

            db.SaveChanges();

            return RedirectToAction("Index");
        }
	}
}