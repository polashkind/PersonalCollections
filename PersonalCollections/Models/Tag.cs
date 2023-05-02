using System;
namespace PersonalCollections.Models
{
	public class Tag
	{
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Tag_Item> Tags_Items { get; set; }
    }
}

