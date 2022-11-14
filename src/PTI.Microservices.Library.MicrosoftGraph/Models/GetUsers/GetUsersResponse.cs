using System;
using System.Collections.Generic;
using System.Text;

namespace PTI.Microservices.Library.Models.MicrosoftGraphService.GetUsers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class GetUsersResponse
    {
        public string odatacontext { get; set; }
        public Value[] value { get; set; }
    }

    public class Value
    {
        public object[] businessPhones { get; set; }
        public string displayName { get; set; }
        public string givenName { get; set; }
        public object jobTitle { get; set; }
        public object mail { get; set; }
        public object mobilePhone { get; set; }
        public object officeLocation { get; set; }
        public object preferredLanguage { get; set; }
        public string surname { get; set; }
        public string userPrincipalName { get; set; }
        public string id { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
