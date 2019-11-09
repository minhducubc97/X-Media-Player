using CefSharp;
using CefSharp.Wpf;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Win32;
using NReco.VideoConverter;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using WindowMediaPlayer.Model;
using YoutubeExplode;
using YoutubeExplode.Models.MediaStreams;

namespace WindowMediaPlayer.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// See http://www.mvvmlight.net
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Variables declaration
        /// </summary>
        /// 
        private bool isDragged;
        private double temporaryVolume;
        private bool IsFullScreen;
        private DispatcherTimer timer;

        /// <summary>
        /// Changes to that property's value raise the PropertyChanged event. 
        /// </summary>
        /// 

        private MediaElement _mediaPlayer;
        public MediaElement mediaPlayer
        {
            get
            {
                return _mediaPlayer;
            }
            set
            {
                _mediaPlayer = value;
                RaisePropertyChanged("mediaPlayer");
            }
        }

        private ICommand _findAudio;
        public ICommand FindAudio
        {
            get
            {
                return _findAudio;
            }
            set
            {
                _findAudio = value;
                RaisePropertyChanged("FindAudio");
            }
        }

        private ICommand _play;
        public ICommand Play
        {
            get
            {
                return _play;
            }
            set
            {
                _play = value;
                RaisePropertyChanged("Play");
            }
        }

        private ICommand _stop;
        public ICommand Stop
        {
            get
            {
                return _stop;
            }
            set
            {
                _stop = value;
                RaisePropertyChanged("Stop");
            }
        }

        private string _mainInstruction;
        public string MainInstruction
        {
            get
            {
                return _mainInstruction;
            }
            set
            {
                _mainInstruction = value;
                RaisePropertyChanged("MainInstruction");
            }
        }

        private double _maximumMediaLength;
        public double MaximumMediaLength
        {
            get
            {
                return _maximumMediaLength;
            }
            set
            {
                _maximumMediaLength = value;
                RaisePropertyChanged("MaximumMediaLength");
            }
        }

        private double _mediaProgress;
        public double MediaProgress
        {
            get
            {
                return _mediaProgress;
            }
            set
            {
                _mediaProgress = value;
                mediaPlayer.Position = System.TimeSpan.FromSeconds(value);
                RaisePropertyChanged("MediaProgress");
            }
        }

        private ICommand _dragCompletedCommand;
        public ICommand DragCompletedCommand
        {
            get
            {
                return _dragCompletedCommand;
            }
            set
            {
                _dragCompletedCommand = value;
                RaisePropertyChanged("DragCompletedCommand");
            }
        }

        public double Volume
        {
            get
            {
                return mediaPlayer.Volume;
            }
            set
            {
                mediaPlayer.Volume = value;
                RaisePropertyChanged("Volume");
            }
        }

        private bool _isMute = false;
        public bool IsMute
        {
            get
            {
                return _isMute;
            }
            set
            {
                _isMute = value;
                RaisePropertyChanged("IsMute");
            }
        }

        private ICommand _mute;
        public ICommand Mute
        {
            get
            {
                return _mute;
            }
            set
            {
                _mute = value;
                RaisePropertyChanged("Mute");
            }
        }

        private string _speakerNormal;
        public string SpeakerNormal
        {
            get
            {
                return _speakerNormal;
            }
            set
            {
                _speakerNormal = value;
                RaisePropertyChanged("SpeakerNormal");
            }
        }

        private string _speakerHover;
        public string SpeakerHover
        {
            get
            {
                return _speakerHover;
            }
            set
            {
                _speakerHover = value;
                RaisePropertyChanged("SpeakerHover");
            }
        }

        private string _speakerPressed;
        public string SpeakerPressed
        {
            get
            {
                return _speakerPressed;
            }
            set
            {
                _speakerPressed = value;
                RaisePropertyChanged("SpeakerPressed");
            }
        }

        private string _playNormal;
        public string PlayNormal
        {
            get
            {
                return _playNormal;
            }
            set
            {
                _playNormal = value;
                RaisePropertyChanged("PlayNormal");
            }
        }

        private string _playHover;
        public string PlayHover
        {
            get
            {
                return _playHover;
            }
            set
            {
                _playHover = value;
                RaisePropertyChanged("PlayHover");
            }
        }

        private string _playPressed;
        public string PlayPressed
        {
            get
            {
                return _playPressed;
            }
            set
            {
                _playPressed = value;
                RaisePropertyChanged("PlayPressed");
            }
        }

        private bool _isPlaying;
        public bool IsPlaying
        {
            get
            {
                return _isPlaying;
            }
            set
            {
                _isPlaying = value;
                RaisePropertyChanged("IsPlaying");
            }
        }

        private bool _isMediaAvailable;
        public bool IsMediaAvailable
        {
            get
            {
                return _isMediaAvailable;
            }
            set
            {
                _isMediaAvailable = value;
                RaisePropertyChanged("IsMediaAvailable");
            }
        }

        private ICommand _fullScreen;
        public ICommand FullScreen
        {
            get
            {
                return _fullScreen;
            }
            set
            {
                _fullScreen = value;
                RaisePropertyChanged("FullScreen");
            }
        }

        private WindowStyle _windowStyle;
        public WindowStyle WindowStyle0
        {
            get
            {
                return _windowStyle;
            }
            set
            {
                _windowStyle = value;
                RaisePropertyChanged("WindowStyle0");
            }
        }

        private WindowState _windowState;
        public WindowState WindowState0
        {
            get
            {
                return _windowState;
            }
            set
            {
                _windowState = value;
                RaisePropertyChanged("WindowState0");
            }
        }

        private string _fullScreenNormal;
        public string FullScreenNormal
        {
            get
            {
                return _fullScreenNormal;
            }
            set
            {
                _fullScreenNormal = value;
                RaisePropertyChanged("FullScreenNormal");
            }
        }

        private string _fullScreenHover;
        public string FullScreenHover
        {
            get
            {
                return _fullScreenHover;
            }
            set
            {
                _fullScreenHover = value;
                RaisePropertyChanged("FullScreenHover");
            }
        }

        private string _fullScreenPressed;
        public string FullScreenPressed
        {
            get
            {
                return _fullScreenPressed;
            }
            set
            {
                _fullScreenPressed = value;
                RaisePropertyChanged("FullScreenPressed");
            }
        }

        private ICommand _moveForwards;
        public ICommand MoveForwards
        {
            get
            {
                return _moveForwards;
            }
            set
            {
                _moveForwards = value;
                RaisePropertyChanged("MoveForwards");
            }
        }

        private ICommand _moveBackwards;
        public ICommand MoveBackwards
        {
            get
            {
                return _moveBackwards;
            }
            set
            {
                _moveBackwards = value;
                RaisePropertyChanged("MoveBackwards");
            }
        }

        private int _mediaSpan;
        public int MediaSpan
        {
            get
            {
                return _mediaSpan;
            }
            set
            {
                _mediaSpan = value;
                RaisePropertyChanged("MediaSpan");
            }
        }

        private ICommand _playlist;
        public ICommand Playlist
        {
            get
            {
                return _playlist;
            }
            set
            {
                _playlist = value;
                RaisePropertyChanged("Playlist");
            }
        }

        private string _showPlaylist;
        public string ShowPlaylist
        {
            get
            {
                return _showPlaylist;
            }
            set
            {
                _showPlaylist = value;
                RaisePropertyChanged("ShowPlaylist");
            }
        }

        private int _mediaColumn;
        public int MediaColumn
        {
            get
            {
                return _mediaColumn;
            }
            set
            {
                _mediaColumn = value;
                RaisePropertyChanged("MediaColumn");
            }
        }

        private ObservableCollection<MediaSingleElement> _listOfMedia;
        public ObservableCollection<MediaSingleElement> ListOfMedia
        {
            get
            {
                return _listOfMedia;
            }
            set
            {
                _listOfMedia = value;
                RaisePropertyChanged("ListOfMedia");
            }
        }

        private ICommand _addMedia;
        public ICommand AddMedia
        {
            get
            {
                return _addMedia;
            }
            set
            {
                _addMedia = value;
                RaisePropertyChanged("AddMedia");
            }
        }

        private ICommand _removeMedia;
        public ICommand RemoveMedia
        {
            get
            {
                return _removeMedia;
            }
            set
            {
                _removeMedia = value;
                RaisePropertyChanged("RemoveMedia");
            }
        }

        private int _selectedIndex;
        public int SelectedIndex
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged("SelectedIndex");
            }
        }

        private ICommand _playNewMedia;
        public ICommand PlayNewMedia
        {
            get
            {
                return _playNewMedia;
            }
            set
            {
                _playNewMedia = value;
                RaisePropertyChanged("PlayNewMedia");
            }
        }

        private bool _isAddMediaEnabled;
        public bool IsAddMediaEnabled
        {
            get
            {
                return _isAddMediaEnabled;
            }
            set
            {
                _isAddMediaEnabled = value;
                RaisePropertyChanged("IsAddMediaEnabled");
            }
        }

        private bool _isRemoveMediaEnabled;
        public bool IsRemoveMediaEnabled
        {
            get
            {
                return _isRemoveMediaEnabled;
            }
            set
            {
                _isRemoveMediaEnabled = value;
                RaisePropertyChanged("IsRemoveMediaEnabled");
            }
        }

        private JSONCommunicator jsonCommunicator { get; set; }

        private string _showSpeedOptions;
        public string ShowSpeedOptions
        {
            get
            {
                return _showSpeedOptions;
            }
            set
            {
                _showSpeedOptions = value;
                RaisePropertyChanged("ShowSpeedOptions");
            }
        }

        private ObservableCollection<double> _listOfSpeeds;
        public ObservableCollection<double> ListOfSpeeds
        {
            get
            {
                return _listOfSpeeds;
            }
            set
            {
                _listOfSpeeds = value;
                RaisePropertyChanged("ListOfSpeeds");
            }
        }

        private double _chosenSpeed;
        public double ChosenSpeed
        {
            get
            {
                return _chosenSpeed;
            }
            set
            {
                _chosenSpeed = value;
                mediaPlayer.SpeedRatio = _chosenSpeed;
                ShowSpeedOptions = "Collapsed";
                RaisePropertyChanged("ChosenSpeed");
            }
        }

        private ICommand _setSpeed;
        public ICommand SetSpeed
        {
            get
            {
                return _setSpeed;
            }
            set
            {
                _setSpeed = value;
                RaisePropertyChanged("SetSpeed");
            }
        }

        private bool IsSpeedOptionsShown;

        private ICommand _shadowEffectFadeIn;
        public ICommand ShadowEffectFadeIn
        {
            get
            {
                return _shadowEffectFadeIn;
            }
            set
            {
                _shadowEffectFadeIn = value;
                RaisePropertyChanged("ShadowEffectFadeIn");
            }
        }

        private ICommand _shadowEffectFadeOut;
        public ICommand ShadowEffectFadeOut
        {
            get
            {
                return _shadowEffectFadeOut;
            }
            set
            {
                _shadowEffectFadeOut = value;
                RaisePropertyChanged("ShadowEffectFadeOut");
            }
        }

        private double _topPanelVisibility;
        public double TopPanelVisibility
        {
            get
            {
                return _topPanelVisibility;
            }
            set
            {
                _topPanelVisibility = value;
                RaisePropertyChanged("TopPanelVisibility");
            }
        }

        private DispatcherTimer Timer3 { get; set; }
        private DispatcherTimer Timer2 { get; set; }

        private int _debugger;
        public int Debugger
        {
            get
            {
                return _debugger;
            }
            set
            {
                _debugger = value;
                RaisePropertyChanged("Debugger");
            }
        }

        private int _debugger2;
        public int Debugger2
        {
            get
            {
                return _debugger2;
            }
            set
            {
                _debugger2 = value;
                RaisePropertyChanged("Debugger2");
            }
        }

        private ICommand _youtubeMode;
        public ICommand YoutubeMode
        {
            get
            {
                return _youtubeMode;
            }
            set
            {
                _youtubeMode = value;
                RaisePropertyChanged("YoutubeMode");
            }
        }

        private string _youtubeVisibility;
        public string YoutubeVisibility
        {
            get
            {
                return _youtubeVisibility;
            }
            set
            {
                _youtubeVisibility = value;
                RaisePropertyChanged("YoutubeVisibility");
            }
        }

        private ChromiumWebBrowser _youtube;
        public ChromiumWebBrowser Youtube
        {
            get
            {
                return _youtube;
            }
            set
            {
                _youtube = value;
                RaisePropertyChanged("Youtube");
            }
        }

        private string _link;
        public string Link
        {
            get
            {
                return _link;
            }
            set
            {
                _link = value;
                RaisePropertyChanged("Link");
                RaisePropertyChanged("Url");
            }
        }
        
        public string Url
        {
            get
            {
                return GetEmbedURL(Link);
            }
        }

        private string _browserAddress;
        public string BrowserAddress
        {
            get
            {
                return _browserAddress;
            }
            set
            {
                _browserAddress = value;
                Youtube.Address = value;
                RaisePropertyChanged("BrowserAddress");
            }
        }

        private string _errorVisibility;
        public string ErrorVisibility
        {
            get
            {
                return _errorVisibility;
            }
            set
            {
                _errorVisibility = value;
                RaisePropertyChanged("ErrorVisibility");
            }
        }

        private ICommand _validateLink;
        public ICommand ValidateLink
        {
            get
            {
                return _validateLink;
            }
            set
            {
                _validateLink = value;
                RaisePropertyChanged("ValidateLink");
            }
        }

        public CefSettings browserSettings { get; set; }

        private bool _isInYouTubeMode;
        public bool IsInYouTubeMode
        {
            get
            {
                return _isInYouTubeMode;
            }
            set
            {
                _isInYouTubeMode = value;
                RaisePropertyChanged("IsInYouTubeMode");
            }
        }

        private string _mediaVisibility;
        public string MediaVisibility
        {
            get
            {
                return _mediaVisibility;
            }
            set
            {
                _mediaVisibility = value;
                RaisePropertyChanged("MediaVisibility");
            }
        }

        private ICommand _download;
        public ICommand Download
        {
            get
            {
                return _download;
            }
            set
            {
                _download = value;
                RaisePropertyChanged("Download");
            }
        }

        private string _instruction;
        public string Instruction
        {
            get
            {
                return _instruction;
            }
            set
            {
                _instruction = value;
                RaisePropertyChanged("Instruction");
            }
        }

        public string Title { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel(IDataService dataService)
        {
            mediaPlayer = new MediaElement();
            mediaPlayer.LoadedBehavior = MediaState.Manual;
            MainInstruction = "00:00 / 00:00";
            initializeVariables();
            initializeRelayCommand();
            populateCollection();
        }

        /// <summary>
        /// Initialization.
        /// </summary>
        /// 

        private void initializeVariables()
        {
            SpeakerNormal = "/Resources/speakerIcon.jpg";
            SpeakerHover = "/Resources/speakerIconHover.jpg";
            SpeakerPressed = "/Resources/speakerIconPressed.jpg";
            PlayNormal = "/Resources/playIcon.jpg";
            PlayHover = "/Resources/playIconHover.jpg";
            PlayPressed = "/Resources/playIconPressed.jpg";
            IsPlaying = false;
            IsMediaAvailable = false;
            IsFullScreen = false;
            isDragged = false;
            WindowStyle0 = WindowStyle.SingleBorderWindow;
            WindowState0 = WindowState.Normal;
            FullScreenNormal = "/Resources/fullScreen.png";
            FullScreenHover = "/Resources/fullScreenHover.png";
            FullScreenPressed = "/Resources/fullScreenPressed.png";
            MediaSpan = 2;
            ShowPlaylist = "Collapsed";
            MediaColumn = 0;
            ListOfMedia = new ObservableCollection<MediaSingleElement>();
            IsAddMediaEnabled = true;
            jsonCommunicator = new JSONCommunicator();
            ShowSpeedOptions = "Collapsed";
            ListOfSpeeds = new ObservableCollection<double>();
            IsSpeedOptionsShown = false;
            ChosenSpeed = 1.0;
            TopPanelVisibility = 1.0;
            IsInYouTubeMode = false;
            MediaVisibility = "Visible";
            Instruction = "";
            Title = "";

            // Youtube
            Link = "";
            Youtube = new ChromiumWebBrowser();
            browserSettings = new CefSettings();
            if (!Cef.IsInitialized)
            {
                Cef.Initialize(browserSettings);
            }
            ErrorVisibility = "Hidden";
            YoutubeVisibility = "Hidden";
            // For debugging
            Debugger = 0;
            Debugger2 = 0;
        }

        private void initializeRelayCommand()
        {
            FindAudio = new RelayCommand(findAudio_Click);
            Play = new RelayCommand(play_Click);
            Stop = new RelayCommand(stop_Click);
            DragCompletedCommand = new RelayCommand(dragCompleted);
            Mute = new RelayCommand(muteMethod);
            FullScreen = new RelayCommand(fullScreenMethod);
            MoveForwards = new RelayCommand(moveForwardsMethod);
            MoveBackwards = new RelayCommand(moveBackwardsMethod);
            Playlist = new RelayCommand(playlistMethod);
            AddMedia = new RelayCommand(addMediaMethod);
            RemoveMedia = new RelayCommand(removeMediaMethod);
            PlayNewMedia = new RelayCommand(playNewMediaMethod);
            SetSpeed = new RelayCommand(setSpeedMethod);
            ShadowEffectFadeIn = new RelayCommand(shadowEffectFadeInMethod);
            ShadowEffectFadeOut = new RelayCommand(shadowEffectFadeOutMethod);
            YoutubeMode = new RelayCommand(youtubeModeMethod);
            ValidateLink = new RelayCommand(validateLinkMethod);
            Download = new RelayCommand(downloadMethod);
        }

        private void populateCollection()
        {
            ListOfSpeeds.Add(0.25);
            ListOfSpeeds.Add(0.5);
            ListOfSpeeds.Add(0.75);
            ListOfSpeeds.Add(1);
            ListOfSpeeds.Add(1.25);
            ListOfSpeeds.Add(1.5);
            ListOfSpeeds.Add(1.75);
            ListOfSpeeds.Add(2);
        }

        /// <summary>
        /// Helper functions.
        /// </summary>
        /// 

        private void findAudio_Click(object sender)
        {
            TurnOffUnFocused();

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio files (*.mp3 *.wav *.flac *.ogg)|*.mp3; *.wav; *.flac; *.ogg|Video files (*.mp4 *.avi *.flv *.wmv *.mov)|*.mp4; *.avi; *.flv; *.wmv; *.mov|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                mediaPlayer.Source = new Uri(openFileDialog.FileName);
                playingStatus();
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if ((mediaPlayer.Source != null) && (mediaPlayer.NaturalDuration.HasTimeSpan) && (!isDragged))
            {
                MainInstruction = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
                MaximumMediaLength = mediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                MediaProgress = mediaPlayer.Position.TotalSeconds;
            }
            else if ((isDragged) && (mediaPlayer.NaturalDuration.HasTimeSpan))
            {
                MainInstruction = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
            }
            else
            {
                MainInstruction = "00:00 / 00:00";
            }
        }

        private void play_Click(object sender)
        {
            if (IsMediaAvailable)
            {
                if (!IsPlaying)
                {
                    IsPlaying = true;
                    mediaPlayer.Play();
                    PlayNormal = "/Resources/pauseIcon.jpg";
                    PlayHover = "/Resources/pauseIconHover.jpg";
                    PlayPressed = "/Resources/pauseIconPressed.jpg";
                }
                else
                {
                    IsPlaying = false;
                    mediaPlayer.Pause();
                    PlayNormal = "/Resources/playIcon.jpg";
                    PlayHover = "/Resources/playIconHover.jpg";
                    PlayPressed = "/Resources/playIconPressed.jpg";
                }
            }
        }

        private void stop_Click(object sender)
        {
            mediaPlayer.Stop();
            MediaProgress = 0;
        }

        private void dragCompleted(object sender)
        {
            isDragged = true;
            mediaPlayer.Position = TimeSpan.FromSeconds(MediaProgress);
        }

        private void muteMethod(object sender)
        {
            if (Volume != 0)
            {
                temporaryVolume = Volume;
            }
            IsMute = !IsMute;
            if (IsMute)
            {
                Volume = 0;
                SpeakerNormal = "/Resources/muteIcon.jpg";
                SpeakerHover = "/Resources/muteIconHover.jpg";
                SpeakerPressed = "/Resources/muteIconPressed.jpg";
            }
            else
            {
                Volume = temporaryVolume;
                SpeakerNormal = "/Resources/speakerIcon.jpg";
                SpeakerHover = "/Resources/speakerIconHover.jpg";
                SpeakerPressed = "/Resources/speakerIconPressed.jpg";
            }
        }

        private void fullScreenMethod(object sender)
        {
            TurnOffUnFocused();
            if (!IsFullScreen)
            {
                WindowStyle0 = WindowStyle.None;
                WindowState0 = WindowState.Maximized;

                FullScreenNormal = "/Resources/minimizeIcon.png";
                FullScreenHover = "/Resources/minimizeIconHover.png";
                FullScreenPressed = "/Resources/minimizeIconPressed.png";

                TopPanelVisibility = 1.0;
            }
            else
            {
                WindowStyle0 = WindowStyle.SingleBorderWindow;
                WindowState0 = WindowState.Normal;

                FullScreenNormal = "/Resources/fullScreen.png";
                FullScreenHover = "/Resources/fullScreenHover.png";
                FullScreenPressed = "/Resources/fullScreenPressed.png";

                TopPanelVisibility = 1.0;
            }
            IsFullScreen = !IsFullScreen;
        }

        private void moveForwardsMethod(object sender)
        {
            mediaPlayer.Position = mediaPlayer.Position + TimeSpan.FromSeconds(10);
            MainInstruction = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
        }

        private void moveBackwardsMethod(object sender)
        {
            mediaPlayer.Position = mediaPlayer.Position - TimeSpan.FromSeconds(10);
            MainInstruction = String.Format("{0} / {1}", mediaPlayer.Position.ToString(@"mm\:ss"), mediaPlayer.NaturalDuration.TimeSpan.ToString(@"mm\:ss"));
        }

        /// <summary>
        /// Show the favorite playlist
        /// </summary>
        /// <param name="sender"></param>
        private void playlistMethod(object sender)
        {
            TurnOffUnFocused();
            if (ShowPlaylist.Equals("Collapsed"))
            {
                ShowPlaylist = "Visible";

                getPlaylist();

                if (ListOfMedia.Count >= 1)
                {
                    IsRemoveMediaEnabled = true;
                    foreach (MediaSingleElement mse in ListOfMedia) {
                        getIconImage(mse, mse.Extension);
                    }
                }
                else
                {
                    IsRemoveMediaEnabled = false;
                }
                MediaSpan = 1;
                MediaColumn = 1;
            }
            else
            {
                ShowPlaylist = "Collapsed";
                MediaSpan = 2;
                MediaColumn = 0;
            }
        }

        /// <summary>
        /// Add new media to the favorite playlist
        /// </summary>
        /// <param name="sender"></param>
        private void addMediaMethod(object sender)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Audio files (*.mp3 *.wav *.flac *.ogg)|*.mp3; *.wav; *.flac; *.ogg|Video files (*.mp4 *.avi *.flv *.wmv *.mov)|*.mp4; *.avi; *.flv; *.wmv; *.mov|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                IsRemoveMediaEnabled = true;
                MediaSingleElement selectedMedia = new MediaSingleElement();
                selectedMedia.Title = Path.GetFileNameWithoutExtension(openFileDialog.FileName);
                selectedMedia.mediaUri = openFileDialog.FileName;
                var ffProbe = new NReco.VideoInfo.FFProbe();
                var videoInfo = ffProbe.GetMediaInfo(openFileDialog.FileName);
                selectedMedia.MediaDuration = videoInfo.Duration.ToString(@"mm\:ss");
                ListOfMedia.Add(selectedMedia);

                selectedMedia.Extension = Path.GetExtension(openFileDialog.FileName);
                getIconImage(selectedMedia, selectedMedia.Extension);
            }
        }

        /// <summary>
        /// Remove the selected media on the favorite playlist
        /// </summary>
        /// <param name="sender"></param>
        private void removeMediaMethod(object sender)
        {
            if (ListOfMedia.Count == 1)
            {
                ListOfMedia.RemoveAt(0);
                IsRemoveMediaEnabled = false;
                SelectedIndex = 0;
            }
            else if (ListOfMedia.Count > 1)
            {
                if (SelectedIndex >= ListOfMedia.Count)
                {
                    SelectedIndex = ListOfMedia.Count - 1;
                }
                else if (SelectedIndex < 0)
                {
                    SelectedIndex = 0;
                }
                ListOfMedia.RemoveAt(SelectedIndex);
                IsRemoveMediaEnabled = true;
            }
            else
            {
                IsRemoveMediaEnabled = false;
            }
        }

        private void playNewMediaMethod(object sender)
        {
            mediaPlayer.Stop();
            mediaPlayer.LoadedBehavior = MediaState.Manual;
            this.mediaPlayer.Source = new Uri(ListOfMedia[SelectedIndex].mediaUri);
            playingStatus();
        }

        /// <summary>
        /// Steps to play the media
        /// </summary>
        private void playingStatus()
        {
            mediaPlayer.Play();
            IsPlaying = true;
            IsMediaAvailable = true;
            PlayNormal = "/Resources/pauseIcon.jpg";
            PlayHover = "/Resources/pauseIconHover.jpg";
            PlayPressed = "/Resources/pauseIconPressed.jpg";

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
        }
        public ICommand WindowClosing
        {
            get
            {
                return new RelayCommand<CancelEventArgs>(
                    (args) => {
                        saveListOfMedia();
                    });
            }
        }

        private void saveListOfMedia()
        {
            jsonCommunicator.SerializeObject<MediaSingleElement>(ListOfMedia, pathProcessing());
        }

        private Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                Bitmap bitmap = new Bitmap(outStream);

                return new Bitmap(bitmap);
            }
        }

        private void getIconImage(MediaSingleElement tmpmedia, string extension)
        {
            if (!tmpmedia.Extension.Equals(".mp3"))
            {
                var ffMpeg = new NReco.VideoConverter.FFMpegConverter();
                using (MemoryStream sampleStream = new MemoryStream())
                {
                    ffMpeg.GetVideoThumbnail(new Uri(tmpmedia.mediaUri).LocalPath, sampleStream, 2);
                    sampleStream.Seek(0, SeekOrigin.Begin);
                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.StreamSource = sampleStream;
                    bitmapImage.EndInit();
                    tmpmedia.IconImage = bitmapImage;
                }
            }
            else
            {
                tmpmedia.IconImage = new BitmapImage(new Uri("pack://application:,,,/XMediaPlayer;component/Icons/mp3.png"));
            }
        }

        private void setSpeedMethod(object sender)
        {
            if (!IsSpeedOptionsShown)
            {
                ShowSpeedOptions = "Visible";
            }
            else
            {
                ShowSpeedOptions = "Collapsed";
            }
            IsSpeedOptionsShown = !IsSpeedOptionsShown;
        }

        private void shadowEffectFadeInMethod(object sender)
        {
            if (IsFullScreen)
            {
                if (Timer2 != null)
                {
                    Timer2.Stop();
                }
                Timer3 = new DispatcherTimer();
                Timer3.Interval = System.TimeSpan.FromMilliseconds(5);
                Timer3.Tick += FadeIn;
                Timer3.Start();
            }
        }

        private void FadeIn(object sender, EventArgs e)
        {
            if (IsFullScreen)
            {
                DispatcherTimer currentTimer = (DispatcherTimer)sender;
                currentTimer.Start();
                
                if (TopPanelVisibility >= 1)
                {
                    currentTimer.Stop();
                }
                else
                {
                    TopPanelVisibility += 0.01;
                }
            }
        }

        private void shadowEffectFadeOutMethod(object sender)
        {
            if (IsFullScreen)
            {
                if (Timer3 != null)
                {
                    Timer3.Stop();
                }
                Timer2 = new DispatcherTimer();
                Timer2.Interval = System.TimeSpan.FromMilliseconds(5);
                Timer2.Tick += FadeOut;
                Timer2.Start();
            }
        }

        private void FadeOut(object sender, EventArgs e)
        {
            if (IsFullScreen)
            {
                DispatcherTimer currentTimer = (DispatcherTimer)sender;
                currentTimer.Start();
                
                if (TopPanelVisibility <= 0)
                {
                    currentTimer.Stop();
                }
                else
                {
                    TopPanelVisibility -= 0.01;
                }
            }
        }

        private void TurnOffUnFocused()
        {
            if (ShowSpeedOptions == "Visible")
            {
                setSpeedMethod(new object());
            }
        }

        private void getPlaylist()
        {
            ListOfMedia = jsonCommunicator.DeSerializeObject<MediaSingleElement>(pathProcessing());
        }

        private string pathProcessing()
        {
            string filePath;
#if (DEBUG)
            filePath = "..\\..\\..\\Resources\\MediaData.txt";
#else
            filePath = Directory.GetCurrentDirectory() + "\\data\\MediaData.txt";
            try
            {
                // create the folder if it doesn't exist
                System.IO.Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\data");
                // check if the file already exists
                if (!File.Exists(filePath))
                {
                    var data = Properties.Resources.MediaData;
                    byte[] dataInByte = new UTF8Encoding(true).GetBytes(data);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        stream.Write(dataInByte, 0, dataInByte.Count());
                        stream.Flush();
                    }
                }
            }
            catch (Exception)
            {

            }
