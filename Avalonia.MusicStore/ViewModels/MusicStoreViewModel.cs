using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;

namespace Avalonia.MusicStore.ViewModels
{
    public class MusicStoreViewModel : ViewModelBase
    {
        [Reactive] string SearchText {get; set;}
        [Reactive] bool IsBusy { get; set; }
        [Reactive] AlbumViewModel? SelectedAlbum { get; set; }

        public ObservableCollection<AlbumViewModel> SearchResults { get; } = new();

        public MusicStoreViewModel()
        {
            SearchResults.Add(new AlbumViewModel());
            SearchResults.Add(new AlbumViewModel());
            SearchResults.Add(new AlbumViewModel());
        }
    }
}
