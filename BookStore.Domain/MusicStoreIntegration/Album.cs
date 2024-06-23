using BookStore.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.MusicStoreIntegration
{
    public class Album :BaseEntity
    {
        public string AlbumName {  get; set; }
        public int AlbumPrice { get; set; }
        public string AlbumImage { get; set; }
       public Guid ArtistId { get; set; }
        public Artist Artist { get; set; }
    }
}
