using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkApp
{
    class Order
    {
        [Key, Column(Order = 0)]
        public string Number { get; set; }
        [Key, Column(Order = 1)]
        public DateTime Date { get; set; }
        public string Stage { get; set; }

        [ForeignKey("Customer")]
        [Column(Order = 2)]
        public string CustomerLogin { get; set; }

        [ForeignKey("Manager")]
        [Column(Order = 3)]
        public string ManagerLogin { get; set; }

        public double Price { get; set; }

        public User Customer { get; set; }
        public User Manager { get; set; }
    }
}
