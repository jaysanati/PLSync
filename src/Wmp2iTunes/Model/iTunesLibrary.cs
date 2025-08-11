using iTunesLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Wmp2iTunes.Model
{
    public class iTunesPlaylist : GenericPlaylist
    {
        public iTunesPlaylist(string name, object obj) : base(name, obj)
        {
        }

        public override void LoadTracks()
        {
            IITUserPlaylist pl = (IITUserPlaylist)_Ref;
            IITTrackCollection tracks = pl.Tracks;
            foreach (IITTrack track in tracks)
            {
                if (track.Kind == ITTrackKind.ITTrackKindFile)
                {
                    IITFileOrCDTrack fileTrack = (IITFileOrCDTrack)track;

                    if (!string.IsNullOrEmpty(fileTrack.Location))
                    {
                        Tracks.Add(new GenericMediaTrack(fileTrack.Location, fileTrack));
                    }
                }
            }
        }
    }

    public class iTunesLibrary : IMediaLibrary
    {
        private iTunesAppClass? App;
        public List<IPlaylist> Playlists { get; private set; }
        public IMediaPlayer Player { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public iTunesLibrary()
        {
            Playlists = new List<IPlaylist>();
        }

        private IPlaylist FetchPlayList(IITPlaylist pl, bool loadTracks)
        {
            var result = new iTunesPlaylist(pl.Name, pl);

            if (loadTracks)
            {
                IITTrackCollection tracks = pl.Tracks;
                foreach (IITTrack track in tracks)
                {
                    if (track.Kind == ITTrackKind.ITTrackKindFile)
                    {
                        IITFileOrCDTrack fileTrack = (IITFileOrCDTrack)track;

                        if (!string.IsNullOrEmpty(fileTrack.Location))
                        {
                            result.Tracks.Add(new GenericMediaTrack(fileTrack.Location, fileTrack));
                        }
                    }
                }
            }

            return result;
        }

        public void Load()
        {
            IITSourceCollection sources = GetApp().Sources;

            foreach (IITSource src in sources)
            {
                if (src.Name == "Library")
                {
                    IITPlaylistCollection pls = src.Playlists;
                    foreach (IITPlaylist p in pls)
                    {
                        if (p is IITUserPlaylist)
                        {
                            var upl = (IITUserPlaylist)p;
                            if (upl.SpecialKind != ITUserPlaylistSpecialKind.ITUserPlaylistSpecialKindNone)
                                continue;
                        }

                        if (p.Kind == ITPlaylistKind.ITPlaylistKindLibrary)
                            continue;

                        IPlaylist? pl = FetchPlayList(p, false);
                        if (pl != null)
                        {
                            Playlists.Add(pl);
                        }
                    }
                }
            }
        }

        public async Task LoadAsync()
        {
            var task = new Task(() =>
            {
                IITSourceCollection sources = GetApp().Sources;

                foreach (IITSource src in sources)
                {
                    if (src.Name == "Library")
                    {
                        IITPlaylistCollection pls = src.Playlists;
                        foreach (IITPlaylist p in pls)
                        {
                            if (p is IITUserPlaylist)
                            {
                                var upl = (IITUserPlaylist)p;
                                if (upl.SpecialKind != ITUserPlaylistSpecialKind.ITUserPlaylistSpecialKindNone)
                                    continue;
                            }

                            if (p.Kind == ITPlaylistKind.ITPlaylistKindLibrary)
                                continue;

                            IPlaylist? pl = FetchPlayList(p, false);
                            if (pl != null)
                            {
                                Playlists.Add(pl);
                            }
                        }
                    }
                }
            });
            task.Start();
            await task;
        }

        private iTunesAppClass GetApp()
        {
            if (App == null)
            {
                App = new iTunesAppClass();
                //App.BrowserWindow.Minimized = true;
            }
            return App;
        }

        public void Dispose()
        {
            if (App != null)
            {
                App.Quit();
                Marshal.ReleaseComObject(App);
            }
        }

        public IPlaylist CreatePlaylist(string name, bool ignoreIfExists)
        {
            IITPlaylistCollection playlists = GetApp().Sources.ItemByName["Library"].Playlists;
            IITPlaylist pl = playlists.ItemByName[name];
            if (pl != null && ignoreIfExists)
                return FetchPlayList(pl, false);

            pl = (IITUserPlaylist)App.CreatePlaylist(name);
            return FetchPlayList(pl, false);
        }

        public bool DeletePlaylist(string name)
        {
            IITPlaylistCollection playlists = GetApp().Sources.ItemByName["Library"].Playlists;
            IITPlaylist pl = playlists.ItemByName[name];
            if (pl != null)
            {
                pl.Delete();
                return true;
            }

            return false;
        }

        public void AddFile(string filename, string playlist)
        {
            IITPlaylistCollection playlists = GetApp().Sources.ItemByName["Library"].Playlists;
            IITPlaylist pl = playlists.ItemByName[playlist];
            if (pl != null && (pl is IITUserPlaylist))
            {
                (pl as IITUserPlaylist).AddFile(filename);
            }
        }

        public void ClearPlayList(string playlist)
        {
            IITPlaylistCollection playlists = GetApp().Sources.ItemByName["Library"].Playlists;
            IITPlaylist pl = playlists.ItemByName[playlist];

            foreach (IITTrack track in pl.Tracks)
            {
                IITFileOrCDTrack trk = (IITFileOrCDTrack)track;
                trk.Delete();
            }
        }
    }
}
