using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonalCollections.Models;
using PersonalCollections.Data.Enums;

namespace PersonalCollections.Data.Interfaces
{
	public interface ICommentsService
	{
		Task<Comment?> GetById(int id, CancellationToken cancellationToken);
    }
}

 