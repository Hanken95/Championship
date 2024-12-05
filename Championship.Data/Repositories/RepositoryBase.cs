using Championchip.Core.Repositories;
using Championship.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Championship.Data.Repositories
{
    public abstract class RepositoryBase<T>(ChampionshipContext context) : IRepositoryBase<T> where T : class
    {
        protected DbSet<T> DbSet { get; set; } = context.Set<T>();

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> conditions)
        {
            return await DbSet.AnyAsync(conditions);
        }
        public async Task<bool> AnyAsync()
        {
            return await DbSet.AnyAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> conditions)
        {
            return await DbSet.Where(conditions).ToListAsync();
        }

        public async Task<T?> GetAsync(Expression<Func<T, bool>> conditions)
        {
            return await DbSet.FirstOrDefaultAsync(conditions);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
        
    }
}
