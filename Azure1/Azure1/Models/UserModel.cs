using System.ComponentModel.DataAnnotations;

namespace Azure1.Models;

public class UserModel : ICloneable
{
    [Key]
    public int UserId { get; set; }

    [Required(ErrorMessage = "Username required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "First Name required")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name required")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.PhoneNumber, ErrorMessage = "Invalid Phone Number")]
    [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
    public string Phone { get; set; } = string.Empty;

    [Required(ErrorMessage = "Enter a password")]
    public string Password { get; set; } = string.Empty;

    public object Clone()
    {
        return MemberwiseClone();
    }
}