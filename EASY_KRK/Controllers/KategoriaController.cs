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

        public ActionResult EdytujKategorie(int IdKategorii)
        {
            DodajKategorieViewModel Model = new DodajKategorieViewModel();
            Model.Kategoria = db.Kategorie.Find(IdKategorii);
            Model.KategoriaNadrzedna = db.Kategorie.Find(Model.Kategoria.IdKategoriiNadrzednej);
            this.HttpContext.Session["IdKategorii"] = Model.Kategoria.IdKategorii;
            return View(Model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EdytujKategorie(DodajKategorieViewModel Model, string Edytuj, string Anuluj)
        {
            if (Anuluj != null)
            {
                return RedirectToAction("Index");
            }
            else
            {
                if (Model.Kategoria.NazwaKategorii == null ||
                    (Model.Kategoria.MinECTS.HasValue && Model.Kategoria.MinECTS < 1))
                {
                    return EdytujKategorie(Convert.ToInt32(this.HttpContext.Session["IdKategorii"]));
                }
                else
                {
                    Model.Kategoria.IdProgramuStudiow = Convert.ToInt32(this.HttpContext.Session["IdProgramu"]);
                    db.Entry(Model.Kategoria).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }

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
            DeleteUtils.UsunKategorieDb(db, IdKategorii);
            return RedirectToAction("Index");
        }
	}
}