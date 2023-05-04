using System;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace PersonalCollections.Models
{
    public class Tag
	{
        public int Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public List<Tag_Item> Tags_Items { get; set; }
    }
}

