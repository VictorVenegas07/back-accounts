using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Movement
    {
        public DateTime MovementDate { get; set; }
        public string State { get; set; }
        public int HistoryMovementsID { get; set; }
        

    }
}
