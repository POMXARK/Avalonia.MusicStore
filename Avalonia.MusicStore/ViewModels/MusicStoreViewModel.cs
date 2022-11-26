using Avalonia.MusicStore.Models;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;

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
            BuyMusicCommand = ReactiveCommand.Create(() =>
            {
                return SelectedAlbum;
            });

            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(400))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(DoSearch!);
        }

        private CancellationTokenSource? _cancellationTokenSource;

        private async void DoSearch(string s)
        {
            IsBusy = true;
            SearchResults.Clear();

            if (!string.IsNullOrWhiteSpace(s))
            {
                var albums = await Album.SearchAsync(s);

                foreach (var album in albums)
                {
                    var vm = new AlbumViewModel(album);

                    SearchResults.Add(vm);
                }
            }

            IsBusy = false;
        }

        public ReactiveCommand<Unit, AlbumViewModel?> BuyMusicCommand { get; }
    }
}
