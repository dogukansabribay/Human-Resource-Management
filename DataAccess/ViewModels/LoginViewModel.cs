using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DataAccess.ViewModels
{
    public class LoginViewModel
    {
        [Key]
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
    }
}
