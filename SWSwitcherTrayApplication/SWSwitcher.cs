using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SWSwitcherTrayApp
{
  public partial class SWSwitcher : IDisposable
  {
    [DllImport("user32.dll", CharSet = CharSet.Auto)]
    private static extern int SystemParametersInfo(
                        int uAction, int uParam,
                        string lpvParam, int fuWinIni);
    private const int SPI_SETDESKWALLPAPER = 20;
    private const int SPIF_SENDCHANGE = 0x2;

    private System.Timers.Timer Timer;
    private string DefaultPicturesFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
    private string PicturesFolderPath = Properties.Settings.Default.PicturesFolderPath;
    private string SWSwitcherPicturesFolderName = "Wallpapers";
    private string DefaultWallpaperFile = Properties.Settings.Default.DefaultWallpaperFile;
    private List<string> FilePathList = new List<string>();
    private List<int> RandomFilePathList = new List<int>();
    private Random Rnd = new Random();
    private int CurIndex = 0;
    public int TimeInterval = Properties.Settings.Default.TimeInterval; // initial = 30 mins
    public string CurWallpaper = Properties.Settings.Default.CurWallpaper;
    public Style CurStyle = (Style)Properties.Settings.Default.CurStyle;
    public Order CurOrder = (Order)Properties.Settings.Default.CurOrder;
    public bool hasRegisterAccess = true;
    public bool isElevated = false;

    public SWSwitcher()
    {

      CheckAdmin();
      InitService();
      StartTimer();

      this.ChangeWallpaper(this.CurWallpaper, this.CurStyle);
    }

    public enum Style : int
    {
      Tiled,
      Centered,
      Stretched,
      Filled
    }

    public enum Order : int
    {
      Sequential,
      Random
    }

    public void RestartService()
    {
      FilePathList.Clear();
      InitService();
    }

    public void RestartTimer()
    {
      this.StopTimer();
      this.StartTimer();
    }

    public void RotateWallpaper()
    {
      this.RestartService();
      if (this.FilePathList.Count > 0)
      {
        Console.WriteLine("Changing Wallpaper..." + this.FilePathList.ElementAt(this.CurIndex));
        int result = this.ChangeWallpaper(this.FilePathList.ElementAt(this.CurIndex), this.CurStyle);

        switch (this.CurOrder)
        {
          case Order.Random:
            if (this.RandomFilePathList.Count == 0)
            {
              this.RandomFilePathList = Enumerable.Range(0, this.FilePathList.Count).ToList<int>();
            }
            int RandomIndex = this.Rnd.Next(this.RandomFilePathList.Count);
            this.CurIndex = this.RandomFilePathList[RandomIndex];
            this.RandomFilePathList.RemoveAt(RandomIndex);
            break;
          default:
          case Order.Sequential:
            this.CurIndex = (this.CurIndex + 1) % this.FilePathList.Count;
            break;
        }

        if (result > 0)
        {
          Console.WriteLine("Done!");
        }
        else
        {
          Console.WriteLine("Failed!");
        }
      }
    }

    public int ChangeWallpaper(string path, Style style)
    {
      int ret = -1;

      if (!File.Exists(path))
      {
        //throw new Exception("ChangeWallpaper(): Missing Wallpaper File");
        MessageBox.Show("WARNING: the selected file does not exist; it may have been removed, please update the selected wallpaper folder.");
        return ret;
      }
      else
      {
        // Open registry key and update layout
        RegistryKey key = null;
        if (hasRegisterAccess)
        {
          key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
          if (key == null)
          {
            hasRegisterAccess = false;
          }
          else
            switch (style)
            {
              case Style.Stretched:
                key.SetValue(@"WallpaperStyle", 2.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
                break;
              case Style.Tiled:
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 1.ToString());
                break;
              case Style.Filled:
                key.SetValue(@"WallpaperStyle", 10.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
                break;
              default:
              case Style.Centered:
                key.SetValue(@"WallpaperStyle", 1.ToString());
                key.SetValue(@"TileWallpaper", 0.ToString());
                break;
            }

          // update local values
          this.CurWallpaper = path;
          this.CurStyle = style;
          Properties.Settings.Default["CurWallpaper"] = this.CurWallpaper;
          Properties.Settings.Default["CurStyle"] = (int)this.CurStyle;
          Properties.Settings.Default.Save();

          // Windows10 domain policy fix
          if (hasRegisterAccess && isElevated)
          {
            RegistryKey policy_key = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System", true);
            policy_key.SetValue(@"Wallpaper", path);
          }

          // Force SystemParametersInfo 
          // Windows 7 domain: OK
          // Windows 10 domain: OK with above FIX
          // Also triggers SENDCHANGE so the wallpaper updates
          ret = SystemParametersInfo(SPI_SETDESKWALLPAPER, 1, this.CurWallpaper, SPIF_SENDCHANGE);
        }

        if (!hasRegisterAccess)
        {
          MessageBox.Show("ERROR: cannot access register, wallpaper cannot be changed.");
        }
      }
      return ret;
    }

    public int ChangeStyle(Style style)
    {
      return this.ChangeWallpaper(this.CurWallpaper, style);
    }

    public int ChangeOrder(Order order)
    {
      this.CurOrder = order;
      Properties.Settings.Default["CurOrder"] = (int)this.CurOrder;
      return 0;
    }

    public void SetWallpaperFolder(string path)
    {
      if (path != "" && Directory.Exists(path))
      {
        this.PicturesFolderPath = path;
        Properties.Settings.Default["PicturesFolderPath"] = path;
        Properties.Settings.Default.Save();
        Console.Write("Changed folder to " + path);

        this.StopTimer();
        this.RestartService();
        this.StartTimer();
      }
    }

    public void SetDefaultWallpaper(string filepath)
    {
      if (File.Exists(filepath))
      {
        this.DefaultWallpaperFile = filepath;
        Properties.Settings.Default["DefaultWallpaperFile"] = filepath;
      }
    }

    public bool ResetWallpaper()
    {
      if (File.Exists(this.DefaultWallpaperFile))
      {
        this.ChangeWallpaper(this.DefaultWallpaperFile, Style.Centered);
        this.StopTimer();
        return true;
      }
      return false;
    }

    public void SetTimeInterval(int interval)
    {
      if (interval > 0 && interval < 1000 * 60 * 60 * 24)
      {
        this.TimeInterval = interval;
        Properties.Settings.Default["TimeInterval"] = interval;
        Properties.Settings.Default.Save();
        Console.Write("Set interval to " + interval / 1000 + " sec");

        this.RestartTimer();
      }

      if (interval == 0)
      {
        this.TimeInterval = interval;
        Properties.Settings.Default["TimeInterval"] = interval;
        Properties.Settings.Default.Save();
        Console.Write("Stop wallpaper rotation.");
        this.StopTimer();
      }
    }

    public void Dispose()
    {
      ((IDisposable)Timer).Dispose();
    }

    // PROTECTED FUNCTIONS
    protected void CheckAdmin()
    {

      using (System.Security.Principal.WindowsIdentity identity = System.Security.Principal.WindowsIdentity.GetCurrent())
      {
        System.Security.Principal.WindowsPrincipal principal = new System.Security.Principal.WindowsPrincipal(identity);
        isElevated = principal.IsInRole(System.Security.Principal.WindowsBuiltInRole.Administrator);
      }
      if (!isElevated)
        MessageBox.Show("WARNING: missing admin rights, won't work under Windows 10 domain systems.");

    }

    protected void InitService()
    {
      if (Directory.Exists(this.PicturesFolderPath))
      {
        LoadDirectory(this.PicturesFolderPath);
      }
      else
      {
        if (Directory.Exists(this.DefaultPicturesFolderPath))
        {
          LoadDirectory(Path.Combine(this.DefaultPicturesFolderPath, this.SWSwitcherPicturesFolderName));
        }
      }
    }

    protected void StartTimer()
    {
      if (this.TimeInterval > 0)
      {
        this.Timer = new System.Timers.Timer(this.TimeInterval);
        this.Timer.AutoReset = true;
        this.Timer.Elapsed += new System.Timers.ElapsedEventHandler(this.TimerElapsed);
        this.Timer.Start();
      }
    }

    protected void StopTimer()
    {
      if (this.Timer != null)
      {
        this.Timer.Stop();
      }
      this.Timer = null;
    }

    protected void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
    {
      RotateWallpaper();
    }

    protected void LoadDirectory(string path)
    {
      if (Directory.Exists(path))
      {
        string[] fileEntries = Directory.GetFiles(path);
        foreach (string fileName in fileEntries)
        {
          LoadFile(fileName);
        }
      }
    }

    protected void LoadFile(string path)
    {
      if (Path.GetExtension(path) == ".jpg")
      {
        this.FilePathList.Add(path);
      }
    }

  }
}
