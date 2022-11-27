using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Collections.ObjectModel;
using System.Reactive.Linq;
using System.Windows.Input;
using System;
using System.Linq;
using System.Reactive.Concurrency;
using Avalonia.MusicStore.Models;

namespace Avalonia.MusicStore.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        [Reactive] bool CollectionEmpty { get; set; }

        public MainWindowViewModel()
        {
            ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();

            BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new MusicStoreViewModel();

                var result = await ShowDialog.Handle(store);

                if (result != null)
                {
                    Albums.Add(result);

                    await result.SaveToDiskAsync();
                }
            });

            this.WhenAnyValue(x => x.Albums.Count)
                .Subscribe(x => CollectionEmpty = x == 0);

            RxApp.MainThreadScheduler.Schedule(LoadAlbums);
        }

        private async void LoadAlbums()
        {
            var albums = (await Album.LoadCachedAsync()).Select(x => new AlbumViewModel(x));

            foreach (var album in albums)
            {
                Albums.Add(album);
            }

            foreach (var album in Albums.ToList())
            {
                await album.LoadCover();
            }
        }

        public ICommand BuyMusicCommand { get; }
        public Interaction<MusicStoreViewModel, AlbumViewModel?> ShowDialog { get; }
        public ObservableCollection<AlbumViewModel> Albums { get; } = new();
    }      
}
