using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.Netduino;
using Microsoft.SPOT.Hardware;

namespace CSharpRobots
{
  public class Program3
  {

    public static void Main3()
    {
      // configure an output port for us to "write" to the LED
      var led = new OutputPort(Pins.ONBOARD_LED, false);

      // 1.) Configure an input port for us to read the state of the button
      // 2.) Glitch Filter is set to false, only needed to prevent switch "bounce", and is not needed here
      // 3.) ResistorMode is set to Disabled as the netduino has a pulldown resistor
      // 4.) Switch will change from HIGH to LOW when pressed/released 
      var button = new InputPort(Pins.ONBOARD_BTN, false, Port.ResistorMode.Disabled);

      bool buttonState = false;
      // 1.) Continuous loop to look for state changes
      while (true)
      {
        var newState = button.Read();

        if (buttonState != newState)
        {
          buttonState = newState;
          led.Write(buttonState);
        }
      }

    }
  }
}