#endif
            return filePath;
        }

        private void youtubeModeMethod(object sender)
        {
            Instruction = "";
            if (!IsInYouTubeMode)
            {
                YoutubeVisibility = "Visible";
                MediaVisibility = "Hidden";

            }
            else
            {
                YoutubeVisibility = "Hidden";
                MediaVisibility = "Visible";
            }
            IsInYouTubeMode = !IsInYouTubeMode;
        }

        private string GetEmbedURL(string link)
        {
            string embedUrl = link.Replace("watch?v=", "embed/");
            return embedUrl;
        }

        private void validateLinkMethod(object sender)
        {
            if (!Url.Contains("youtube.com"))
            {
                ErrorVisibility = "Hidden"; // => Trigger the blinking effect of ErrorStyle
                ErrorVisibility = "Visible";
                TurnOffUnFocused();
                if (ShowPlaylist.Equals("Visible"))
                {
                    ShowPlaylist = "Collapsed";
                }
            }
            else
            {
                ErrorVisibility = "Hidden";
                BrowserAddress = Url;
            }
        }

        private async void downloadMethod(object sender)
        {
            Instruction = "Downloading ...";
            var id = YoutubeClient.ParseVideoId(Link);
            var client = new YoutubeClient();
            var video = await client.GetVideoAsync(id);
            var streamInfoSet = await client.GetVideoMediaStreamInfosAsync(id);
            
            var streamInfo = streamInfoSet.Muxed.WithHighestVideoQuality();
            //var ext = streamInfo.Container.GetFileExtension();
            Title = video.Title.Replace("/", "").Replace("\\", "");
            string videoPath = "D:\\" + Title + ".mp4";
            string audioPath = "D:\\" + Title + ".mp3";
            string outputFilePath = Title + ".avi";

            await client.DownloadMediaStreamAsync(streamInfo, videoPath);

            var streamInfo2 = streamInfoSet.Audio.WithHighestBitrate();
            await client.DownloadMediaStreamAsync(streamInfo2, audioPath);

            // Not working ffmpeg due to lack of version support => Can't yet to merge video and audio together
            //var ffmpeg = new FFMpegConverter();
            //var ffmpegPath = ffmpeg.FFMpegToolPath + ffmpeg.FFMpegExeName;
            //Process.Start(new ProcessStartInfo
            //{
            //    FileName = ffmpegPath,
            //    Arguments = $"-i {videoPath} -i {audioPath} -c:v copy -c:a aac -strict experimental {outputFilePath}"
            //}).WaitForExit();

            Instruction = "Done!";
        }

        ////public override void Cleanup()
        ////{
        ////    // Clean up if needed

        ////    base.Cleanup();
        ////}
    }
}