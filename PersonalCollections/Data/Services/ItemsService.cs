using System;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalCollections.Data.Enums;
using PersonalCollections.Data.Interfaces;
using PersonalCollections.Models;

namespace PersonalCollections.Data.Services
{
    public class ItemsService : IItemsService
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<Item> _dbSet;

        public ItemsService(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Item>();
        }

        public async Task<IEnumerable<Item>> GetAll(CancellationToken cancellationToken)
        {
            var result = await _dbSet
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .ToListAsync(cancellationToken);
            return result;
        }

        public async Task<IEnumerable<Item>> GetNonCollectionByType(ItemType type, List<Item> collectionItems, CancellationToken cancellationToken)
        {
            var result = await _dbSet
                .Include(i => i.CreatedBy)
                .Include(i => i.UpdatedBy)
                .Where(item => item.Subject == type && !collectionItems.Contains(item))
                .ToListAsync(cancellationToken);
            return result;
        }

        public async Task<Item> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _dbSet
                .Include(i => i.CreatedBy)
                .Include(i => i.UpdatedBy)
                .Include(i => i.Comments)
                .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

            return result;
        }

        public async Task<List<Item>> GetNonCollectionItems(List<Item> collectionItems, CancellationToken cancellationToken)
        {
            var result = await _dbSet
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .Where(item => !collectionItems.Contains(item))
                .ToListAsync(cancellationToken);

            return result;
        }

        public async Task<Item?> Create(Item item, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync(cancellationToken);
            return item;
        }

        public async Task<Item?> Update(Item item, CancellationToken cancellationToken)
        {
            _context.Entry(item).State = EntityState.Modified;
            _dbSet.Update(item);
            await _context.SaveChangesAsync(cancellationToken);
            return item;
        }

        public async Task Delete(Item item, CancellationToken cancellationToken)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
