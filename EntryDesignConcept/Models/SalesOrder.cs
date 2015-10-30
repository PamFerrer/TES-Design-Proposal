using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EntryDesignConcept.Models
{
    public class SalesOrder
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public User Manager { get; set; }
        public Team Team { get; set; }       
        public Organization Organization { get; set; }
        public bool Status { get; set; }
    }
}