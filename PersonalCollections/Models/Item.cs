using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonalCollections.Models
{
	public class Item
	{
		public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
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

