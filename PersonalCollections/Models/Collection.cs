using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PersonalCollections.Models
{
    public class Collection
	{
        [Key]
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required")]
        [StringLength(20, MinimumLength = 2, ErrorMessage = "Title should be more than 2 and less than 20")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Subject is required")]
        public string Subject { get; set; }

        // Relationship
        public List<Item>? Items { get; set; }
    } 
}

