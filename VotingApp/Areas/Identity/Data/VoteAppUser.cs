using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace VotingApp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the VoteAppUser class
    public class VoteAppUser : IdentityUser
    {
        [PersonalData]
        public string Name { get; set; }
        [PersonalData]
        public string Address { get; set; }

    }
}
