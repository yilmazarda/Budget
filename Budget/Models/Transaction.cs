using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing.Patterns;

namespace Budget.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}