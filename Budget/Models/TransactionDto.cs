namespace Budget.Models
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Name { get; set;}
        public DateTime Date { get; set; }
        public decimal Value { get; set; }
        public int CategoryId { get; set; }
    }

}