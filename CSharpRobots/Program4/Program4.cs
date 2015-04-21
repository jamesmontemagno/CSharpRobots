using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.Netduino;
using Microsoft.SPOT.Hardware;
using System.Threading;

namespace CSharpRobots
{
  public class Program4
  {

    public static void Main5()
    {
      // configure an output port for us to "write" to the LED
      var led = new OutputPort(Pins.ONBOARD_LED, false);

      // 1.) Configure an input port for us to read the state of the button
      // 2.) Glitch Filter is set to false, only needed to prevent switch "bounce", and is not needed here
      // 3.) ResistorMode is set to Disabled as the netduino has a pulldown resistor
      // 4.) Switch will change from HIGH to LOW when pressed/released 
      // 5.) Tell Interrupt to trigger action whenever the switch changes state.
      var button = new InterruptPort(Pins.ONBOARD_BTN, false, Port.ResistorMode.Disabled, Port.InterruptMode.InterruptEdgeBoth);

      button.OnInterrupt += (uint port, uint data, DateTime time) =>
        {
          led.Write(data == 1);

          Debug.Print("Interrupt occured on port: " + port + " with data: " + data + " at " + time);
        };

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
