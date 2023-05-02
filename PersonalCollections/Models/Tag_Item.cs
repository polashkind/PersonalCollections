using System;
namespace PersonalCollections.Models
{
	public class Tag_Item
	{
		public int ItemId { get; set; }
		public Item Item { get; set; }

		public int TagId { get; set; }
		public Tag Tag { get; set; }
	}
}

