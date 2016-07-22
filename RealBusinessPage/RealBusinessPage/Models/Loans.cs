using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealBusinessPage.Models
{
    public class Loans
    {
        [Key]
        public int LoanId { get; set; }
        public String Date { get; set; } 

        [ForeignKey("Books")]
        public int BookId { get; set; }

        public Books Books { get; set; }

        [ForeignKey("Accounts")]
        public int AccId { get; set; }

        public Accounts Accounts { get; set; }
    }
}