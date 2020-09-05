using DomainName.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainName.Domain.Responses
{
    public class UserAuthenticationResponse
    {

        public UserAuthenticationResponse()
        {
            Generated = DateTime.Now;
        }
        public UserAuthenticationResponse(string token)
        {
            Generated = DateTime.Now;
        }

        public bool Authenticated { get; set; }
        public string Token { get; set; }
        public User User{ get; set; }
        public DateTime Generated { get;  }
        public List<string> Roles { get; set; }



    }
}
