using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PersonalCollections.Data.ViewModels
{
    public class NewItemVM
	{
        public int Id { get; set; }

        [Display(Name = "Item title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Item description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Select a collection")]
        [Required(ErrorMessage = "Item collection is required")]
        public int CollectionId { get; set; }
    }
}

