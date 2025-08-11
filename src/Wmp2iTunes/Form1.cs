using Wmp2iTunes.Model;

namespace Wmp2iTunes
{
    public partial class MainForm : Form
    {
        IMediaLibrary srcLib;
        IMediaLibrary trgLib;

        public MainForm()
        {
            InitializeComponent();
        }

        private void LoadTree(IMediaLibrary library, TreeView tvList)
        {
            tvList.Nodes.Clear();
            foreach (var pl in library.Playlists)
            {
                TreeNode node = tvList.Nodes.Add(pl.Name);
                node.Tag = pl;
                foreach (var trk in pl.Tracks)
                {
                    var tn = node.Nodes.Add(trk.Location);
                    tn.Tag = trk;
                }
            }
        }

        private void LoadNode(IPlaylist playlist, TreeNode node)
        {
            node.Nodes.Clear();
            foreach (var trk in playlist.Tracks)
            {
                var tn = node.Nodes.Add(trk.Location);
                tn.Tag = trk;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbSource.SelectedIndex = 0;
            cbTarget.SelectedIndex = 0;
        }

        private async void cbSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbSource.SelectedIndex == 0)
            {
                if (srcLib != null)
                    srcLib.Dispose();
                tvSource.Nodes.Clear();
            }
            if (cbSource.SelectedIndex == 1)
            {
                srcLib = new WmpLibrary();
                await srcLib.LoadAsync();
                LoadTree(srcLib, tvSource);
            }
        }

        private async void cbTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbTarget.SelectedIndex == 0)
            {
                if (trgLib != null)
                    trgLib.Dispose();
                tvTarget.Nodes.Clear();
            }
            if (cbTarget.SelectedIndex == 1)
            {
                trgLib = new iTunesLibrary();
                await trgLib.LoadAsync();
                LoadTree(trgLib, tvTarget);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            foreach (TreeNode node in tvSource.Nodes)
            {
                if (node.Checked)
                {
                    IPlaylist? srcPl = srcLib.Playlists.Where(x => x.Name == node.Text).FirstOrDefault();
                    if (srcPl != null)
                    {
                        IPlaylist? trgPl = trgLib.Playlists.SingleOrDefault(x => x.Name.ToUpper() == srcPl.Name.ToUpper());
                        if (trgPl != null)
                        {
                            if (cbxReCreateTarget.Checked)
                                trgLib.DeletePlaylist(trgPl.Name);
                            else
                                trgLib.ClearPlayList(trgPl.Name);
                        }
                        trgPl = trgLib.CreatePlaylist(node.Text, !cbxReCreateTarget.Checked);

                        foreach (var track in srcPl.Tracks)
                        {
                            trgLib.AddFile(track.Location, trgPl.Name);
                        }

                    }

                }
            }
        }

        private void tvSource_AfterCheck(object sender, TreeViewEventArgs e)
        {
            IPlaylist pl = e.Node.Tag is IPlaylist ? (IPlaylist)e.Node.Tag : null;
            if (pl != null)
            {
                if (e.Node.Checked)
                {
                    pl.LoadTracks();
                }
                else
                    pl.Tracks.Clear();
                LoadNode(pl, e.Node);
            }
        }

        private void tvTarget_AfterCheck(object sender, TreeViewEventArgs e)
        {
            IPlaylist pl = e.Node.Tag is IPlaylist ? e.Node.Tag as IPlaylist : null;
            if (pl != null)
            {
                if (e.Node.Checked)
                {
                    pl.LoadTracks();
                }
                else
                    pl.Tracks.Clear();
                LoadNode(pl, e.Node);
            }
        }
    }
}