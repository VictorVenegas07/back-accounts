using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MovementBalance:Movement
    {
        public int IdMovementBalance { get; set; }
        public string Type { get; set; }
        public string CurrentBalance { get; set; }
        public string NewBalance { get; set; }
        public int HistoryMovementsID { get; set; }
        public HistoryMovements HistoryMovements { get; set; }
    }
}
