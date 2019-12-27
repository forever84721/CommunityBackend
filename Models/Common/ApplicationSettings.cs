using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Common
{
    public interface ISetting
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<暫止>")]
        public string JWT_Secret { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "<暫止>")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "<暫止>")]
        public string Client_URL { get; set; }
        public string IdentityConnection { get; set; }
    }
    public class ApplicationSettings: ISetting
    {
        public string JWT_Secret { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1056:Uri properties should not be strings", Justification = "<暫止>")]
        public string Client_URL { get; set; }
        public string IdentityConnection { get; set; }
    }
}
