using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public abstract class BaseEntity
    {
        public DateTime Created { get; set; }
        public string? LastModifieBy { get; set; }
        public DateTime? LastModified { get; set; }
    }
}
