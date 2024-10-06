using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budget.Models
{
        public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        // Optionally include only necessary fields for transactions
        public List<TransactionDto> Transactions { get; set; } = new List<TransactionDto>();
    }
}