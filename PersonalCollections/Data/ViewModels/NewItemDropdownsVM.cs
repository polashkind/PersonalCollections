using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalCollections.Models;

namespace PersonalCollections.Data.ViewModels
{
	public class NewItemDropdownsVM
	{
        public NewItemDropdownsVM()
        {
            Collections = new List<Collection>();
        }

        public List<Collection> Collections { get; set; }
    }
}
