﻿using System.Collections.Generic;
using System.Web.Http;
using Thinktecture.Samples.Models;

namespace Thinktecture.Samples
{
    /// <summary>
    /// IdentityController
    /// </summary>
    [Authorize]
    public class IdentityController : ApiController
    {
        /// <summary>
        /// Returns the claims of the current principal
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ViewClaim> Get()
        {
            var principal = Request.GetClaimsPrincipal();
            return ViewClaims.GetAll(principal);
        }
    }
}
