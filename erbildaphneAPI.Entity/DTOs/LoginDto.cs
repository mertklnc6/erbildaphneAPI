using System.ComponentModel.DataAnnotations;

namespace erbildaphneAPI.Entity.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage = "Email boş geçilemez")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Şifre boş geçilemez")]
        [Display(Name = "Şifre")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public bool RememberMe { get; set; }
        //public string ReturnUrl { get; set; }
    }
}
