using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalCollections.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        // relationships
        public List<Collection>? CreatedCollections { get; set; }
        public List<Collection>? UpdatedCollections { get; set; }
        public List<Item>? CreatedItems { get; set; }
        public List<Item>? UpdatedItems { get; set; }
    }
}
