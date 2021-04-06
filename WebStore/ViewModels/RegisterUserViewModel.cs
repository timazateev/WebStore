using System.ComponentModel.DataAnnotations;

namespace WebStore.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required, MaxLength(256)]
        [Display(Name = "User Name")]
        public string UserName { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        [Required]
        [Display(Name = "Password Confirmation")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string PasswordConfirm { get; init; }

    }
}
