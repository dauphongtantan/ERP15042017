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
    
    public partial class DM_DOI_TUONG
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DM_DOI_TUONG()
        {
            this.MH_CT_MDV = new HashSet<MH_CT_MDV>();
            this.NH_CT_NTTK = new HashSet<NH_CT_NTTK>();
            this.NH_NTTK = new HashSet<NH_NTTK>();
            this.NH_UNC = new HashSet<NH_UNC>();
            this.NH_CT_UNC = new HashSet<NH_CT_UNC>();
            this.QUY_CT_PHIEU_CHI = new HashSet<QUY_CT_PHIEU_CHI>();
            this.QUY_PHIEU_CHI = new HashSet<QUY_PHIEU_CHI>();
            this.QUY_PHIEU_THU = new HashSet<QUY_PHIEU_THU>();
        }
    
        public string MA_DOI_TUONG { get; set; }
        public string TEN_DOI_TUONG { get; set; }
        public string DIA_CHI { get; set; }
        public string MA_LOAI_DOI_TUONG { get; set; }
        public string MA_CONG_TY { get; set; }
    
        public virtual CCTC_CONG_TY CCTC_CONG_TY { get; set; }
        public virtual DM_LOAI_DOI_TUONG DM_LOAI_DOI_TUONG { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<MH_CT_MDV> MH_CT_MDV { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NH_CT_NTTK> NH_CT_NTTK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NH_NTTK> NH_NTTK { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NH_UNC> NH_UNC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<NH_CT_UNC> NH_CT_UNC { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUY_CT_PHIEU_CHI> QUY_CT_PHIEU_CHI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUY_PHIEU_CHI> QUY_PHIEU_CHI { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<QUY_PHIEU_THU> QUY_PHIEU_THU { get; set; }
    }
}
