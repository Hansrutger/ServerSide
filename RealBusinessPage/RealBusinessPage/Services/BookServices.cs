using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using RealBusinessPage.Interfaces;
using RealBusinessPage.Models;

namespace RealBusinessPage.Services
{
    public class BookServices : IBookServices
    {
        public void Delete(string isbn)
        {
            throw new NotImplementedException();
        }

        public List<BOOKSet> List()
        {
            ServerSideEntities2 db = new ServerSideEntities2();
            var dbBooks = (from b in db.BOOKSet orderby b.Title select b).ToList();
            List<BOOKSet> bList = new List<BOOKSet>();
            foreach (var obj in dbBooks)
            {
                bList.Add(obj);
            }
            return bList;
        }

        public BOOKSet Read(string isbn)
        {
            throw new NotImplementedException();
        }

        public void Update(string isbn, BOOKSet bookObj)
        {
            throw new NotImplementedException();
        }
    }
}