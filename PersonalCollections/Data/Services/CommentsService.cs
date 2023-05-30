using System;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PersonalCollections.Data.Interfaces;
using PersonalCollections.Models;

namespace PersonalCollections.Data.Services
{
    public class CommentsService : ICommentsService
    {
        private readonly AppDbContext _context;
        protected readonly DbSet<Comment> _dbSet;

        public CommentsService(AppDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<Comment>();
        }

        public async Task<Comment?> GetById(int id, CancellationToken cancellationToken)
        {
            var result = await _dbSet
                .Include(i => i.CreatedBy)
                .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

            return result;
        }
    }
}
