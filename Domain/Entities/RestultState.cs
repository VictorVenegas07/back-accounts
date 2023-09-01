using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RestultState:Account
    {
        public int IdRestultState { get; set; }
        public string LinkedName { get; set; }
        public string LinkedPhone { get; set; }
        public decimal Interest { get; set; }
        public int NumberInstallations { get; set; }

    }
}
