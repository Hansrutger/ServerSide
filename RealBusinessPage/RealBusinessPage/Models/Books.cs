using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace RealBusinessPage.Models
{
    public class Books
    {
        [Key]
        public int BookId { get; set; }
        public String ISBN { get; set; }
        public String Title { get; set; }
        public int PubYear { get; set; }
        public String Description { get; set; }
        public int Pages { get; set; }

        [ForeignKey("Authors")]
        public int AuthorId { get; set; }

        public Authors Authors { get; set; }
    }
}