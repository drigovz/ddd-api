using System.ComponentModel.DataAnnotations;

namespace Api.Domain.DTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email not valid")]
        public string Email { get; set; }
    }
}