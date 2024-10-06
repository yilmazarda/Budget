using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Models
{
    public class CategoryTransactionModel
    {
        public List<Category> Categories { get; set; }
        public Transaction Transaction  { get; set; }
    }
}