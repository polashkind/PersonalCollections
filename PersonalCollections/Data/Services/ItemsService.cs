using System;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalCollections.Data.Interfaces;
using PersonalCollections.Data.ViewModels;
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

        public async Task<Item> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _dbSet
                .Include(c => c.CreatedBy)
                .Include(c => c.UpdatedBy)
                .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
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

        public async Task<NewItemDropdownsVM> GetNewItemDropdownsValues()
        {
            var response = new NewItemDropdownsVM()
            {
                Collections = await _context.Collections.OrderBy(c => c.Title).ToListAsync(),
            };

            return response;
        }

        public async Task Update(ItemVM data)
        {

            var dbItem = await _context.Items.FirstOrDefaultAsync(i => i.Id == data.Id);

            if (dbItem != null)
            {
                dbItem.Title = data.Title;
                dbItem.Description = data.Description;
                //dbItem.CollectionId = data.CollectionId;
                await _context.SaveChangesAsync();
            }

        }
    }
}

