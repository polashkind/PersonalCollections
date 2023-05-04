using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PersonalCollections.Models
{
    public class Collection
	{
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Display(Name = "Subject")]
        public string Subject { get; set; }

        public List<Item> Items { get; set; }
    } 
}

