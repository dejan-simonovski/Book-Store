using BookStore.Domain.MusicStoreIntegration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Service.Interface
{
    public interface IAlbumService
    {

        List<Album> GetAlbums();
    }
}
