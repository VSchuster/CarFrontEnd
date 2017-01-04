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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using CarApplication;
using CarApplication.Models;
using Windows.Storage.FileProperties;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace CarApplication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Library : Page
    {

        private List<Artist> AllArtists;
        public List<Album> AllAlbums;
        public List<Song> AllSongs;
        private StorageFolder root = KnownFolders.MusicLibrary;

        
        public Library()
        {
            this.InitializeComponent();
        }
         
        
        private async void Artists_Click(object sender, RoutedEventArgs e)
        {
            TypeSelection.Visibility = Visibility.Collapsed;
            LoadingRing.IsActive = true;
            AllArtists = new List<Artist>();
            AllArtists = await PopulateArtistList(root);
            ArtistsGrid.ItemsSource = AllArtists;
            LoadingRing.IsActive = false;
            if(ArtistSelection.Visibility == Visibility.Collapsed)
                ArtistSelection.Visibility = Visibility.Visible;
        }

        private async void Albums_Click(object sender, RoutedEventArgs e)
        {
            TypeSelection.Visibility = Visibility.Collapsed;
            LoadingRing.IsActive = true;

            AllAlbums = new List<Album>();
            StorageFolder root = KnownFolders.MusicLibrary;
            
            foreach (StorageFolder folder in await root.GetFoldersAsync())
            {
                var albumsToAdd = await PopulateAlbumList(folder);
                foreach (Album album in albumsToAdd)
                {
                    AllAlbums.Add(album);
                }
            }
            AlbumGrid.ItemsSource = AllAlbums;
            LoadingRing.IsActive = false;
            if (AlbumSelection.Visibility == Visibility.Collapsed)
                AlbumSelection.Visibility = Visibility.Visible;
        }

        private async void Songs_Click(object sender, RoutedEventArgs e)
        {
            TypeSelection.Visibility = Visibility.Collapsed;
            LoadingRing.IsActive = true;
            AllSongs = new List<Song>();

            foreach (StorageFolder artistFolder in await root.GetFoldersAsync())
            {
                foreach (StorageFolder albumFolder in await artistFolder.GetFoldersAsync())
                {
                    List<Song> songsToAdd = await PopulateSongList(albumFolder);
                    foreach (Song song in songsToAdd)
                    {
                        AllSongs.Add(song);
                    }
                    
                }
            }

            LoadingRing.IsActive = false;
            SongList.ItemsSource = AllSongs;
            if (SongSelection.Visibility == Visibility.Collapsed)
                SongSelection.Visibility = Visibility.Visible;
        }

        



        // Iterates over the Parent folder and returns <List<Artist>>.
        private async Task<List<Artist>> PopulateArtistList(StorageFolder Parent)
        {
            foreach (var folder in await Parent.GetFoldersAsync())
            {
                var artistToAdd = new Artist {
                    name = folder.Name,
                    photo = new BitmapImage(new Uri(this.BaseUri, "Assets/Square150x150Logo.scale-200.png")),
                    folder = folder
                };

                    //get the file source and store it into an image if there is one
                foreach (var file in await folder.GetFilesAsync())
                {
                    if (file.DisplayName == "ArtistPhoto")
                    {
                        StorageItemThumbnail currentThumb = await file.GetThumbnailAsync(
                        ThumbnailMode.MusicView,
                        200,
                        ThumbnailOptions.UseCurrentScale);

                        var artistThumb = new BitmapImage();
                        artistThumb.SetSource(currentThumb);
                        artistToAdd.photo = artistThumb;
                    }
                }
                    AllArtists.Add(artistToAdd);
            }         

            
            return AllArtists;
        }

        private async void ArtistSelection_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 1. change view to albums displayed (Play All should be an option)
            if (ArtistSelection.Visibility == Visibility.Visible)
                ArtistSelection.Visibility = Visibility.Collapsed;
            else if (TypeSelection.Visibility == Visibility.Visible)
                TypeSelection.Visibility = Visibility.Collapsed;

            LoadingRing.Visibility = Visibility.Visible;
            LoadingRing.IsActive = true;

            var clickedArtist = (Artist)e.ClickedItem;
            var clickedArtistFolder = clickedArtist.folder;
            List<Album> ClickedAlbums = await PopulateAlbumList(clickedArtistFolder);
            AlbumGrid.ItemsSource = ClickedAlbums;

            LoadingRing.Visibility = Visibility.Collapsed;
            LoadingRing.IsActive = false;
            AlbumSelection.Visibility = Visibility.Visible;
        }

        private async Task<List<Album>> PopulateAlbumList(StorageFolder Parent)
        {
            var albums = new List<Album>();
            foreach (var folder in await Parent.GetFoldersAsync())
            {
                 var albumToAdd = new Album {
                    name = folder.Name,
                    cover = new BitmapImage(new Uri(this.BaseUri, "Assets/Square150x150Logo.scale-200.png")),
                    folder = folder
                };

                    //get the file source and store it into an image if there is one
                foreach (var file in await folder.GetFilesAsync())
                {
                    if (file.DisplayName == "AlbumCover")
                    {
                        StorageItemThumbnail currentThumb = await file.GetThumbnailAsync(
                        ThumbnailMode.MusicView,
                        200,
                        ThumbnailOptions.UseCurrentScale);

                        var albumThumb = new BitmapImage();
                        albumThumb.SetSource(currentThumb);
                        albumToAdd.cover = albumThumb;
                    }
                }
                    albums.Add(albumToAdd);
            }
            return albums;     
        }

        private async void AlbumGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (AlbumSelection.Visibility == Visibility.Visible)
                AlbumSelection.Visibility = Visibility.Collapsed;

            LoadingRing.Visibility = Visibility.Visible;
            LoadingRing.IsActive = true;

            var clickedAlbum = (Album)e.ClickedItem;
            List<Song> ClickedAlbum = await PopulateSongList(clickedAlbum.folder);
            SongList.ItemsSource = ClickedAlbum;

            LoadingRing.Visibility = Visibility.Collapsed;
            LoadingRing.IsActive = false;
            if (SongSelection.Visibility == Visibility.Collapsed)
                SongSelection.Visibility = Visibility.Visible;
        }

        private async Task<List<Song>> PopulateSongList(StorageFolder parent)
        {
            var songs = new List<Song>();
            var albumThumb = new BitmapImage(new Uri(this.BaseUri, "Assets/Square150x150Logo.scale-200.png"));
            foreach (var file in await parent.GetFilesAsync())
            {
                if (file.DisplayName == "AlbumCover")
                {
                    StorageItemThumbnail currentThumb = await file.GetThumbnailAsync(
                       ThumbnailMode.MusicView,
                       200,
                       ThumbnailOptions.UseCurrentScale);
                    albumThumb.SetSource(currentThumb);
                }
            }
            foreach (var file in await parent.GetFilesAsync())
            {
                if (file.FileType == ".mp3")
                {
                    MusicProperties songProperties = await file.Properties.GetMusicPropertiesAsync();
                    var songToAdd = new Song
                    {
                        name = songProperties.Title,
                        artist = songProperties.Artist,
                        album = parent.DisplayName,
                        bitrate = songProperties.Bitrate,
                        duration = songProperties.Duration,
                        //length = Duration_Converter(),
                        albumCover = albumThumb,
                        
                    };
                    songs.Add(songToAdd);
                }           
            }
            return songs;
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            Music music = new Music();
            music.songNowPlaying = (Song)e.ClickedItem;
            
        }

       

        
    }


}

