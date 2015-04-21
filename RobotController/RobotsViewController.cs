using Foundation;
using Robotics.Mobile.Core.Bluetooth.LE;
using System;
using System.CodeDom.Compiler;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using UIKit;

namespace RobotController
{
	partial class RobotsViewController : UITableViewController, IUITableViewDataSource
	{
    public bool IsBusy { get; set; }

    ObservableCollection<IDevice> devices;

		public RobotsViewController (IntPtr handle) : base (handle)
		{
		}

    IAdapter adapter;
    public override void ViewDidLoad()
    {
      base.ViewDidLoad();
      NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
      devices = new ObservableCollection<IDevice>();
      adapter = Adapter.Current;

      this.RefreshControl = new UIRefreshControl();

      this.RefreshControl.ValueChanged += (sender, args) =>
        {
          if (IsBusy)
            return;

          StartScanning();
        };

      adapter.DeviceDiscovered += (object sender, DeviceDiscoveredEventArgs e) =>
      {
        InvokeOnMainThread(() =>
        {
          //TODO: uncomment this if there are a lot of Bluetooth devices around cluttering your list (and remove the line below)
          if (e.Device.Name != null) 
          {
            if (e.Device.Name.ToLower().Contains("biscuit")) 
            {
              devices.Add(e.Device);
              TableView.ReloadData();
            }
          }
        });
      };

      adapter.ScanTimeoutElapsed += (sender, e) =>
      {
        IsBusy = false;
        Debug.WriteLine("Scan timeout");
      };

      StartScanning();
    }


    void StartScanning()
    {
      if (!adapter.IsScanning)
      {
        IsBusy = true;
        devices.Clear();
        adapter.StartScanningForDevices();
        this.RefreshControl.BeginRefreshing();
        Debug.WriteLine("adapter.StartScanningForDevices()");
      }
    }

    void StopScanning()
    {
      // stop scanning
      Task.Run(() =>
      {
        if (adapter.IsScanning)
        {
          Debug.WriteLine("Still scanning, stopping the scan");
          adapter.StopScanningForDevices();
          IsBusy = false;
          this.RefreshControl.EndRefreshing();
        }
      });
    }

    public override void ViewDidDisappear(bool animated)
    {
      base.ViewDidDisappear(animated);
      StopScanning();
    }

    UITableViewCell IUITableViewDataSource.GetCell(UITableView tableView, NSIndexPath indexPath)
    {
      var cell = tableView.DequeueReusableCell("robot", indexPath);

      cell.TextLabel.Text = devices[indexPath.Row].Name;
      cell.DetailTextLabel.Text = devices[indexPath.Row].ID.ToString();

      return cell;
    }

    nint IUITableViewDataSource.RowsInSection(UITableView tableView, nint section)
    {
      return devices.Count;
    }

    public override void PrepareForSegue(UIStoryboardSegue segue, NSObject sender)
    {
      base.PrepareForSegue(segue, sender);
      var device = devices[TableView.IndexPathForSelectedRow.Row];

      var vc = segue.DestinationViewController as CarViewController;
      vc.Robot = device;
    }
  }
}
