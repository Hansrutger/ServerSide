using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealBusinessPage.Interfaces;

namespace RealBusinessPage.Services
{
    public class BookServices : IBookServices
    {
        public void Delete(string isbn)
        {
            throw new NotImplementedException();
        }

        public List<BOOK> List()
        {
            DBLibraryEntities db = new DBLibraryEntities();
            var dbBooks = (from b in db.BOOKs orderby b.Title select b).ToList();
            List<BOOK> bList = new List<BOOK>();
            foreach (var obj in dbBooks)
            {
                bList.Add(obj);
            }
            return bList;
        }

        public BOOK Read(string isbn)
        {
            throw new NotImplementedException();
        }

        public void Update(string isbn, BOOK bookObj)
        {
            throw new NotImplementedException();
        }
    }
}