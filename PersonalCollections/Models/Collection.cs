using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PersonalCollections.Data.Enums;

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
        public ItemType Subject { get; set; }

        // Relationship
        public List<Item>? Items { get; set; }

        [Display(Name = "Create date")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Update date")]
        public DateTime UpdatedAt { get; set; }

        public string CreatedByUserId { get; set; }
        public virtual ApplicationUser? CreatedBy { get; set; }

        public string UpdatedByUserId { get; set; }
        public virtual ApplicationUser? UpdatedBy { get; set; }
    } 
}

