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
    
    public partial class BORROWSet
    {
        public int Borrowid { get; set; }
        public string BorrowDate { get; set; }
        public string ToBeReturnedDate { get; set; }
        public string ReturnDate { get; set; }
        public int COPYBarcode { get; set; }
        public int BORROWERPersonId { get; set; }
    
        public virtual BORROWERSet BORROWERSet { get; set; }
        public virtual COPYSet COPYSet { get; set; }
    }
}
