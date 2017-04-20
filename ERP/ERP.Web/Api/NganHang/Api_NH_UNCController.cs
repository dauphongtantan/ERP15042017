﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ERP.Web.Models.Database;
using System.Text.RegularExpressions;
using ERP.Web.Models.NewModels.NganHang;
using ERP.Web.Common;
using ERP.Web.Models.NewModels.All;

namespace ERP.Web.Api.NganHang
{
    public class Api_NH_UNCController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_NH_UNC
        public IQueryable<NH_UNC> GetNH_UNC()
        {
            return db.NH_UNC;
        }

        // GET: api/Api_NH_UNC/5
        [ResponseType(typeof(NH_UNC))]
        public IHttpActionResult GetNH_UNC(string id)
        {
            NH_UNC nH_UNC = db.NH_UNC.Find(id);
            if (nH_UNC == null)
            {
                return NotFound();
            }

            return Ok(nH_UNC);
        }

        // PUT: api/Api_NH_UNC/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNH_UNC(string id, NH_UNC nH_UNC)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nH_UNC.SO_CHUNG_TU)
            {
                return BadRequest();
            }

            db.Entry(nH_UNC).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NH_UNCExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        public string AutoMA_DU_KIEN()
        {
            Regex digitsOnly = new Regex(@"[^\d]");
            string year = DateTime.Now.Year.ToString().Substring(2, 2);
            string month = DateTime.Now.Month.ToString();
            string day = DateTime.Now.Day.ToString();
            if (month.Length == 1)
            {
                month = "0" + month;
            }
            if (day.Length == 1)
            {
                day = "0" + day;
            }
            string prefixNumber = "UNC" + year.ToString() + month.ToString() + day.ToString();
            string SoChungTu = (from nhapkho in db.NH_UNC where nhapkho.SO_CHUNG_TU.Contains(prefixNumber) select nhapkho.SO_CHUNG_TU).Max();


            if (SoChungTu == null)
            {
                return "UNC" + year + month + day + "0001";
            }
            SoChungTu = SoChungTu.Substring(9, SoChungTu.Length - 9);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 4 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "UNC" + year + month + day + result;
        }

        // POST: api/Api_NH_UNC
        [HttpPost]
        [Route("api/Api_NH_UNC/PostNH_UNC")]
        [ResponseType(typeof(NH_UNC))]

