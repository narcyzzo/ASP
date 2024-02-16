using System.ComponentModel.DataAnnotations;

namespace Bookstore.Dto
{
    public class Register
    {
        public string UserName { get; set; } = String.Empty;
        public string EmailAddress { get; set; } = String.Empty;

        [DataType(DataType.Password)]
        public string Password { get; set; } = String.Empty;

        [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; } = String.Empty;
    }
}
