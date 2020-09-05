using System;
using System.Collections.Generic;
using System.Text;

namespace Data.AuthenticationTypes
{
    public class AppToken 
    {

        public Guid UserId { get; set; }
        public Guid SessionToken { get; set; }
        public DateTime NotValidBefore { get; set; }
        public int ValidityDurationMilliseconds { get; set; }
        public string[] Roles { get; set; }
        
        
    }
}
