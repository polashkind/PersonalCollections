using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalCollections.Models
{
	public class Item
	{
		public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedAt { get; set; }

        [Display(Name = "Likes")]
        public int Likes { get; set; }

        public int CollectionId { get; set; }
        [ForeignKey("CollectionId")]
        public Collection Collection { get; set; }

        public List<Tag_Item> Tags_Items { get; set; }

        public int CommentId { get; set; }
        [ForeignKey("CommentId")]
        public Comment Comments { get; set; }
    }
}

