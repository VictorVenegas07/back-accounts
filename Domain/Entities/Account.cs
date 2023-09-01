using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Account
    {

       
        public string AccountName { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public string Class { get; set; }
        public string State { get; set; }
        public string Type { get; set; }
        public DateTime? FinishDate { get; set; }
        public DateTime InitialDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string Id { get; set; }
        public User User { get; set; }
        public HistoryMovements HistoryMovements { get; set; }
        public HistoryAccounts HistoryAccounts { get; set; }
        [NotMapped]
        public Total Total { get; set; }
        public void Movement()
        {

        }
        public void ChangeState(string state)
        {

        }
    }
}
