namespace ClassTask.Models
{
    public class MediaUser
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public string UserName { get; set; } = string.Empty;
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required DateOnly DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public DateTime DateAdded { get; init; } = DateTime.UtcNow;
    }
}
