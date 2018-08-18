using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.PublicModel
{
    public class LoginModels
    {
        public class LoginEntry
        {
            public string Username { get; set; }
            public string Password { get; set; }
            public string NextAction { get; set; }
        }
    }
}
