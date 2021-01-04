using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Data.Context;
using Api.Domain.Entities;
using Api.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;

        public BaseRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                if (entity.Id == Guid.Empty)
                    entity.Id = Guid.NewGuid();

                entity.CreatedAt = DateTime.UtcNow;

                _context.Set<T>().Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return entity;
        }

        public Task<bool> DeleteAsync(Guid id)
        {
        }

        public Task<IEnumerable<T>> GetAsync()
        {
        }

        public Task<T> GetById(Guid id)
        {
        }

        public Task<T> UpdateAsync(T entity)
        {
        }
    }
}