using System;
using PersonalCollections.Models;
using System.ComponentModel.DataAnnotations;

namespace PersonalCollections.Data.ViewModels
{
	public class NewCommentVM
	{
        public int ItemId { get; set; }

        public string? Comment { get; set; } = null!;
    }
}
