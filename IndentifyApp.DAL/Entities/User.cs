using System.ComponentModel.DataAnnotations.Schema;

namespace IndentifyApp.DAL.Entities;

[Table("Users")]
public class User
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
}