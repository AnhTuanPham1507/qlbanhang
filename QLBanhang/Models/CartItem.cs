﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QLBanhang.Models
{
    public class CartItem
    {
        public string MaSp { get; set; }
        public string TenSP { get; set; }
        public double DonGia { get; set; }
        public int SoLuong { get; set; }

        public double ThanhTien
        {
            get { return SoLuong * DonGia; }
        }
    }
}