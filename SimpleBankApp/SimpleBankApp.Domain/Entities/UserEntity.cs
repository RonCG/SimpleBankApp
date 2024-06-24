namespace SimpleBankApp.Domain.Entities
{
    public class UserEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedOn { get; set; } 
        public DateTime LastUpdatedOn { get; set; }
    }
}
