# X-Media-Player
A customized media player with eye-catching design that can perform basic operations of a typical window media player and some extra functionalities.
## Design:
<img src="https://github.com/minhducubc97/X-Media-Player/blob/master/Design/InitialView.PNG" height="250"/>

*Figure 1: X Media Player initial view when opened.*
<br/><br/>

<img src="https://github.com/minhducubc97/X-Media-Player/blob/master/Design/InAction.PNG" height="250"/>

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;*Figure 2: X Media Player playing a movie.*
<br/><br/>

<img src="https://github.com/minhducubc97/X-Media-Player/blob/master/Design/Playlist.PNG" height="250"/>

*Figure 3: X Media Player operating with playlist.*
<br/><br/>

## Functionalities:

### 1. Basic:

X Media Player can perform basic operation of a Window Media Player:
- Play/Pause media files (both audio and video types)
- Adjust volume
- Control over playing process (can drag to a prefered time of the media duration)
- Fast forwards/backwards
- Explore local media files
- Create playlist for user
- Set speed to play the media
- Go fullscreen

### 2. Extra:

X Media Player's extra feature is its ability to work with Youtube videos. After received the Youtube link typed in by user, the player will be able to:
- Play the specified Youtube video inside the Media Player.
- Download the video directly from Youtube.

<img src="https://github.com/minhducubc97/X-Media-Player/blob/master/Design/StreamYoutube.PNG" height="250"/>

*Figure 4: X Media Player streaming Youtube video.*

## Specifications:

The project is developed in WPF with MVVM Light Framework, .NET Framework 4.6.1.
The libraries used in this project:
- [AngleSharp](https://anglesharp.github.io/)
- [CefSharp](https://cefsharp.github.io/)
- [MVVMLightLibs](http://www.mvvmlight.net/)
- [NReco](https://www.nrecosite.com/)
- [YoutubeExplode](https://www.tyrrrz.me/Projects/YoutubeExplode)
