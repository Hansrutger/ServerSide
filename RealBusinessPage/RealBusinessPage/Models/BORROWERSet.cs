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
    
    public partial class BORROWERSet
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BORROWERSet()
        {
            this.BORROWSet = new HashSet<BORROWSet>();
        }
    
        public int PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public int TelNo { get; set; }
        public int CATEGORYCategoryId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public string Salt { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<BORROWSet> BORROWSet { get; set; }
        public virtual CATEGORYSet CATEGORYSet { get; set; }
    }
}
