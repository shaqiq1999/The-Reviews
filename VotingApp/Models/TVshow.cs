using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VotingApp.Areas.Identity.Data
{
    public class TVshow
    {
        [Key]
        public int Id { get; set; }
        public string ShowName { get; set; }
        public string Reviewer { get; set; }

        [Description]
        public string Review { get; set; }
        public int Rating { get; set; }
        
        [DisplayName("Image Name")]
        public string ShowImg { get; set; }
        [NotMapped]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
