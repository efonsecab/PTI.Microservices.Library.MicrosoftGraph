using PTI.Microservices.Library.Models.MicrosoftGraphService.GetApplication;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTI.Microservices.Library.Models.MicrosoftGraphService.UpdateApplicationRoles
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateApplicationRolesRequest
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Approle> appRoles { get; set; }
    }
}
