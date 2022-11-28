using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using QLBanhang.Models;

namespace QLBanhang.Controllers
{
    public class HomeController : Controller
    {
        qlbanhangEntities db = new qlbanhangEntities();
        public ActionResult Index(int MaLoaiSP = 0)
        {
            if(MaLoaiSP == 0)
            {
                var loaiSanphams = db.LoaiSPs.ToList();
                var sanphams = db.SanPhams.Include(s => s.LoaiSP);
                ViewData["LoaiSPs"] = loaiSanphams;
                return View(sanphams.ToList());
            }
            else
            {
                var loaiSanphams = db.LoaiSPs.ToList();
                var sanphams = db.SanPhams.Where(x => x.MaLoaiSP == MaLoaiSP).Include(s => s.LoaiSP);
                ViewData["LoaiSPs"] = loaiSanphams;
                return View(sanphams.ToList());
            }
           
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}