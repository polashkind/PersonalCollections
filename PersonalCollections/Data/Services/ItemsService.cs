using System;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
			var result = await _dbSet.AsNoTracking().Include(i => i.Collection).Include(ic => ic.Comments).ToListAsync(cancellationToken);
			return result;
		}

        public async Task<Item> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _dbSet.AsNoTracking().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
            return result;
        }

        public async Task<Item?> Create(Item item, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(item);
            await _context.SaveChangesAsync(cancellationToken);
            return item;
        }

        public async Task Delete(Item item, CancellationToken cancellationToken)
        {
            _dbSet.Remove(item);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<Item?> Update(Item item, CancellationToken cancellationToken)
        {
            _context.Entry(item).State = EntityState.Modified;
            _dbSet.Update(item);
            await _context.SaveChangesAsync(cancellationToken);
            return item;
        }

    }
}

