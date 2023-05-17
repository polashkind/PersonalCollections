﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalCollections.Models;
using PersonalCollections.Data.ViewModels;

namespace PersonalCollections.Data.Interfaces
{
	public interface IItemsService
	{
		Task<IEnumerable<Item>> GetAll(CancellationToken cancellationToken);
		Task<Item> GetById(int id, CancellationToken cancellationToken);
        Task<Item?> Create(Item item, CancellationToken cancellationToken);
        Task<Item?> Update(Item item, CancellationToken cancellationToken);
        Task Delete(Item item, CancellationToken cancellationToken);
    }
}

 