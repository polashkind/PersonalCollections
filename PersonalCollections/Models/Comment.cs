using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Hosting;

namespace PersonalCollections.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Content { get; set; }

        [Display(Name = "Create date")]
        public DateTime CreatedAt { get; set; }

        //relationship
        public string CreatedByUserId { get; set; }
        public virtual ApplicationUser? CreatedBy { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

    }
}

