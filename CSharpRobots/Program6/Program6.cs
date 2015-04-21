using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.Netduino;
using Microsoft.SPOT.Hardware;
using System.Threading;
using Robotics.Micro.Devices;
using Robotics.Micro.Motors;

namespace CSharpRobots
{
  public class Program6
  {
    public static void Main6()
    {
      //Create our "Blocks"
      var button = new DigitalInputPin(Pins.ONBOARD_BTN);
      var led = new DigitalOutputPin(Pins.ONBOARD_LED);


      var leftMotor = HBridgeMotor.CreateForNetduino(PWMChannels.PWM_PIN_D3, Pins.GPIO_PIN_D1, Pins.GPIO_PIN_D2);
      leftMotor.CalibrationInput.Value = 1;

      var rightMotor = HBridgeMotor.CreateForNetduino(PWMChannels.PWM_PIN_D6, Pins.GPIO_PIN_D4, Pins.GPIO_PIN_D5);
      rightMotor.CalibrationInput.Value = 1;

     

      button.Output.ConnectTo(led.Input);
      button.Output.ConnectTo(rightMotor.SpeedInput);
      button.Output.ConnectTo(leftMotor.SpeedInput);

      Thread.Sleep(Timeout.Infinite);
    }
  }
}
