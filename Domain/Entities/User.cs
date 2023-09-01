using Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User: IdentityUser
    {
        //public int UserID { get; set; }
        public string Name { get; set; }
        public IEnumerable<Natural> Naturals { get; set; }
        public IEnumerable<RestultState> RestultStates { get; set; }
    }
}
