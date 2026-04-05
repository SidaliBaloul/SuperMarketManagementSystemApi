using System.ComponentModel.DataAnnotations;

namespace SuperMarketManagementSystemApi.DTOs
{
    public class RefreshDto
    {
        [Required(ErrorMessage = "Refresh Token Is Required")]
        public string RefreshToken { get; set; }

        [Required(ErrorMessage = "UserName Is Required")]
        public string UserName { get; set; }
    }
}
