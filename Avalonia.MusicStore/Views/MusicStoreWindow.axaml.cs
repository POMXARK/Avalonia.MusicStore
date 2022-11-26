
using Avalonia.MusicStore.ViewModels;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;

namespace Avalonia.MusicStore.Views
{
    public partial class MusicStoreWindow : ReactiveWindow<MusicStoreViewModel>
    {
        public MusicStoreWindow()
        {
            InitializeComponent();
            //This line says when the Window is activated (becomes visible on the screen), the lambda expression will be called.
            //The d is an action that we can pass a disposable to, so things can be unsubscribed when the Window is no longer active.
            // This means the result of the BuyMusicCommand will be passed to the Close
            this.WhenActivated(d => d(ViewModel!.BuyMusicCommand.Subscribe(Close)));
        }
    }
}
