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
    public class RepositoryBase<T>(ChampionshipContext context) : IRepositoryBase<T> where T : class
    {
        protected DbSet<T> DbSet { get; set; } = context.Set<T>();

        public void Add(T entity)
        {
            DbSet.Add(entity);
        }

        public async Task<bool> AnyAsync(int id)
        {
            return await DbSet.FindAsync(id) == null;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<T?> GetAsync(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public void Remove(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Update(T entity)
        {
            DbSet.Update(entity);
        }
        
        public async Task<IEnumerable<T>> FindByCondition(Expression<Func<T, bool>> conditions)
        {
            return await DbSet.Where(conditions).ToListAsync();
        }
    }
}
