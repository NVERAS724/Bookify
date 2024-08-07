using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookify.Infrastructure.Authentication.Models
{
    public sealed class CredentialRepresentationModel
    {
        public string Value { get; set; }
        public bool Temporary { get; set; }
        public string Type { get; set; }
    }
}
