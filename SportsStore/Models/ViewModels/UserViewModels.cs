using System.ComponentModel.DataAnnotations;

namespace SportsStore.Models.ViewModels;

public class LoginModel
{
    [Required]
    [UIHint("email")]
    public string UserName { get; set; }

    [Required]
    [UIHint("password")]
    public string Password { get; set; }

    public string ReturnUrl { get; set; } = "/";
}

public class RegisterModel
{
    [Required]
    [Display(Name = "Username")]
    public string UserName { get; set; }

    [Required]
    [Display(Name = "Email")]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [Display(Name = "Password")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Compare("Password", ErrorMessage = "Password must mathc!!!")]
    [Display(Name = "Confirm Password")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; }
}
