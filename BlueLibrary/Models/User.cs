using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BlueLibrary.Models
{
    public enum UserType
    {
        Client,
        Admin
    }

    public class User
    {
        public int Id { get; set; }

        [Required]
        [RegularExpression("^\\w{3,10}$", ErrorMessage = "Please enter a valid username: between 3 to 10 valid (word) charcaters!")]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [StringLength(15, MinimumLength =5, ErrorMessage = "Password must consist of 5 to 15 charcters!")]
        public string Password { get; set; }

        public UserType Type { get; set; } = UserType.Client;
    }
}
