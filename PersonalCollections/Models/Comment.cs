using System;
namespace PersonalCollections.Models
{
	public class Comment
	{
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedAt { get; set; }

        public List<Item> Items { get; set; }
    }
}

 