using BookStore.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Web.Controllers
{
    public class MusicStoreController : Controller
    {
        private readonly IAlbumService albumService;

        public MusicStoreController(IAlbumService albumService)
        {
            this.albumService = albumService;
        }

        public IActionResult Index()
        {
            return View(albumService.GetAlbums());
        }
    }
}
