using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.Threading;
using Robotics.Micro.Devices;
using Robotics.Micro.SpecializedBlocks;
using Robotics.Micro.Sensors.Proximity;
using Robotics.Micro.Motors;
using Robotics.Micro;
using Robotics.Micro.Sensors.Motion;
using Robotics.Micro.Sensors.Buttons;

namespace CSharpRobots
{
  public class Program
  {
    public static void Main2()
    {
      // configure an output port for us to "write" to the LED
      var led = new Microsoft.SPOT.Hardware.OutputPort(Pins.ONBOARD_LED, false);

     
      int i = 0;
      while (true)
      {
        led.Write(true); // turn on the LED 
        Thread.Sleep(250); // sleep for 250ms 
        led.Write(false); // turn off the LED 
        Thread.Sleep(250); // sleep for 250ms 

        Debug.Print("Looping" + i);
        i++;
      }

    }
  }
}
