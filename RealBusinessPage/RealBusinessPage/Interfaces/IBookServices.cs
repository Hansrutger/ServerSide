using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealBusinessPage.Interfaces
{
    public interface IBookServices
    {
        List<BOOK> List();
        BOOK Read(string isbn);
        void Update(string isbn, BOOK bookObj);
        void Delete(string isbn);
    }
}
