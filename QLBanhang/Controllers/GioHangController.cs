using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web.Mvc;
using QLBanhang.Models;

namespace QLBanhang.Controllers
{
    public class GioHangController : Controller
    {
        private qlbanhangEntities db = new qlbanhangEntities();
        // GET: GioHang
        public ActionResult Index()
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            return View(giohang);
        }

        public RedirectToRouteResult AddToCart(string MaSP)
        {
            if(Session["giohang"] == null)
            {
                Session["giohang"] = new List<CartItem>();
            }
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;

            if (giohang.FirstOrDefault(m => m.MaSp == MaSP) == null)
            {
                SanPham sp = db.SanPhams.Find(MaSP);
                CartItem newCartItem = new CartItem();
                newCartItem.MaSp = sp.MaSP;
                newCartItem.TenSP = sp.TenSP;
                newCartItem.SoLuong = 1;
                newCartItem.DonGia = Convert.ToDouble(sp.Dongia);
                giohang.Add(newCartItem);

            }
            else
            {
                CartItem cartItem = giohang.FirstOrDefault(m => m.MaSp == MaSP);
                cartItem.SoLuong++;
            }
            Session["giohang"] = giohang;
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult Update(string MaSP, int txtSoluong)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem item = giohang.FirstOrDefault(m => m.MaSp == MaSP);
            if(item != null)
            {
                item.SoLuong = txtSoluong;
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }

        public RedirectToRouteResult DelCartItem(string MaSP)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            CartItem item = giohang.FirstOrDefault(m => m.MaSp == MaSP);
            if(item != null)
            {
                giohang.Remove(item);
                Session["giohang"] = giohang;
            }
            return RedirectToAction("Index");
        }

        public ActionResult Order(string email, string phone)
        {
            List<CartItem> giohang = Session["giohang"] as List<CartItem>;
            string sMsg = "<html><p>đặt hàng thành công</p></html>";
            MailMessage mail = new MailMessage("phamanhtuan9a531@gmail.com", email, "Thông tin đơn hàng", sMsg);
            SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
            client.UseDefaultCredentials = false;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("phamanhtuan9a531@gmail.com", "dykfmuughbhmoixx");
            mail.IsBodyHtml = true;
            client.Send(mail);
            return RedirectToAction("Index","Home");
        }
    }
}