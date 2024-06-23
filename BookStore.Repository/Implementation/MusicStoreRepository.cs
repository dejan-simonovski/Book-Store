using BookStore.Domain.Domain;
using BookStore.Domain.MusicStoreIntegration;
using BookStore.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Implementation
{
    public class MusicStoreRepository<T> : IMusicStoreRepository<T> where T : BaseEntity
    {
        private readonly MusicStoreDbContext context;
        private DbSet<T> entities;
        string errorMessage = string.Empty;

        public MusicStoreRepository(MusicStoreDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }
        public IEnumerable<T> GetAll()
        {
            if (typeof(T).IsAssignableFrom(typeof(Album)))
            {
                return entities
                       .Include("Artist")
                      .AsEnumerable();
            }
            else
                return entities.AsEnumerable();
        }
    }
}
