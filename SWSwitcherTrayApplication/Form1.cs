using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SWSwitcherTrayApp
{
    public partial class SWSwitcherTrayApp : Form
    {
        SWSwitcher sws;
        public SWSwitcherTrayApp()
        {
            InitializeComponent();
            contextMenuStrip1.Items.Insert(0, new ToolStripLabel(this.notifyIcon1.Text) { Font = new Font(DefaultFont, FontStyle.Bold) });
            contextMenuStrip1.Items.Insert(1, new ToolStripSeparator());

            this.sws = new SWSwitcher();

            switch (this.sws.TimeInterval/(60000))
            {
                case 5:
                    this.minToolStripMenuItem.Checked = true;
                    break;
                case 15:
                    this.minToolStripMenuItem1.Checked = true;
                    break;
                case 30:
                    this.minToolStripMenuItem2.Checked = true;
                    break;
                case 60:
                    this.minToolStripMenuItem3.Checked = true;
                    break;
                case 0:
                    this.neverToolStripMenuItem.Checked = true;
                    break;
            }

            switch (this.sws.CurStyle)
            {
                case SWSwitcher.Style.Stretched:
                    this.stretchToolStripMenuItem.Checked = true;
                    break;
                case SWSwitcher.Style.Filled:
                    this.fillToolStripMenuItem.Checked = true;
                    break;
                case SWSwitcher.Style.Centered:
                    this.centerToolStripMenuItem.Checked = true;
                    break;
                case SWSwitcher.Style.Tiled:
                    this.tileToolStripMenuItem.Checked = true;
                    break;
            }

            switch (this.sws.CurOrder)
            {
                case SWSwitcher.Order.Sequential:
                    this.sequentialToolStripMenuItem.Checked = true;
                    break;
                case SWSwitcher.Order.Random:
                    this.randomToolStripMenuItem.Checked = true;
                    break;
            }
        }

        protected override void SetVisibleCore(bool value)
        {
            if (!IsHandleCreated && value)
            {
                base.CreateHandle();
                value = false;
            }
            base.SetVisibleCore(value);
        }

        private NotifyIcon notifyIcon1;
        private IContainer components;
        private ContextMenuStrip contextMenuStrip1;
        private ToolStripMenuItem exitToolStripMenuItem;

        private void InitializeComponent()
        {
      this.components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SWSwitcherTrayApp));
      this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
      this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.chooseFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.interval1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.minToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.minToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
      this.minToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
      this.minToolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
      this.neverToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.orderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.sequentialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.randomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.styleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.stretchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.centerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.tileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.fillToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.resetWallpaperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.restartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.contextMenuStrip1.SuspendLayout();
      this.SuspendLayout();
      // 
      // notifyIcon1
      // 
      this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
      #if DEBUG
      this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon2.Icon")));
      #else 
      this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
      #endif
      this.notifyIcon1.Text = "SWSwitcher " + this.ProductVersion.ToString();
      this.notifyIcon1.Visible = true;
      this.notifyIcon1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseDoubleClick_1);
      // 
      // contextMenuStrip1
      // 
      this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.chooseFolderToolStripMenuItem,
            this.interval1ToolStripMenuItem,
            this.orderToolStripMenuItem,
            this.styleToolStripMenuItem,
            this.resetWallpaperToolStripMenuItem,
            this.restartToolStripMenuItem,
            this.exitToolStripMenuItem});
      this.contextMenuStrip1.Name = "contextMenuStrip1";
      this.contextMenuStrip1.Size = new System.Drawing.Size(158, 158);
      // 
      // chooseFolderToolStripMenuItem
      // 
      this.chooseFolderToolStripMenuItem.Name = "chooseFolderToolStripMenuItem";
      this.chooseFolderToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
      this.chooseFolderToolStripMenuItem.Text = "Choose folder...";
      this.chooseFolderToolStripMenuItem.Click += new System.EventHandler(this.chooseFolderToolStripMenuItem_Click);
      // 
      // interval1ToolStripMenuItem
      // 
      this.interval1ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minToolStripMenuItem,
            this.minToolStripMenuItem1,
            this.minToolStripMenuItem2,
            this.minToolStripMenuItem3,
            this.neverToolStripMenuItem});
      this.interval1ToolStripMenuItem.Name = "interval1ToolStripMenuItem";
      this.interval1ToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
      this.interval1ToolStripMenuItem.Text = "Interval";
      // 
      // minToolStripMenuItem
      // 
      this.minToolStripMenuItem.Name = "minToolStripMenuItem";
      this.minToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
      this.minToolStripMenuItem.Text = "5 min";
      this.minToolStripMenuItem.Click += new System.EventHandler(this.minToolStripMenuItem_Click);
      // 
      // minToolStripMenuItem1
      // 
      this.minToolStripMenuItem1.Name = "minToolStripMenuItem1";
      this.minToolStripMenuItem1.Size = new System.Drawing.Size(110, 22);
      this.minToolStripMenuItem1.Text = "15 min";
      this.minToolStripMenuItem1.Click += new System.EventHandler(this.minToolStripMenuItem1_Click);
      // 
      // minToolStripMenuItem2
      // 
      this.minToolStripMenuItem2.Name = "minToolStripMenuItem2";
      this.minToolStripMenuItem2.Size = new System.Drawing.Size(110, 22);
      this.minToolStripMenuItem2.Text = "30 min";
      this.minToolStripMenuItem2.Click += new System.EventHandler(this.minToolStripMenuItem2_Click);
      // 
      // minToolStripMenuItem3
      // 
      this.minToolStripMenuItem3.Name = "minToolStripMenuItem3";
      this.minToolStripMenuItem3.Size = new System.Drawing.Size(110, 22);
      this.minToolStripMenuItem3.Text = "60 min";
      this.minToolStripMenuItem3.Click += new System.EventHandler(this.minToolStripMenuItem3_Click);
      // 
      // neverToolStripMenuItem
      // 
      this.neverToolStripMenuItem.Name = "neverToolStripMenuItem";
      this.neverToolStripMenuItem.Size = new System.Drawing.Size(110, 22);
      this.neverToolStripMenuItem.Text = "Never";
      this.neverToolStripMenuItem.Click += new System.EventHandler(this.neverToolStripMenuItem_Click);
      // 
      // orderToolStripMenuItem
      // 
      this.orderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sequentialToolStripMenuItem,
            this.randomToolStripMenuItem});
      this.orderToolStripMenuItem.Name = "orderToolStripMenuItem";
      this.orderToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
      this.orderToolStripMenuItem.Text = "Order";
      // 
      // sequentialToolStripMenuItem
      // 
      this.sequentialToolStripMenuItem.Name = "sequentialToolStripMenuItem";
      this.sequentialToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
      this.sequentialToolStripMenuItem.Text = "Sequential";
      this.sequentialToolStripMenuItem.Click += new System.EventHandler(this.sequentialToolStripMenuItem_Click);
      // 
      // randomToolStripMenuItem
      // 
      this.randomToolStripMenuItem.Name = "randomToolStripMenuItem";
      this.randomToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
      this.randomToolStripMenuItem.Text = "Random";
      this.randomToolStripMenuItem.Click += new System.EventHandler(this.randomToolStripMenuItem_Click);
      // 
      // styleToolStripMenuItem
      // 
      this.styleToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.stretchToolStripMenuItem,
            this.centerToolStripMenuItem,
            this.tileToolStripMenuItem,
            this.fillToolStripMenuItem});
      this.styleToolStripMenuItem.Name = "styleToolStripMenuItem";
      this.styleToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
      this.styleToolStripMenuItem.Text = "Style";
      // 
      // stretchToolStripMenuItem
      // 
      this.stretchToolStripMenuItem.Name = "stretchToolStripMenuItem";
      this.stretchToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
      this.stretchToolStripMenuItem.Text = "Stretch";
      this.stretchToolStripMenuItem.Click += new System.EventHandler(this.stretchToolStripMenuItem_Click);
      // 
      // centerToolStripMenuItem
      // 
      this.centerToolStripMenuItem.Name = "centerToolStripMenuItem";
      this.centerToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
      this.centerToolStripMenuItem.Text = "Center";
      this.centerToolStripMenuItem.Click += new System.EventHandler(this.centerToolStripMenuItem_Click);
      // 
      // tileToolStripMenuItem
      // 
      this.tileToolStripMenuItem.Name = "tileToolStripMenuItem";
      this.tileToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
      this.tileToolStripMenuItem.Text = "Tile";
      this.tileToolStripMenuItem.Click += new System.EventHandler(this.tileToolStripMenuItem_Click);
      // 
      // fillToolStripMenuItem
      // 
      this.fillToolStripMenuItem.Name = "fillToolStripMenuItem";
      this.fillToolStripMenuItem.Size = new System.Drawing.Size(111, 22);
      this.fillToolStripMenuItem.Text = "Fill";
      this.fillToolStripMenuItem.Click += new System.EventHandler(this.fillToolStripMenuItem_Click);
      // 
      // resetWallpaperToolStripMenuItem
      // 
      this.resetWallpaperToolStripMenuItem.Name = "resetWallpaperToolStripMenuItem";
      this.resetWallpaperToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
      this.resetWallpaperToolStripMenuItem.Text = "Reset Default";
      this.resetWallpaperToolStripMenuItem.Click += new System.EventHandler(this.resetWallpaperToolStripMenuItem_Click);
      // 
      // restartToolStripMenuItem
      // 
      this.restartToolStripMenuItem.Name = "restartToolStripMenuItem";
      this.restartToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
      this.restartToolStripMenuItem.Text = "Restart";
      this.restartToolStripMenuItem.Click += new System.EventHandler(this.restartToolStripMenuItem_Click);
      // 
      // exitToolStripMenuItem
      // 
      this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
      this.exitToolStripMenuItem.Size = new System.Drawing.Size(157, 22);
      this.exitToolStripMenuItem.Text = "Exit";
      this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
      // 
      // SWSwitcherTrayApp
      // 
      this.ClientSize = new System.Drawing.Size(284, 262);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.Name = "SWSwitcherTrayApp";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SWSwitcherTrayApp_FormClosing);
      this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.SWSwitcherTrayApp_FormClosed);
      this.contextMenuStrip1.ResumeLayout(false);
      this.ResumeLayout(false);

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            System.Windows.Forms.Application.Exit();
        }

        private void notifyIcon1_MouseDoubleClick_1(object sender, MouseEventArgs e)
        {
            this.sws.RotateWallpaper();
            this.sws.RestartTimer();
        }

        private ToolStripMenuItem chooseFolderToolStripMenuItem;

        private ToolStripMenuItem interval1ToolStripMenuItem;
        private ToolStripMenuItem minToolStripMenuItem;
        private ToolStripMenuItem minToolStripMenuItem1;
        private ToolStripMenuItem minToolStripMenuItem2;
        private ToolStripMenuItem minToolStripMenuItem3;

        private void minToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.checkToolStripMenuItem(0);
        }

        private void checkToolStripMenuItem(int index)
        {
            this.minToolStripMenuItem.Checked = true;
            this.minToolStripMenuItem1.Checked = false;
            this.minToolStripMenuItem2.Checked = false;
            this.minToolStripMenuItem3.Checked = false;
            this.neverToolStripMenuItem.Checked = false;

            this.sws.SetTimeInterval(1000 * 60 * 5);
        }

        private void minToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.minToolStripMenuItem.Checked = false;
            this.minToolStripMenuItem1.Checked = true;
            this.minToolStripMenuItem2.Checked = false;
            this.minToolStripMenuItem3.Checked = false;
            this.neverToolStripMenuItem.Checked = false;

            this.sws.SetTimeInterval(1000 * 60 * 15);
        }

        private void minToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            this.minToolStripMenuItem.Checked = false;
            this.minToolStripMenuItem1.Checked = false;
            this.minToolStripMenuItem2.Checked = true;
            this.minToolStripMenuItem3.Checked = false;
            this.neverToolStripMenuItem.Checked = false;

            this.sws.SetTimeInterval(1000 * 60 * 30);
        }

        private void minToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            this.minToolStripMenuItem.Checked = false;
            this.minToolStripMenuItem1.Checked = false;
            this.minToolStripMenuItem2.Checked = false;
            this.minToolStripMenuItem3.Checked = true;
            this.neverToolStripMenuItem.Checked = false;

            this.sws.SetTimeInterval(1000 * 60 * 60);
        }

        private void neverToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.minToolStripMenuItem.Checked = false;
            this.minToolStripMenuItem1.Checked = false;
            this.minToolStripMenuItem2.Checked = false;
            this.minToolStripMenuItem3.Checked = false;
            this.neverToolStripMenuItem.Checked = true;

            this.sws.SetTimeInterval(0);
        }

        private void chooseFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    this.sws.SetWallpaperFolder(fbd.SelectedPath);
                }
            }
        }

        private ToolStripMenuItem resetWallpaperToolStripMenuItem;

        private void resetWallpaperToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!this.sws.ResetWallpaper())
            {
                DialogResult dialogResult = MessageBox.Show("Warning: the default wallpaper does not exist! Do you want to select the default file?", "", MessageBoxButtons.YesNo);
                if (dialogResult == DialogResult.Yes)
                {
                    using (var fbd = new OpenFileDialog())
                    {
                        DialogResult result = fbd.ShowDialog();

                        if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.FileName))
                        {
                            this.sws.SetDefaultWallpaper(fbd.FileName);
                            this.sws.ResetWallpaper();
                        }
                    }
                }
            }
        }

        private ToolStripMenuItem restartToolStripMenuItem;

        private void restartToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sws.RestartService();
        }

        private void SWSwitcherTrayApp_FormClosing(object sender, FormClosingEventArgs e)
        {
            notifyIcon1.Dispose();
        }

        private void SWSwitcherTrayApp_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private ToolStripMenuItem styleToolStripMenuItem;
        private ToolStripMenuItem stretchToolStripMenuItem;
        private ToolStripMenuItem centerToolStripMenuItem;
        private ToolStripMenuItem tileToolStripMenuItem;
        private ToolStripMenuItem fillToolStripMenuItem;

        private void stretchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.stretchToolStripMenuItem.Checked = true;
            this.fillToolStripMenuItem.Checked = false;
            this.centerToolStripMenuItem.Checked = false;
            this.tileToolStripMenuItem.Checked = false;

            this.sws.ChangeStyle(SWSwitcher.Style.Stretched);
        }

        private void tileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.stretchToolStripMenuItem.Checked = false;
            this.fillToolStripMenuItem.Checked = false;
            this.centerToolStripMenuItem.Checked = false;
            this.tileToolStripMenuItem.Checked = true;

            this.sws.ChangeStyle(SWSwitcher.Style.Tiled);
        }

        private void centerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.stretchToolStripMenuItem.Checked = false;
            this.fillToolStripMenuItem.Checked = false;
            this.centerToolStripMenuItem.Checked = true;
            this.tileToolStripMenuItem.Checked = false;

            this.sws.ChangeStyle(SWSwitcher.Style.Centered);
        }

        private void fillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.stretchToolStripMenuItem.Checked = false;
            this.fillToolStripMenuItem.Checked = true;
            this.centerToolStripMenuItem.Checked = false;
            this.tileToolStripMenuItem.Checked = false;

            this.sws.ChangeStyle(SWSwitcher.Style.Filled);
        }

        private ToolStripMenuItem orderToolStripMenuItem;
        private ToolStripMenuItem sequentialToolStripMenuItem;
        private ToolStripMenuItem randomToolStripMenuItem;

        private void sequentialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sequentialToolStripMenuItem.Checked = true;
            this.randomToolStripMenuItem.Checked = false;

            this.sws.ChangeOrder(SWSwitcher.Order.Sequential);

        }

        private void randomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.sequentialToolStripMenuItem.Checked = false;
            this.randomToolStripMenuItem.Checked = true;

            this.sws.ChangeOrder(SWSwitcher.Order.Random);
        }

        private ToolStripMenuItem neverToolStripMenuItem;


    }
}