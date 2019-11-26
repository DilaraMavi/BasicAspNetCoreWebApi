using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAspNetCoreWebApi.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Please enter your name.")]
        [StringLength(100, ErrorMessage = "Please do not exceed 100 characters.", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage="Please enter your e-mail."),DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter your department."), DataType(DataType.EmailAddress)]
        [StringLength(100, ErrorMessage = "Please do not exceed 100 characters.", MinimumLength = 3)]
        public string Department { get; set; }

        //[Required(ErrorMessage ="Please enter your user name.")]
        //[StringLength(50, ErrorMessage = "Please do not exceed 50 characters.", MinimumLength = 3)]
        //public string UserName { get; set; }

        //[Required(ErrorMessage = "Please enter your password."), DataType(DataType.Password)]
        //[StringLength(8, ErrorMessage = "Lease enter 8 characters.", MinimumLength = 8)]
        //public string Password { get; set; }

        //public string Token { get; set; }
    }
}
