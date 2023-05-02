using System;

namespace PersonalCollections.Models
{
	public class Collection
	{
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Subject { get; set; }

        public List<Item> Items { get; set; }
    } 
}

