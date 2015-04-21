using System;
using Microsoft.SPOT;
using SecretLabs.NETMF.Hardware.Netduino;
using Microsoft.SPOT.Hardware;
using System.Threading;
using Robotics.Micro.Devices;
using Robotics.Micro.Motors;
using Robotics.Micro;

namespace CSharpRobots
{
  public class Program7
  {
    public static void Main6()
    {
      //Create our "Blocks"
      var leftMotor = HBridgeMotor.CreateForNetduino(PWMChannels.PWM_PIN_D3, Pins.GPIO_PIN_D1, Pins.GPIO_PIN_D2);
      var rightMotor = HBridgeMotor.CreateForNetduino(PWMChannels.PWM_PIN_D6, Pins.GPIO_PIN_D4, Pins.GPIO_PIN_D5);

      leftMotor.CalibrationInput.Value = 1;
      rightMotor.CalibrationInput.Value = 1;

      var awesomeBlock = new AwesomeBlock(Pins.ONBOARD_BTN, Pins.ONBOARD_LED, leftMotor, rightMotor);

      int i = 0;
      while (true)
      {
        Thread.Sleep(1000); // sleep for 1000ms 

        Debug.Print("Looping" + i);
        i++;
      }
    }

    public class AwesomeBlock : Block
    {
      public AwesomeBlock(Cpu.Pin buttonPin,  Cpu.Pin ledPin, IDCMotor leftMotor, IDCMotor rightMotor)
      {
        var button = new DigitalInputPin(buttonPin);
        var led = new DigitalOutputPin(ledPin);



        button.Output.ConnectTo(led.Input);
        button.Output.ConnectTo(rightMotor.SpeedInput);
        button.Output.ConnectTo(leftMotor.SpeedInput);
      }
    }
  }
}
