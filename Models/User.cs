using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AngularNetCoreSample.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string Token { get; set; }

    }
}
