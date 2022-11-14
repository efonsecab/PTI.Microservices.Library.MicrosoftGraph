using System;
using System.Collections.Generic;
using System.Text;

namespace PTI.Microservices.Library.Models.MicrosoftGraphService.GetAppToken
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class GetAppTokenResponse
    {
        public string token_type { get; set; }
        public int expires_in { get; set; }
        public int ext_expires_in { get; set; }
        public string access_token { get; set; }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
