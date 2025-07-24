using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data
{
    public class GenericRepository<T>(StoreContext context) : IGenericRepository<T> where T : BaseEntity
    {
        private readonly StoreContext _context = context;

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            var maybeT = await _context.Set<T>().FindAsync(id);

            return maybeT ?? throw new InvalidOperationException($"Entity {typeof(T)} not found with id {id}.");
        }

        public async Task<T> GetEntityWithSpecification(ISpecifications<T> specifications)
        {
            var maybeT = await ApplySpecification(specifications).FirstOrDefaultAsync();
            return maybeT ?? throw new InvalidOperationException($"Entity {typeof(T)} not found.");
        }

        public async Task<IReadOnlyList<T>> ListEntityWithSpecificationAsync(ISpecifications<T> specifications)
        {
            return await ApplySpecification(specifications).ToListAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecifications<T> specifications)
        {
            return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(), specifications);
        }
    }
}