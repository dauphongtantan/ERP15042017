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
using ERP.Web.Models.NewModels.NganHang;
using ERP.Web.Common;
using System.Text.RegularExpressions;
using ERP.Web.Models.NewModels.All;
using ERP.Web.Models.NewModels.XuatKho;

namespace ERP.Web.Api.NganHang
{
    public class Api_NganHangController : ApiController
    {
        private ERP_DATABASEEntities db = new ERP_DATABASEEntities();

        // GET: api/Api_NganHang
        public IQueryable<NH_NTTK> GetNH_NTTK()
        {
            return db.NH_NTTK;
        }

        // GET: api/Api_NganHang/5
        [ResponseType(typeof(NH_NTTK))]
        public IHttpActionResult GetNH_NTTK(string id)
        {
            NH_NTTK nH_NTTK = db.NH_NTTK.Find(id);
            if (nH_NTTK == null)
            {
                return NotFound();
            }

            return Ok(nH_NTTK);
        }

        // PUT: api/Api_NganHang/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNH_NTTK(string id, NH_NTTK nH_NTTK)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != nH_NTTK.SO_CHUNG_TU)
            {
                return BadRequest();
            }

            db.Entry(nH_NTTK).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NH_NTTKExists(id))
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
            string prefixNumber = "NTTK" + year.ToString() + month.ToString() + day.ToString();
            string SoChungTu = (from nhapkho in db.NH_NTTK where nhapkho.SO_CHUNG_TU.Contains(prefixNumber) select nhapkho.SO_CHUNG_TU).Max();


            if (SoChungTu == null)
            {
                return "NTTK" + year + month + day + "0001";
            }
            SoChungTu = SoChungTu.Substring(10, SoChungTu.Length - 10);
            string number = (Convert.ToInt32(digitsOnly.Replace(SoChungTu, "")) + 1).ToString();
            string result = number.ToString();
            int count = 4 - number.ToString().Length;
            for (int i = 0; i < count; i++)
            {
                result = "0" + result;
            }
            return "NTTK" + year + month + day + result;
        }

        // POST: api/Api_NganHang
        [HttpPost]
        [Route("api/Api_NganHang/PostKNH_NTTK")]
        [ResponseType(typeof(NH_NTTK))]

        public IHttpActionResult PostKNH_NTTK(ThuNganHang chi_nganhang)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }


            //Lưu thông tin nhập kho
            NH_NTTK nhnttk = new NH_NTTK();
            nhnttk.NGAY_CHUNG_TU = GeneralFunction.ConvertToTime(chi_nganhang.NGAY_CHUNG_TU);
            nhnttk.NGAY_HACH_TOAN = GeneralFunction.ConvertToTime(chi_nganhang.NGAY_HACH_TOAN);
            nhnttk.SO_CHUNG_TU = AutoMA_DU_KIEN();
            nhnttk.MA_DOI_TUONG = chi_nganhang.MA_DOI_TUONG;
            nhnttk.NOP_VAO_TAI_KHOAN = chi_nganhang.NOP_VAO_TAI_KHOAN;
            nhnttk.LY_DO_THU = chi_nganhang.LY_DO_THU;
            nhnttk.DIEN_GIAI_LY_DO_THU = chi_nganhang.DIEN_GIAI_LY_DO_THU;
            nhnttk.NHAN_VIEN_THU = chi_nganhang.NHAN_VIEN_THU;
            nhnttk.NGUOI_LAP_BIEU = chi_nganhang.NGUOI_LAP_BIEU;
            nhnttk.TRUC_THUOC = "HOPLONG";
            db.NH_NTTK.Add(nhnttk);

            //Lưu thông tin tham chiếu
            if (chi_nganhang.ThamChieu.Count > 0)
            {
                foreach (ThamChieu item in chi_nganhang.ThamChieu)
                {
                    XL_THAM_CHIEU_CHUNG_TU newItem = new XL_THAM_CHIEU_CHUNG_TU();
                    newItem.SO_CHUNG_TU_GOC = nhnttk.SO_CHUNG_TU;
                    newItem.SO_CHUNG_TU_THAM_CHIEU = item.SO_CHUNG_TU;
                    db.XL_THAM_CHIEU_CHUNG_TU.Add(newItem);
                }
            }
            //Lưu chi tiết
            decimal tongtien = 0;
            //TONKHO_HOPLONG HHTon = new TONKHO_HOPLONG();
            //HH_NHOM_VTHH NhomHang = new HH_NHOM_VTHH();
            if (chi_nganhang.ChiTietPTNH != null && chi_nganhang.ChiTietPTNH.Count > 0)
            {
                foreach (ChiTietPhieuThuNH item in chi_nganhang.ChiTietPTNH)
                {
                    NH_CT_NTTK newItem = new NH_CT_NTTK();
                    newItem.SO_CHUNG_TU = nhnttk.SO_CHUNG_TU;
                    newItem.DIEN_GIAI = item.DIEN_GIAI;
                    newItem.LOAI_TIEN = item.LOAI_TIEN;
                    newItem.TK_CO = item.TK_CO;
                    newItem.TK_NO = item.TK_NO;
                    newItem.SO_TIEN = Convert.ToDecimal(item.SO_TIEN);
                    newItem.TY_GIA = Convert.ToInt32(item.TY_GIA);
                    newItem.QUY_DOI = newItem.SO_TIEN * newItem.TY_GIA;
                    tongtien += newItem.QUY_DOI;
                    newItem.MA_DOI_TUONG = nhnttk.MA_DOI_TUONG;
                    newItem.DON_VI = item.DON_VI;
                    db.NH_CT_NTTK.Add(newItem);
                  

                }
            }

            nhnttk.TONG_TIEN = tongtien;



            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (NH_NTTKExists(chi_nganhang.SO_CHUNG_TU))
                {
                    return Conflict();
                }
                else

                    throw;

            }


            return Ok(nhnttk.SO_CHUNG_TU);
            
        }

        // DELETE: api/Api_NganHang/5
        [ResponseType(typeof(NH_NTTK))]
        public IHttpActionResult DeleteNH_NTTK(string id)
        {
            NH_NTTK nH_NTTK = db.NH_NTTK.Find(id);
            if (nH_NTTK == null)
            {
                return NotFound();
            }

            db.NH_NTTK.Remove(nH_NTTK);
            db.SaveChanges();

            return Ok(nH_NTTK);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NH_NTTKExists(string id)
        {
            return db.NH_NTTK.Count(e => e.SO_CHUNG_TU == id) > 0;
        }
    }
}