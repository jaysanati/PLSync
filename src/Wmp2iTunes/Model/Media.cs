namespace Wmp2iTunes
{
    public interface IMediaPlayer
    {

    }

    public interface IMediaLibrary: IDisposable
    {
        IMediaPlayer Player { get; set; }
        List<IPlaylist> Playlists { get; }
        void Load();
        Task LoadAsync();
        IPlaylist CreatePlaylist(string name, bool ignoreIfExists);
        bool DeletePlaylist(string name);
        void AddFile(string filename, string playlist);
        void ClearPlayList(string playlist);
    }

    public interface IPlaylist
    {
        object _Ref { get; }
        bool Auto { get; set; }
        string Name { get; }
        List<IMediaTrack> Tracks { get; }
        void LoadTracks();
    }

    public interface IMediaTrack
    {
         object _Ref { get; }
         string Location { get; }
    }

    public class GenericMediaTrack : IMediaTrack
    {
        public GenericMediaTrack(string location, object _ref)
        {
            Location = location;
            _Ref = _ref;
        }
        public string Location { get; }

        public object _Ref { get; protected set; }
    }

    public class GenericPlaylist : IPlaylist
    {
        public GenericPlaylist(string name, object _ref)
        {
            Name = name;
            Tracks = new List<IMediaTrack>();
            _Ref = _ref;
        }

        public bool Auto { get; set; }
        public string Name { get; }
        public List<IMediaTrack> Tracks { get; }

        public object _Ref { get; }

        public virtual void LoadTracks()
        {
            throw new NotImplementedException();
        }
    }
}