        public IHttpActionResult PostNH_UNC(ChiNganHang chi_nganhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //Lưu thông tin nhập kho
            NH_UNC unc = new NH_UNC();
            unc.NGAY_CHUNG_TU = GeneralFunction.ConvertToTime(chi_nganhang.NGAY_CHUNG_TU);
            unc.NGAY_HACH_TOAN = GeneralFunction.ConvertToTime(chi_nganhang.NGAY_HACH_TOAN);
            unc.SO_CHUNG_TU = AutoMA_DU_KIEN();
            unc.TAI_KHOAN_CHI = chi_nganhang.TAI_KHOAN_CHI;
            unc.MA_DOI_TUONG = chi_nganhang.MA_DOI_TUONG;
            unc.NOI_DUNG_THANH_TOAN = chi_nganhang.NOI_DUNG_THANH_TOAN;
            unc.DIEN_GIAI_NOI_DUNG_THANH_TOAN = chi_nganhang.DIEN_GIAI_NOI_DUNG_THANH_TOAN;
            unc.TAI_KHOAN_NHAN = chi_nganhang.TAI_KHOAN_NHAN;
            unc.NHAN_VIEN_CHUYEN_KHOAN = chi_nganhang.NHAN_VIEN_CHUYEN_KHOAN;
            unc.NGUOI_LAP_BIEU = chi_nganhang.NGUOI_LAP_BIEU;
            unc.TRUC_THUOC = "HOPLONG";
            db.NH_UNC.Add(unc);

            //Lưu thông tin tham chiếu
            if (chi_nganhang.ThamChieu.Count > 0)
            {
                foreach (ThamChieu item in chi_nganhang.ThamChieu)
                {
                    XL_THAM_CHIEU_CHUNG_TU newItem = new XL_THAM_CHIEU_CHUNG_TU();
                    newItem.SO_CHUNG_TU_GOC = unc.SO_CHUNG_TU;
                    newItem.SO_CHUNG_TU_THAM_CHIEU = item.SO_CHUNG_TU;
                    db.XL_THAM_CHIEU_CHUNG_TU.Add(newItem);
                }
            }
            //Lưu chi tiết
            decimal tongtien = 0;
            //TONKHO_HOPLONG HHTon = new TONKHO_HOPLONG();
            //HH_NHOM_VTHH NhomHang = new HH_NHOM_VTHH();
            if (chi_nganhang.ChiTietHachToan != null && chi_nganhang.ChiTietHachToan.Count > 0)
            {
                foreach (ChiTietHachToanPhieuChi item in chi_nganhang.ChiTietHachToan)
                {
                    NH_CT_UNC newItem = new NH_CT_UNC();
                    newItem.SO_CHUNG_TU = unc.SO_CHUNG_TU;
                    newItem.DIEN_GIAI = item.DIEN_GIAI;
                    newItem.LOAI_TIEN = item.LOAI_TIEN;
                    newItem.TK_CO = item.TK_CO;
                    newItem.TK_NO = item.TK_NO;
                    newItem.SO_TIEN = Convert.ToDecimal(item.SO_TIEN);
                    newItem.TY_GIA = Convert.ToInt32(item.TY_GIA);
                    newItem.QUY_DOI = newItem.SO_TIEN * newItem.TY_GIA;
                    tongtien += newItem.QUY_DOI;
                    newItem.MA_DOI_TUONG = unc.MA_DOI_TUONG;
                    newItem.DON_VI = item.DON_VI;
                    if(chi_nganhang.ChiTietThue != null && chi_nganhang.ChiTietThue.Count > 0)
                    {
                        var thue = chi_nganhang.ChiTietThue.Where(x => x.MA_NHA_CUNG_CAP == unc.MA_DOI_TUONG).FirstOrDefault();
                        newItem.DIEN_GIAI_THUE = thue.DIEN_GIAI_THUE;
                        newItem.TK_THUE_GTGT = thue.TK_THUE_GTGT;
                        newItem.TIEN_THUE_GTGT = thue.TIEN_THUE_GTGT;
                        newItem.CK_THUE_GTGT = thue.CK_THUE_GTGT;
                        newItem.GIA_TRI_HHDV_CHUA_THUE = thue.GIA_TRI_HHDV_CHUA_THUE;
                        newItem.NGAY_HD = Convert.ToDateTime(thue.NGAY_HD);
                        newItem.SO_HD = thue.SO_HD;
                        newItem.MAU_SO_HD = thue.MAU_SO_HD;
                        newItem.KY_HIEU_HD = thue.KY_HIEU_HD;
                        newItem.MA_NHA_CUNG_CAP = thue.MA_NHA_CUNG_CAP;
                    }
                    





                    db.NH_CT_UNC.Add(newItem);
                }
            }

            unc.TONG_TIEN = tongtien;



            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NH_UNCExists(chi_nganhang.SO_CHUNG_TU))
                {
                    return Conflict();
                }
                else

                    throw;

            }


            return Ok(unc.SO_CHUNG_TU);

        }

        // DELETE: api/Api_NH_UNC/5
        [ResponseType(typeof(NH_UNC))]
        public IHttpActionResult DeleteNH_UNC(string id)
        {
            NH_UNC nH_UNC = db.NH_UNC.Find(id);
            if (nH_UNC == null)
            {
                return NotFound();
            }

            db.NH_UNC.Remove(nH_UNC);
            db.SaveChanges();

            return Ok(nH_UNC);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NH_UNCExists(string id)
        {
            return db.NH_UNC.Count(e => e.SO_CHUNG_TU == id) > 0;
        }
    }
}