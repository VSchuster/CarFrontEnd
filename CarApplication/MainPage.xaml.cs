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
using CarApplication.Models;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace CarApplication
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        
        public MainPage()
        {
            this.InitializeComponent();
            if (BackButton.Visibility == Visibility.Visible)
            {
                BackButton.Visibility = Visibility.Collapsed;
            }
            //StorageFolder Library = KnownFolders.MusicLibrary;
            var AllArtists = new ObservableCollection<StorageFolder>();
            var AllAlbums = new ObservableCollection<StorageFolder>();
            var AllSongs = new ObservableCollection<StorageFile>();
            var AllPlaylists = new ObservableCollection<StorageFolder>();
            var SearchList = new ObservableCollection<StorageItemTypes>();
            MainFrame.Navigate(typeof(Home));
        }

        //Hambergermenu Click opens the SplitView Pane
        private void HamburgerMenu_Click(object sender, RoutedEventArgs e)
        {
            NavigationBar.IsPaneOpen = !NavigationBar.IsPaneOpen;
        }

        //Selection for ListBox in NavigationBar
        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Home.IsSelected)
            {
                BackButton.Visibility = Visibility.Collapsed;
                Set_SecondaryNavigation("Home");
                Set_NowPlaying_On(true);
                MainFrame.Navigate(typeof(Home));
                //Check to make correct SecondaryNavigation ListBoxItems are displayed
            }
            else if (Music.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                Set_SecondaryNavigation("Music");
                Set_NowPlaying_On(false);
                MainFrame.Navigate(typeof(Music));
            }
            else if (Navigation.IsSelected)
            {
                /*
                var uriBing = new Uri(@"bingmaps:?");

                // Launch the URI
                var success = await Windows.System.Launcher.LaunchUriAsync(uriBing);

                if (!success)
                {
                    // URI did not launched
                    var dialog = new Windows.UI.Popups.MessageDialog("Maps has failed to launch");

                    dialog.Commands.Add(new Windows.UI.Popups.UICommand("Continue") { Id = 0 });
                }
               */
                BackButton.Visibility = Visibility.Visible;
                Set_SecondaryNavigation("Navigation");
                Set_NowPlaying_On(true);
                MainFrame.Navigate(typeof(Navigation));
            }
            else if (Daily.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                Set_SecondaryNavigation("Daily");
                Set_NowPlaying_On(true);
                MainFrame.Navigate(typeof(Daily));
            }
            else if (Datalog.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                Set_SecondaryNavigation("Datalog");
                MainFrame.Navigate(typeof(Datalog));
                Set_NowPlaying_On(true);
            }
            else if (Settings.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                Set_SecondaryNavigation("Settings");
                Set_NowPlaying_On(true);
                MainFrame.Navigate(typeof(Settings));
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainFrame.CanGoBack)
            {
                MainFrame.GoBack();
            }
        }

        private void SecondaryNavigation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Library.IsSelected)
            {
                Set_NowPlaying_On(true);
                MainFrame.Navigate(typeof(Library));
            }
            else if (DSP.IsSelected)
            {
                Set_NowPlaying_On(true);
                MainFrame.Navigate(typeof(DSP));
            }
            else if (NowPlaying.IsSelected)
            {
                Set_NowPlaying_On(false);
                MainFrame.Navigate(typeof(Music));
            }
            else if(Route.IsSelected)
            {
                Set_NowPlaying_On(true);
                //MainFrame.Navigate(typeof(Route));
            }
            else if (Map.IsSelected)
            {
                Set_NowPlaying_On(true);
                //MainFrame.Navigate(typeof(Map));
            }
            else if (Maintenance.IsSelected)
            {
                Set_NowPlaying_On(true);
                //MainFrame.Navigate(typeof(Maintenance));
            }

            else if (Gauges.IsSelected)
            {
                Set_NowPlaying_On(true);
                MainFrame.Navigate(typeof(Guages));
            }
            else if (Tuning.IsSelected)
            {
                Set_NowPlaying_On(true);
                //MainFrame.Navigate(typeof(Tuning));
            }
        }

        private void Set_SecondaryNavigation(String Selection)
        {
            if (Selection == "Navigation")
            {
                if (NowPlaying.Visibility == Visibility.Visible)
                {
                    NowPlaying.Visibility = Visibility.Collapsed;
                }
                if (Library.Visibility == Visibility.Visible)
                {
                    Library.Visibility = Visibility.Collapsed;
                }
                if (DSP.Visibility == Visibility.Visible)
                {
                    DSP.Visibility = Visibility.Collapsed;
                }
                if (Route.Visibility == Visibility.Collapsed)
                {
                    Route.Visibility = Visibility.Visible;
                }
                if (Maintenance.Visibility == Visibility.Visible)
                {
                    Maintenance.Visibility = Visibility.Collapsed;
                }
                if (Gauges.Visibility == Visibility.Visible)
                {
                    Gauges.Visibility = Visibility.Collapsed;
                }
                if (Map.Visibility == Visibility.Collapsed)
                {
                    Map.Visibility = Visibility.Visible;
                }
                if (Tuning.Visibility == Visibility.Visible)
                {
                    Tuning.Visibility = Visibility.Collapsed;
                }
            }
            else if (Selection == "Datalog")
            {
                if (NowPlaying.Visibility == Visibility.Visible)
                {
                    NowPlaying.Visibility = Visibility.Collapsed;
                }
                if (Library.Visibility == Visibility.Visible)
                {
                    Library.Visibility = Visibility.Collapsed;
                }
                if (DSP.Visibility == Visibility.Visible)
                {
                    DSP.Visibility = Visibility.Collapsed;
                }
                if (Route.Visibility == Visibility.Visible)
                {
                    Route.Visibility = Visibility.Collapsed;
                }
                if (Maintenance.Visibility == Visibility.Collapsed)
                {
                    Maintenance.Visibility = Visibility.Visible;
                }
                if (Gauges.Visibility == Visibility.Collapsed)
                {
                    Gauges.Visibility = Visibility.Visible;
                }
                if (Map.Visibility == Visibility.Visible)
                {
                    Map.Visibility = Visibility.Collapsed;
                }
                if (Tuning.Visibility == Visibility.Collapsed)
                {
                    Tuning.Visibility = Visibility.Visible;
                }
            }
            else if (Selection == "Music")
            {
                if (NowPlaying.Visibility == Visibility.Collapsed)
                {
                    NowPlaying.Visibility = Visibility.Visible;
                }
                if (Library.Visibility == Visibility.Collapsed)
                {
                    Library.Visibility = Visibility.Visible;
                }
                if (DSP.Visibility == Visibility.Collapsed)
                {
                    DSP.Visibility = Visibility.Visible;
                }
                if (Route.Visibility == Visibility.Visible)
                {
                    Route.Visibility = Visibility.Collapsed;
                }
                if (Maintenance.Visibility == Visibility.Visible)
                {
                    Maintenance.Visibility = Visibility.Collapsed;
                }
                if (Gauges.Visibility == Visibility.Visible)
                {
                    Gauges.Visibility = Visibility.Collapsed;
                }
                if (Map.Visibility == Visibility.Visible)
                {
                    Map.Visibility = Visibility.Collapsed;
                }
                if (Tuning.Visibility == Visibility.Visible)
                {
                    Tuning.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                if (NowPlaying.Visibility == Visibility.Visible)
                {
                    NowPlaying.Visibility = Visibility.Collapsed;
                }
                if (Library.Visibility == Visibility.Visible)
                {
                    Library.Visibility = Visibility.Collapsed;
                }
                if (DSP.Visibility == Visibility.Visible)
                {
                    DSP.Visibility = Visibility.Collapsed;
                }
                if (Route.Visibility == Visibility.Visible)
                {
                    Route.Visibility = Visibility.Collapsed;
                }
                if (Maintenance.Visibility == Visibility.Visible)
                {
                    Maintenance.Visibility = Visibility.Collapsed;
                }
                if (Gauges.Visibility == Visibility.Visible)
                {
                    Gauges.Visibility = Visibility.Collapsed;
                }
                if (Map.Visibility == Visibility.Visible)
                {
                    Map.Visibility = Visibility.Collapsed;
                }
                if (Tuning.Visibility == Visibility.Visible)
                {
                    Tuning.Visibility = Visibility.Collapsed;
                }
            }        
        }
        private void Set_NowPlaying_On(Boolean On)
        {
            if (On)
            {
                if (NowPlayingInfoRight.Visibility == Visibility.Collapsed)
                {
                    NowPlayingInfoRight.Visibility = Visibility.Visible;
                    NowPlayingInfoLeft.Visibility = Visibility.Visible;
                }
            }
            else
            {
                if (NowPlayingInfoRight.Visibility == Visibility.Visible)
                {
                    NowPlayingInfoRight.Visibility = Visibility.Collapsed;
                    NowPlayingInfoLeft.Visibility = Visibility.Collapsed;
                }
            }
        }
        
        private async Task GetArtists(
            ObservableCollection<StorageFolder> Folder,
            StorageFolder Parent)
        {
            foreach (var item in await Parent.GetFoldersAsync())
            {
                Folder.Add(item);
            }
        }

        


        private async Task GetSongs(
            ObservableCollection<StorageFile> Songs,
            StorageFolder Parent)
        {
            foreach (var item in await Parent.GetFilesAsync())
            {
                Songs.Add(item);
            }
        }
    }
}
