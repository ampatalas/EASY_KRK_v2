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

        [HttpPost]
        public ActionResult Index(KategorieViewModel Model, string Dodaj, string Edytuj, string Usun)
        {
            if (Edytuj != null && Model.IdKategorii != 0)
            {
                return RedirectToAction("EdytujKategorie", new { IdKategorii = Model.IdKategorii });
            }
            else if (Usun != null && Model.IdKategorii != 0)
            {
                return RedirectToAction("UsunKategorie", new { IdKategorii = Model.IdKategorii });
            }
            else if (Dodaj != null && Model.IdKategorii != 0)
            {
                return RedirectToAction("DodajKategorie", new { IdKategorii = Model.IdKategorii });
            }

            return Index();
        }

        [HttpPost]
        public ActionResult EdytujKategorie(int IdKategorii)
        {
            return Index();
        }

        public ActionResult DodajKategorie(int IdKategorii)
        {
            DodajKategorieViewModel Model = new DodajKategorieViewModel();
            Model.KategoriaNadrzedna = db.Kategorie.Find(IdKategorii);
            this.HttpContext.Session["IdKategorii"] = Model.KategoriaNadrzedna.IdKategorii;
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DodajKategorie(DodajKategorieViewModel Model, string Dodaj, string Anuluj)
        {
            if (Anuluj != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (Model.Kategoria.NazwaKategorii == null ||
                    (Model.Kategoria.MinECTS.HasValue && Model.Kategoria.MinECTS < 1) ||
                    (Model.Kategoria.MinECTS.HasValue ^ Model.Kategoria.zawieraPrzedmioty))
                {
                    return DodajKategorie(Convert.ToInt32(this.HttpContext.Session["IdKategorii"]));
                }
                else
                {
                    Model.Kategoria.IdKategoriiNadrzednej = Convert.ToInt32(this.HttpContext.Session["IdKategorii"]);
                    Model.Kategoria.IdProgramuStudiow = Convert.ToInt32(this.HttpContext.Session["IdProgramu"]);
                    db.Kategorie.Add(Model.Kategoria);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

        }

        public ActionResult StatusKategorii(int IdKategorii)
        {
            Kategoria kategoria = db.Kategorie.Find(IdKategorii);
            return Json(new { success = true, hasValue = kategoria.MinECTS.HasValue },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult UsunKategorie(int IdKategorii)
        {
            UsunKategorieDb(IdKategorii);
            return RedirectToAction("Index");
        }

        public void UsunKategorieDb(int IdKategorii)
        {
            Kategoria Kategoria = db.Kategorie.Find(IdKategorii);
            List<Kategoria> Kategorie = Kategoria.Kategorie.ToList();
            List<Przedmiot> Przedmioty = Kategoria.Przedmioty.ToList();

            for (var i = 0; i < Przedmioty.Count; i++)
            {
                PrzedmiotController przedmiotController = DependencyResolver.Current.GetService<PrzedmiotController>();
                przedmiotController.UsunPrzedmiotDb(Przedmioty[i].IdPrzedmiotu);
            }

            for (var i = 0; i < Kategorie.Count; i++)
            {
                UsunKategorieDb(Kategorie[i].IdKategorii);
            }

            db.Kategorie.Remove(db.Kategorie.Find(IdKategorii));
            db.SaveChanges();
        }
	}
}