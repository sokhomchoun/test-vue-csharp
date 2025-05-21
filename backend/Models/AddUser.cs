namespace backend.Models
{
    public class AddUser
    {
        public Guid Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
