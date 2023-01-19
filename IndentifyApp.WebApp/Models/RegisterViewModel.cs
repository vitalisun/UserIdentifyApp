using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace IndentifyApp.WebApp.Models;

public class RegisterViewModel
{
    [JsonPropertyName("login")]
    public string Login { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
    [JsonPropertyName("passwordConfirm")]
    public string PasswordConfirm { get; set; }
}