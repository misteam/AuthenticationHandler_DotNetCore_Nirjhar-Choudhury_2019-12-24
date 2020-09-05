using System;
using System.Collections.Generic;
using System.Text;

namespace DomainName.Domain.Entities
{
    public class ApplicationToken
    {

        public ApplicationToken(string token, int durationInMilliseconds)
        {
            Token = token;
            NotValidBefore = DateTime.Now;
            DurationInMilliseconds = durationInMilliseconds;
            
        }

        public int DurationInMilliseconds { get;  }
        public DateTime GeneratedAt { get;  }
        public DateTime NotValidBefore { get;  }
        public string Token { get;  }
        public string TokenType { get; set; }
        public AuthenticationUser User { get;  }

    }
}
