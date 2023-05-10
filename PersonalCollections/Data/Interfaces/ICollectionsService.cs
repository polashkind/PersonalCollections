using System;
using PersonalCollections.Models;

namespace PersonalCollections.Data.Interfaces
{
    public interface ICollectionsService
    {
        Task<IEnumerable<Collection>> GetAll(CancellationToken cancellationToken);
        Task<Collection> GetById(int id, CancellationToken cancellationToken);
        Task<Collection?> Create(Collection collection, CancellationToken cancellationToken);
        //Task Delete(Collection collection, CancellationToken cancellationToken);
        //Task<Collection?> Update(Collection collection, CancellationToken cancellationToken);
    }
}

