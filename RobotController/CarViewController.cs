using CoreMotion;
using Foundation;
using Robotics.Messaging;
using Robotics.Mobile.Core.Bluetooth.LE;
using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using UIKit;
using System.Linq;

namespace RobotController
{
  partial class CarViewController : UIViewController
  {
    public CarViewController(IntPtr handle)
      : base(handle)
    {
    }

    CMMotionManager man = new CMMotionManager();
    IAdapter adapter;
    ControlClient client;
    public IDevice Robot { get; set; }

    public override void ViewDidLoad()
    {
      base.ViewDidLoad();

      NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;

    }
			
    public async override void ViewDidAppear(bool animated)
    {
      base.ViewDidAppear(animated);

      
      adapter = Adapter.Current;

      BigTed.BTProgressHUD.Show("Connecting...");
      RunControlAsync();
      man.StartDeviceMotionUpdates(new NSOperationQueue() { Name = "robots" }, GyroChanged);
      BigTed.BTProgressHUD.Dismiss();
    }


    async Task RunControlAsync()
    {
      var cts = new CancellationTokenSource();
      try
      {
        await adapter.ConnectAsync(Robot);
        using (var s = new LEStream(Robot))
        {
          client = new ControlClient(s);
          await client.RunAsync(cts.Token);
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine("Stream failed");
        Debug.WriteLine(ex);
        InvokeOnMainThread(() =>
        {
          new UIAlertView("Unable to connect", "Please try to select a device again.", null, "OK").Show();
        });
      }
      finally
      {
        //client = null;
      }
    }

    DateTime lastGyroUpdateTime = DateTime.Now;
    static readonly TimeSpan GyroUpdateInterval = TimeSpan.FromSeconds(1.0 / 2);
    public void GyroChanged(CMDeviceMotion m, NSError e)
    {
      var now = DateTime.Now;
      if (e != null || m == null || m.Attitude == null || now - lastGyroUpdateTime <= GyroUpdateInterval)
      {
        return;
      }
      lastGyroUpdateTime = now;
       var speed = Math.Cos(Math.Max(0, Math.Min(Math.PI / 2, m.Attitude.Pitch)));
       var turn = Math.Sin(Math.Max(-Math.PI / 2, Math.Min(Math.PI / 2, m.Attitude.Roll)));



      

       InvokeOnMainThread(() =>
       {
         var speedVariable = client.Variables.FirstOrDefault(x => x.Name == "Speed");
         if (speedVariable != null)
         {
           speedVariable.Value = speed;
         }

         var turnVariable = client.Variables.FirstOrDefault(x => x.Name == "Turn");
         if (turnVariable != null)
         {
           turnVariable.Value = turn;
         }

         ViewLeft.Alpha = (nfloat)(1.0f * (turn > 0 ? 0 : turn * -1));
         ViewRight.Alpha = (nfloat) (1.0f * (turn < 0 ? 0 : turn));
         ViewForward.Alpha = (nfloat)(1.0f * (speed < 0 ? 0 : speed));

         TextTilt.Text = string.Format("Speed: {0:N2}\nTurn: {1:N2}", Math.Round(speed, 2), Math.Round(turn, 2));
       });
    }


    public override void ViewDidDisappear(bool animated)
    {
      base.ViewDidDisappear(animated);
      man.StopDeviceMotionUpdates();

       InvokeOnMainThread(() =>
       {
         var speedVariable = client.Variables.FirstOrDefault(x => x.Name == "Speed");
         if (speedVariable != null)
         {
           speedVariable.Value = 0;
         }

         var turnVariable = client.Variables.FirstOrDefault(x => x.Name == "Turn");
         if (turnVariable != null)
         {
           turnVariable.Value = 0;
         }
       });

      client = null;
    }

  }
}
