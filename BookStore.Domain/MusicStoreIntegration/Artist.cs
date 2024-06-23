using BookStore.Domain.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Domain.MusicStoreIntegration
{
    public class Artist :BaseEntity
    {
        public string FirstName {  get; set; }
        public string LastName { get; set; }
        
        public int Age { get; set; }
        public ICollection<Album> Albums { get; set; } = new List<Album>();
    }
}
