namespace SimpleBankApp.Domain.Entities
{
    public class BankAccountEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreatedOn { get; set; } 
        public DateTime LastUpdatedOn { get; set; }
    }
}
