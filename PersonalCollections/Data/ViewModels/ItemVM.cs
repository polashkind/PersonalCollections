using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using PersonalCollections.Data.Enums;
using PersonalCollections.Models;

namespace PersonalCollections.Data.ViewModels
{
    public class ItemVM
    {
        public int Id { get; set; }

        [Display(Name = "Item title")]
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }

        [Display(Name = "Item description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [Display(Name = "Subject")]
        public ItemType? Subject { get; set; }

        [Display(Name = "Author")]
        public string? Author { get; set; }

        [Display(Name = "Genre")]
        public string? BookGenre { get; set; }

        [Display(Name = "Year of publication")]
        public DateOnly? Year { get; set; }

        [Display(Name = "Brand")]
        public string? Brand { get; set; }

        [Display(Name = "Producer")]
        public string? Producer { get; set; }

        [Display(Name = "Genre")]
        public string? MovieGenre { get; set; }

    }
}

