using Microsoft.EntityFrameworkCore;
using PersonalCollections.Data.Interfaces;
using PersonalCollections.Models;

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
            var result = await _dbSet.Include(n => n.Items).ToListAsync(cancellationToken);
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
                .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
            return result;
        }
    }
}

