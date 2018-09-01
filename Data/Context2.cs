using Data.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Data
{
    public class DataContext2
    {

        /// <summary>
        /// شی مربوط به پایگاه داده
        /// </summary>
        public static Context2 Context2
        {
            get
            {
                try
                {
                    if (HttpContext.Current == null)
                        return new Context2();

                    var ocKey = "ocm_" + HttpContext.Current.GetHashCode().ToString("x");
                    if (!HttpContext.Current.Items.Contains(ocKey))
                        HttpContext.Current.Items.Add(ocKey, new Context2());
                    return HttpContext.Current.Items[ocKey] as Context2;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
