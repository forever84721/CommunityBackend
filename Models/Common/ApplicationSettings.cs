using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Common
{
    public class ApplicationSettings
    {
        public string JWT_Secret { get; set; }
        public string Client_URL { get; set; }
        public string IdentityConnection { get; set; }
    }
}
