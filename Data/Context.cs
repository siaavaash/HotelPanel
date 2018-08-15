using Data.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Data
{
    public class DataContext
    {

        /// <summary>
        /// شی مربوط به پایگاه داده
        /// </summary>
        public static Context Context
        {
            get
            {
                try
                {
                    if (HttpContext.Current == null)
                        return new Context();

                    var ocKey = "ocm_" + HttpContext.Current.GetHashCode().ToString("x");
                    if (!HttpContext.Current.Items.Contains(ocKey))
                        HttpContext.Current.Items.Add(ocKey, new Context());
                    return HttpContext.Current.Items[ocKey] as Context;
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}
