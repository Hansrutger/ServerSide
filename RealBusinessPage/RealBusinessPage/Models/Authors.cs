using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealBusinessPage.Models
{
    public class Authors
    {
        [Key]
        public int AuthorId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public int BirthYear { get; set; }
        
        public ICollection<Books> Books { get; set; }
    }
}