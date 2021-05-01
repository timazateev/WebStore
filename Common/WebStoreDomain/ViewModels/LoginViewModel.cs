using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.ViewModels
{
    public class LoginViewModel
    {
        [Required, MaxLength(256)]
        [Display(Name = "User Name")]
        public string UserName { get; init; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; init; }

        [Display(Name = "Remember Me")]
        public bool RememberMe { get; init; }

        [HiddenInput]
        public string ReturnUrl { get; init; }
    }
}
