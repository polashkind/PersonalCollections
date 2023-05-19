using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalCollections.Data.Interfaces;
using PersonalCollections.Models;
using System.Threading;

namespace PersonalCollections.Data.Services
{
    public class CollectionsService : ICollectionsService
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<Collection> _dbSet;

        public CollectionsService(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Collection>();
        }

        public async Task<IEnumerable<Collection>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _dbSet
                .Include(n => n.Items)
                .ToListAsync(cancellationToken);
            return result;
        }

        public async Task<Collection?> Create(Collection collection, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(collection);
            await _context.SaveChangesAsync(cancellationToken);
            return collection;
        }

        public async Task<Collection?> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _dbSet
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .Include(c => c.Items)
                .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
            return result;
        }

        public async Task<Collection?> Update(Collection collection, CancellationToken cancellationToken)
        {
            _context.Entry(collection).State = EntityState.Modified;
            _dbSet.Update(collection);
            await _context.SaveChangesAsync(cancellationToken);
            return collection;
        }

        public async Task Delete(Collection collection, CancellationToken cancellationToken)
        {
            _dbSet.Remove(collection);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}

