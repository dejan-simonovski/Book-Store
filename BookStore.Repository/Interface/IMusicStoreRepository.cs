using BookStore.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Repository.Interface
{
    public interface IMusicStoreRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
    }
}
