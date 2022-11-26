using ReactiveUI;
using System.Reactive.Linq;
using System.Windows.Input;

namespace Avalonia.MusicStore.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            ShowDialog = new Interaction<MusicStoreViewModel, AlbumViewModel?>();

            BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new MusicStoreViewModel();

                var result = await ShowDialog.Handle(store);
            });
        }
        public ICommand BuyMusicCommand { get; }
        public Interaction<MusicStoreViewModel, AlbumViewModel?> ShowDialog { get; }
    }      
}
