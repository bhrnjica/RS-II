using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebServiceSOAP
{
    /// <summary>
    /// Summary description for WebFaktorijel
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebFaktorijel : System.Web.Services.WebService
    {

        /// <summary>
        /// Servisna metoda (operacija) koja izraćunava faktorijel prirodnog broja
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [WebMethod]
        public long Faktorijel(int n)
        {
            if (n < 0)
                return 1;
            else if (n > 31)
                throw new Exception("Ulazni broj prevelik za izracunavanje. Koristiti servisnu metodu BigFaktorijel");

            return Enumerable.Range(1, n).Aggregate((acc, x) => acc * x);
        }

        /// <summary>
        /// Servisna metoda (operacija) koja izračunava faktorijel prirodnog broja
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        [WebMethod]
        public string BigFaktorijel(int n)
        {
            var fac = new System.Numerics.BigInteger();
            fac = 1;
            for (int i = 1; i < n; i++)
            {
                fac = fac * i;
            }

            return fac.ToString();
        }
    }
}
