using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Models
{
    public class AdminUser
    {
        [Required]
        [Key]
        public int Id { get; set; }
        public string GroupName { get; set; }

        public string Location { get; set; }

        public string Password { get; set; }

    }
}
