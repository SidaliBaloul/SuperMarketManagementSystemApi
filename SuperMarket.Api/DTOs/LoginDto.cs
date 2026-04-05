using System.ComponentModel.DataAnnotations;

namespace SuperMarketManagementSystemApi.DTOs
{
    public class LoginDto
    {
        [Required(ErrorMessage ="UserName Is Required! ")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "PassWord Is Required! ")]
        public string Password { get; set; }
    }
}
