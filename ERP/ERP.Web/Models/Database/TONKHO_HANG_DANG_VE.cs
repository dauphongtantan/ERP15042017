//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ERP.Web.Models.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class TONKHO_HANG_DANG_VE
    {
        public string MA_HANG { get; set; }
        public int SL_DANG_VE { get; set; }
        public System.DateTime NGAY_VE_DU_KIEN { get; set; }
    
        public virtual HH HH { get; set; }
    }
}
