//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RealBusinessPage.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class COPYSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public COPYSet()
        {
            this.BORROWSet = new HashSet<BORROWSet>();
        }
    
        public int Barcode { get; set; }
        public string Location { get; set; }
        public string Library { get; set; }
        public Nullable<int> STATUSStatusId { get; set; }
        public Nullable<int> BOOKISBN { get; set; }
    
        public virtual BOOKSet BOOKSet { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BORROWSet> BORROWSet { get; set; }
        public virtual STATUSSet STATUSSet { get; set; }
    }
}
