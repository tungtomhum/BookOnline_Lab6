using SachOnline.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace SachOnline.Areas.Admin.Controllers
{
    public class ChuDeController : Controller
    {
        dbSachOnlineDataContext db = new dbSachOnlineDataContext("Data Source=LAPTOP-SD6JFUCG\\MSSQLSERVER01;Initial Catalog=SachOnline;Integrated Security=True");
        // GET: Admin/ChuDe
        public ActionResult Index(int? page)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            return View(db.CHUDEs.ToList().OrderBy(n => n.MaCD).ToPagedList(iPageNum, iPageSize));
        }


        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.MaCD = new SelectList(db.CHUDEs.ToList().OrderBy(n => n.TenChuDe), "MaCD", "TenChuDe");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(CHUDE chude)
        {
            if (ModelState.IsValid)
            {
                db.CHUDEs.InsertOnSubmit(chude);
                db.SubmitChanges();
                // Chuyển đến trang Index sau khi thêm chủ đề
                return RedirectToAction("Index");
            }

            return View(chude);
        }

        public ActionResult Details(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            return View(chude);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                return HttpNotFound();
            }
            return View(chude);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id, FormCollection f)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                Response.StatusCode = 404;
                return null;
            }

            var sach = db.SACHes.Where(s => s.MaCD == id);
            if (sach.Count() > 0)
            {
                ViewBag.ThongBao = "Không thể xóa chủ đề này vì có sách liên quan đến nó. Hãy xóa các sách trước khi xóa chủ đề.";
                return View(chude);
            }

            db.CHUDEs.DeleteOnSubmit(chude);
            db.SubmitChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var chude = db.CHUDEs.SingleOrDefault(n => n.MaCD == id);
            if (chude == null)
            {
                return HttpNotFound();
            }

            return View(chude);
        }

        [HttpPost]
        public ActionResult Edit(CHUDE chude)
        {
            if (ModelState.IsValid)
            {
                var existingChude = db.CHUDEs.SingleOrDefault(n => n.MaCD == chude.MaCD);

                if (existingChude != null)
                {
                    existingChude.TenChuDe = chude.TenChuDe;

                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
            }

            return View(chude);
        }




    }
}