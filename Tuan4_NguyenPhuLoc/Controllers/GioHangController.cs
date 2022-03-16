using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Tuan4_NguyenPhuLoc.Models;

namespace Tuan4_NguyenPhuLoc.Controllers
{
    public class GioHangController : Controller
    {
        // GET: GioHang
        MyDataDataContext data = new MyDataDataContext();
        public List<GioHang> LayGioHang()
        {
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang == null)
            {
                listGioHang = new List<GioHang>();
                Session["GioHang"] = listGioHang;
            }
            return listGioHang;
        }
        public ActionResult ThemGioHang(int id, string strURL)
        {
            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.Find(n => n.masach == id);
            if (sanpham == null)
            {
                sanpham = new GioHang(id);
                listGioHang.Add(sanpham);
                return Redirect(strURL);
            }
            else
            {
                sanpham.isoluong++;
                return Redirect(strURL);
            }
        }
        private int TongSoLuong()
        {
            int tsl = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                tsl = listGioHang.Sum(n => n.isoluong);
            }
            return tsl;
        }
        private int TongSoLuongSanPham()
        {
            int tsl = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                tsl = listGioHang.Count;
            }
            return tsl;
        }
        private double TongTien()
        {
            double tt = 0;
            List<GioHang> listGioHang = Session["GioHang"] as List<GioHang>;
            if (listGioHang != null)
            {
                tt = listGioHang.Sum(n => n.dThanhtien);
            }
            return tt;
        }
        public ActionResult GioHang()
        {
            List<GioHang> listGioHang = LayGioHang();
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return View(listGioHang);
        }

        public ActionResult GioHangPartial()
        {
            ViewBag.Tongsoluong = TongSoLuong();
            ViewBag.Tongtien = TongTien();
            ViewBag.Tongsoluongsanpham = TongSoLuongSanPham();
            return PartialView();
        }

        public ActionResult XoaGioHang(int id)
        {
            List<GioHang> listGioHang = LayGioHang();
            GioHang sanpham = listGioHang.SingleOrDefault(n => n.masach == id);
            if (sanpham != null)
            {
                listGioHang.RemoveAll(n => n.masach == id);
                return RedirectToAction("GioHang");
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult CapnhatGioHang(int id, FormCollection collection)
        {
            List<GioHang> listGioHang = LayGioHang();
            GioHang giohang = listGioHang.SingleOrDefault(n => n.masach == id);
            if (giohang != null)
            {
                Sach sach = data.Saches.FirstOrDefault(m => m.masach == id);
                int sl = int.Parse(collection["txtSolg"].ToString());
                if(sl>sach.soluongton)
                {
                    Session["Message"] = "Không đủ số lượng";
                    Session["AlertSatus"] = "danger";
                    return RedirectToAction("GioHang");
                }
                giohang.isoluong = sl;
            }
            return RedirectToAction("GioHang");
        }
        public ActionResult XoaTatCaGioHang()
        {
            List<GioHang> listGioHang = LayGioHang();
            listGioHang.Clear();
            return RedirectToAction("GioHang");
        }

        public ActionResult DatHang()
        {
            List<GioHang> listGioHang = LayGioHang();
            foreach(var)
        }
    }
}