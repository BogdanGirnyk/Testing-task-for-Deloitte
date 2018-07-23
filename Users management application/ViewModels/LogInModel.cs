using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Users_management_application.ViewModels
{
    public class LogInModel
    {
        [Required]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [HiddenInput]
        public string ReturnUrl { get; set; }
    }
}