using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiNCoreEApplication1.Api.Helpers
{
    public class TokenHelper
    {
        public string AccessToken { get; set; }
        public DateTime EmitionDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string ExpiresIn { get; set; }
    }
}
