namespace CinemaSystem.Models
{
    public class LoginAccount
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string PasswordHash { get; set; }
        public string Name { get; set;  } = string.Empty;
        public string Surname { get; set;  } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? LastLogin { get; set; }
    }
}
