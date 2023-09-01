using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MovementAccount:Movement
    {
        public int IdMovementAccount { get; set; }
        public string Attribute { get; set; }
        public int HistoryAccountsID { get; set; }
        public HistoryAccounts HistoryAccounts { get; set; }

    }
}
