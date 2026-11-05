using System.ComponentModel.DataAnnotations;

namespace FixTrack.Web.ViewModels;

public sealed class RegisterViewModel
{
    [Required(ErrorMessage = "Full name is required.")]
    [StringLength(120, MinimumLength = 2,
        ErrorMessage = "Full name must be between 2 and 120 characters.")]
    [Display(Name = "Full name")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email address is required.")]
    [EmailAddress(ErrorMessage = "Enter a valid email address.")]
    [StringLength(256)]
    [Display(Name = "Email address")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Mobile number is required.")]
    [RegularExpression(@"^(09\d{9}|\+639\d{9})$",
        ErrorMessage = "Enter a Philippine mobile number such as 09171234567 or +639171234567.")]
    [Display(Name = "Mobile number")]
    [DataType(DataType.PhoneNumber)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, MinimumLength = 8,
        ErrorMessage = "Password must be at least 8 characters long.")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please confirm your password.")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirm password")]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match.")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
