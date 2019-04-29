using System.ComponentModel.DataAnnotations;

namespace Syski.API.Models
{
    public class UserAuthDTO
    {

        [Required]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; }

    }
}
