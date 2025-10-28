using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public Guid UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    public User(Guid userId, string name, string email, string passwordHash)
    {
        UserId = Guid.NewGuid();
        Username = name;
        Email = email;
        PasswordHash = passwordHash;
    }

    public User() { }
}