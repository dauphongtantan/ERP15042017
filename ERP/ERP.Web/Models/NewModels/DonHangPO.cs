﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ERP.Web.Models.NewModels
{
    public class ThongTinDonPO
    {

        public string MA_SO_PO { get; set; }
        public String NGAY_LEN_PO { get; set; }
        public string MA_KHACH_HANG { get; set; }
        public string TEN_LIEN_HE { get; set; }
        public string HINH_THUC_THANH_TOAN { get; set; }
        public float THUE_SUAT_GTGT { get; set; }
        public decimal? TONG_TIEN_HANG { get; set; }
        public string SO_TIEN_VIET_BANG_CHU { get; set; }
        public string NHAN_VIEN_QUAN_LY { get; set; }
        public Nullable<bool> DA_BAN_HANG { get; set; }
        public string MA_SO_XUAT_KHO { get; set; }
        public string NGAY_GIAO_HANG { set; get; }
        public string DIA_DIEM_GIAO_HANG { set; get; }
        public float TONG_TIEN_THANH_TOAN { set; get; }
        public float TONG_TIEN_THUE_GTGT { set; get; }
        public List<ChiTietDonHangPO> ChiTietPO { set; get; }
        public string TRUC_THUOC { set; get; }
    }
}