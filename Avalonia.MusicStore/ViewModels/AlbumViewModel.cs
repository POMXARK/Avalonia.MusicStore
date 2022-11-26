
using System.Threading.Tasks;
using Avalonia.Media.Imaging;
using Avalonia.MusicStore.Models;
using ReactiveUI.Fody.Helpers;

namespace Avalonia.MusicStore.ViewModels
{
	public class AlbumViewModel : ViewModelBase
    {
        [Reactive] Bitmap? Cover { get; set; }

        private readonly Album _album;

        public AlbumViewModel(Album album)
        {
            _album = album;
        }

        public async Task LoadCover()
        {
            await using (var imageStream = await _album.LoadCoverBitmapAsync())
            {
                Cover = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
            }
        }

        public string Artist => _album.Artist;

        public string Title => _album.Title;
    }
}