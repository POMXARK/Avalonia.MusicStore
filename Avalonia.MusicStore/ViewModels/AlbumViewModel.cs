using System;
using System.Collections.Generic;
using Avalonia.MusicStore.Models;
using ReactiveUI;

namespace Avalonia.MusicStore.ViewModels
{
	public class AlbumViewModel : ViewModelBase
    {
        private readonly Album _album;

        public AlbumViewModel(Album album)
        {
            _album = album;
        }

        public string Artist => _album.Artist;

        public string Title => _album.Title;
    }
}