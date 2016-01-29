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
        public ActionResult Index(PrzedmiotyViewModel Model, string Dodaj, string Edytuj, string Usun)
        {
            return Index();
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

            for (var i = 0; i < Przedmioty.Count - 1; i++)
            {
                PrzedmiotController przedmiotController = DependencyResolver.Current.GetService<PrzedmiotController>();
                przedmiotController.UsunPrzedmiotDb(Przedmioty[i].IdPrzedmiotu);
            }

            for (var i = 0; i < Kategorie.Count - 1; i++)
            {
                UsunKategorieDb(Kategorie[i].IdKategorii);
            }

            db.Kategorie.Remove(db.Kategorie.Find(IdKategorii));
            db.SaveChanges();
        }
	}
}