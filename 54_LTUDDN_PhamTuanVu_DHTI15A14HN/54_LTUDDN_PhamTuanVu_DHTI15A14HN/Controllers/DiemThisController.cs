using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using _54_LTUDDN_PhamTuanVu_DHTI15A14HN.Models;

namespace _54_LTUDDN_PhamTuanVu_DHTI15A14HN.Controllers
{
    public class DiemThisController : Controller
    {
        private QLSVDataContext db = new QLSVDataContext();

        // GET: DiemThis
        public ActionResult Index()
        {
            var diemThis = db.DiemThis.Include(d => d.SinhVien);
            return View(diemThis.ToList());
        }

        public ActionResult TimKiem(string search)
        {
            var diemThis = db.DiemThis.Include(d => d.SinhVien);
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower().Trim();
                diemThis = diemThis.Where(d => d.SinhVien.TenSV.ToLower().Trim().Contains(search));
            }
            return View(diemThis.ToList());
        }
        public ActionResult MinPoint()
        {
            var wordExamScores = db.DiemThis.Include(d => d.SinhVien)
                                            .Where(d => d.TenMT == "Word")
                                            .OrderByDescending(d => d.Diem)
                                            .ToList();

            return View(wordExamScores);
        }

        // GET: DiemThis/Details/5
        public ActionResult Details(int? id,string id2)
        {
            if (id == null && id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemThi diemThi = db.DiemThis.Find(id,id2);
            if (diemThi == null)
            {
                return HttpNotFound();
            }
            return View(diemThi);
        }

        // GET: DiemThis/Create
        public ActionResult Create()
        {
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "TenSV");
            return View();
        }

        // POST: DiemThis/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSV,TenMT,SoTC,NgayThi,Diem")] DiemThi diemThi)
        {
            if (ModelState.IsValid)
            {
                db.DiemThis.Add(diemThi);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "TenSV", diemThi.MaSV);
            return View(diemThi);
        }

        // GET: DiemThis/Edit/5
        public ActionResult Edit(int? id,string id2)
        {
            if (id == null && id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemThi diemThi = db.DiemThis.Find(id,id2);
            if (diemThi == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "TenSV", diemThi.MaSV);
            return View(diemThi);
        }

        // POST: DiemThis/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSV,TenMT,SoTC,NgayThi,Diem")] DiemThi diemThi)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diemThi).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSV = new SelectList(db.SinhViens, "MaSV", "TenSV", diemThi.MaSV);
            return View(diemThi);
        }

        // GET: DiemThis/Delete/5
        public ActionResult Delete(int? id, string id2)
        {
            if (id == null && id2 == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiemThi diemThi = db.DiemThis.Find(id,id2);
            if (diemThi == null)
            {
                return HttpNotFound();
            }
            return View(diemThi);
        }

        // POST: DiemThis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiemThi diemThi = db.DiemThis.Find(id);
            db.DiemThis.Remove(diemThi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
