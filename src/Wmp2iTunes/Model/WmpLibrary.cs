using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace Wmp2iTunes
{
    public class WmpPlaylist : GenericPlaylist
    {
        public WmpPlaylist(string name, object obj) : base(name, obj)
        {
        }

        public override void LoadTracks()
        {
            IWMPPlaylist pl = (IWMPPlaylist)_Ref;
            for (int f = 0; f < pl.count; f++)
            {
                Tracks.Add(new GenericMediaTrack(pl.Item[f].sourceURL, pl.Item[f]));
            }
        }
    }

    public class WmpLibrary : IMediaLibrary
    {
        private WindowsMediaPlayer? App;
        public List<IPlaylist> Playlists { get; private set; }
        public IMediaPlayer Player { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public WmpLibrary()
        {
            Playlists = new List<IPlaylist>();
        }

        private IPlaylist? FetchPlayList(IWMPPlaylist pl, bool loadTracks)
        {
            string[] internalPLs = {"All Music", "All Pictures", "All Video", "Music added in the last month", "Music auto rated at 5 stars", "Music played in the last month"
                , "Music played the most", "Music rated at 4 or 5 stars", "Pictures rated 4 or 5 stars", "Pictures taken in the last month", "TV recorded in the last week", "Video rated at 4 or 5 stars" };

            if (internalPLs.Contains(pl.name))
                return null;

            var result = new WmpPlaylist(pl.name, pl);

            for (int at = 0; at < pl.attributeCount; at++)
            {
                //string s = pl.attributeName[at] + " > " + pl.getItemInfo(pl.attributeName[at]);
                if (pl.attributeName[at] == "PlaylistType")
                {
                    result.Auto = pl.getItemInfo(pl.attributeName[at]) == "Auto";
                    break;
                }
            }

            if (loadTracks)
            {
                for (int f = 0; f < pl.count; f++)
                {
                    result.Tracks.Add(new GenericMediaTrack(pl.Item[f].sourceURL, pl.Item[f]));
                }
            }

            return result;
        }

        public void Load()
        {
            if (App == null)
                App = new WindowsMediaPlayer();

            Playlists.Clear();
            var pList = App.playlistCollection.getAll();
            for (int i = 0; i < pList.count; i++)
            {
                IWMPPlaylist p = pList.Item(i);
                IPlaylist? pl = FetchPlayList(p, false);
                if (pl != null)
                {
                    Playlists.Add(pl);
                }
            }
        }

        public async Task LoadAsync()
        {
            var task = new Task(() =>
            {
                if (App == null)
                    App = new WindowsMediaPlayer();

                Playlists.Clear();
                var pList = App.playlistCollection.getAll();
                for (int i = 0; i < pList.count; i++)
                {
                    IWMPPlaylist p = pList.Item(i);
                    IPlaylist? pl = FetchPlayList(p, false);
                    if (pl != null)
                    {
                        Playlists.Add(pl);
                    }
                }
            });
            task.Start();
            await task;
        }

        public void Dispose()
        {
            if (App != null)
                Marshal.ReleaseComObject(App);
        }

        public IPlaylist CreatePlaylist(string name, bool ignoreIfExists)
        {
            throw new NotImplementedException();
        }

        public bool DeletePlaylist(string name)
        {
            throw new NotImplementedException();
        }

        public void AddFile(string filename, string playlist)
        {
            throw new NotImplementedException();
        }

        public void ClearPlayList(string playlist)
        {
            throw new NotImplementedException();
        }
    }
}
