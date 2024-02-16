using System.ComponentModel.DataAnnotations;

namespace Bookstore.Dto
{
    public class Login
    {
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
