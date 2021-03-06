﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Thinktecture.IdentityModel;
using Thinktecture.IdentityModel.Tokens;

namespace Thinktecture.AuthorizationServer.Models
{
    public abstract class SigningKey
    {
        public virtual int ID { get; set; }
        [Required]
        public virtual string Name { get; set; }
        public abstract SigningCredentials GetSigningCredentials();

        public virtual ICollection<Application> Applications { get; set; }
    }
    
    public class X509CertificateReference : SigningKey
    {
        public virtual StoreLocation Location { get; set; }
        public virtual StoreName StoreName { get; set; }
        public virtual X509FindType FindType { get; set; }
        [Required]
        public virtual string FindValue { get; set; }

        public X509Certificate2 Certificate
        {
            get
            {
                return new X509CertificatesFinder(
                    this.Location, 
                    this.StoreName, 
                    this.FindType)
                .Find(this.FindValue, false).SingleOrDefault();
            }
        }

        public override SigningCredentials GetSigningCredentials()
        {
            var cert = this.Certificate;
            if (cert == null) return null;
            return new X509SigningCredentials(cert);
        }
    }

    public class SymmetricKey : SigningKey
    {
        [Required]
        public virtual byte[] Value { get; set; }

        public override SigningCredentials GetSigningCredentials()
        {
            if (this.Value == null) return null;
            return new HmacSigningCredentials(this.Value);
        }
    }
}
