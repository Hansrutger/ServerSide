using RealBusinessPage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RealBusinessPage.App_Start
{
    public static class dbInitiator
    {
        
        public static void StartUpStuff()
        {
            try
            {
                using (var db = new ServerSideEntities2())
                {
                    var dbStatus = (from a in db.STATUSSet select a).ToList();
                    if(dbStatus.Count==0)
                    {
                        STATUSSet statusInStock = new STATUSSet();
                        statusInStock.StatusId = 1;
                        statusInStock.status = "In Stock";

                        STATUSSet statusOutOffStock = new STATUSSet();
                        statusOutOffStock.StatusId = 2;
                        statusOutOffStock.status = "Out Off Stock";

                        db.STATUSSet.Add(statusOutOffStock);
                        db.STATUSSet.Add(statusInStock);

                        db.SaveChanges();

                    }
                }
            }

            catch
            {
                
            }
        }

    }
}