using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.Netduino;
using Microsoft.SPOT.Hardware;
using System.Threading;
using Robotics.Micro.Devices;

namespace CSharpRobots
{
  public class Program5
  {

    public static void Main2()
    {
      //Create our "Blocks"
      var led = new DigitalOutputPin(Pins.ONBOARD_LED);
      var button = new DigitalInputPin(Pins.ONBOARD_BTN);

      button.Output.ConnectTo(led.Input);

      int i = 0;
      while (true)
      {
        Thread.Sleep(1000); // sleep for 1000ms 

        Debug.Print("Looping" + i);
        i++;
      }
    }
  }
}
