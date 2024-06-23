using BookStore.Domain.MusicStoreIntegration;
using BookStore.Repository.Interface;
using BookStore.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Implementation
{
    public class AlbumService : IAlbumService
    {
        private readonly IMusicStoreRepository<Album> repository;

        public AlbumService(IMusicStoreRepository<Album> repository)
        {
            this.repository = repository;
        }

        public List<Album> GetAlbums()
        {
           return  repository.GetAll().ToList();
        }
    }
}
