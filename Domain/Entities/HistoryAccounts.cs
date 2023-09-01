using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class HistoryAccounts
    {
        public int HistoryAccountsID { get; set; }
        public int IdNatural { get; set; }
        public Natural Natural { get; set; }
        public int IdRestultState { get; set; }
        public RestultState RestultState { get; set; }
        public IEnumerable<MovementAccount> MovementAccounts { get; set; }
    }
}
