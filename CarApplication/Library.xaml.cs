using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using CarApplication;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CarApplication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Library : Page
    {
        public Library()
        {
            this.InitializeComponent();
        }
         
        private async void Artists_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TypeSelection.Visibility = Visibility.Collapsed;
            ArtistSelection.Visibility = Visibility.Visible;
            StorageFolder root = KnownFolders.MusicLibrary;
            var AllArtists = new ObservableCollection<StorageFolder>();
            await RetrieveFoldersinFolder(root, AllArtists);

        }

        private async void Albums_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TypeSelection.Visibility = Visibility.Collapsed;
            AlbumSelection.Visibility = Visibility.Visible;
            StorageFolder root = KnownFolders.MusicLibrary;
            var AllArtists = new ObservableCollection<StorageFolder>();
            var AllAlbums = new ObservableCollection<StorageFolder>();
            await RetrieveFoldersinFolder(root, AllArtists);
            foreach (var item in AllArtists)
            {
               await RetrieveFoldersinFolder(item, AllAlbums);
            }
        }

        private void Playlists_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TypeSelection.Visibility = Visibility.Collapsed;
            PlaylistSelection.Visibility = Visibility.Visible;
        }

        private async void Songs_Tapped(object sender, TappedRoutedEventArgs e)
        {
            TypeSelection.Visibility = Visibility.Collapsed;
            SongSelection.Visibility = Visibility.Visible;
            StorageFolder root = KnownFolders.MusicLibrary;
            var AllSongs = new ObservableCollection<StorageFile>();
            await RetrieveAllFilesOfTypeInFolder(root, AllSongs, ".mp3");
        }

        private async Task RetrieveAllFilesOfTypeInFolder(StorageFolder Parent, 
            ObservableCollection<StorageFile> Files,
            String TypeOfFile)
        {
            foreach (var item in await Parent.GetFoldersAsync())
            {
                if (item.GetType() == typeof(StorageFolder))
                {
                    await RetrieveAllFilesOfTypeInFolder(Parent, Files, TypeOfFile);
                }
            }
            await RetrieveFilesinFolder(Parent, Files, TypeOfFile);
        }

        private async Task RetrieveFoldersinFolder(StorageFolder Parent,
            ObservableCollection<StorageFolder> Folders)
        {
            foreach (var item in await Parent.GetFoldersAsync())
            {
                if (item.GetType() == typeof(StorageFolder))
                {
                    Folders.Add(item);
                }
            }
        }

        private async Task RetrieveFilesinFolder(StorageFolder Parent,
            ObservableCollection<StorageFile> Files,
            String TypeOfFile)
        {
            foreach (var item in await Parent.GetFilesAsync())
            {
                if (item.FileType == TypeOfFile)
                {
                    Files.Add(item);
                }
            }
        }

        private void ArtistSelection_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
