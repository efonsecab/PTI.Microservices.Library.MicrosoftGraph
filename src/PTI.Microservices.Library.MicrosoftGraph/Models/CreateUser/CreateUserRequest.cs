using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PTI.Microservices.Library.MicrosoftGraph.Models.CreateUser
{
    public class CreateUserRequest
    {
        public bool accountEnabled { get; set; }
        public string mailNickname { get; set; }
        public string userPrincipalName { get; set; }

        public string displayName { get; set; }
        public string givenName { get; set; }
        public string surName { get; set; }
        public Identity[] identities { get; set; }
        public Passwordprofile passwordProfile { get; set; }
        public string passwordPolicies { get; set; }
    }

    public class Passwordprofile
    {
        public string password { get; set; }
        public bool forceChangePasswordNextSignIn { get; set; }
    }

    public class Identity
    {
        public string signInType { get; set; }
        public string issuer { get; set; }
        public string issuerAssignedId { get; set; }
    }


}
