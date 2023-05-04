using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PersonalCollections.Models
{
    public class Comment
	{
        public int Id { get; set; }

        [Display(Name = "Text")]
        public string Text { get; set; }

        [Display(Name = "Created")]
        public DateTime CreatedAt { get; set; }

        public List<Item> Items { get; set; }
    }
}

 