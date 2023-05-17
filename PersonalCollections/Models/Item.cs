using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PersonalCollections.Data.Enums;

namespace PersonalCollections.Models
{
	public class Item
	{
		public int Id { get; set; }

        [Display(Name = "Subject")]
        [Required(ErrorMessage = "Subject is required")]
        public ItemType Subject { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }


        // relationship
        public List<Collection>? Collection { get; set; }


        [Display(Name = "Create date")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Update date")]
        public DateTime UpdatedAt { get; set; }

        public string CreatedByUserId { get; set; }
        public virtual ApplicationUser? CreatedBy { get; set; }

        public string UpdatedByUserId { get; set; }
        public virtual ApplicationUser? UpdatedBy { get; set; }


        // Book
        [Display(Name = "Author")]
        public string? Author { get; set; }

        [Display(Name = "Genre")]
        public string? BookGenre { get; set; }

        [Display(Name = "Year of publication")]
        public DateOnly? BookYear { get; set; }

        // Car
        [Display(Name = "Brand")]
        public string? Brand { get; set; }
        [Display(Name = "Year of car manufacture")]
        public DateOnly? CarYear { get; set; }

        // Movie
        [Display(Name = "Producer")]
        public string? Producer { get; set; }

        [Display(Name = "Genre")]
        public string? MovieGenre { get; set; }

        [Display(Name = "Release year")]
        public DateOnly? MovieYear { get; set; }
    }
}

