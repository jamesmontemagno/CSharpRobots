using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;
using System.Threading;

namespace CSharpRobots.Program2
{
  public class Program2
  {

    static OutputPort led;
    public static void Main2()
    {
      // configure an output port for us to "write" to the LED
      led = new OutputPort(Pins.ONBOARD_LED, false);

      while (true)
      {
        ShortBeep();
        ShortBeep();
        ShortBeep();
        ShortBeep();
        Debug.Print("h");

        ShortPause();

        ShortBeep();
        Debug.Print("e");

        ShortPause();
        ShortBeep();
        LongBeep();
        ShortBeep();
        ShortBeep();
        Debug.Print("l");

        ShortPause();
        ShortBeep();
        LongBeep();
        ShortBeep();
        ShortBeep();
        Debug.Print("l");

        ShortPause();
        LongBeep();
        LongBeep();
        LongBeep();
        Debug.Print("o");

        ShortPause();
        LongPause();//space
        ShortPause();
        Debug.Print(" ");

        ShortBeep();
        LongBeep();
        LongBeep();

        Debug.Print("w");

        ShortPause();
        LongBeep();
        LongBeep();
        LongBeep();
        Debug.Print("o");

        ShortPause();
        ShortBeep();
        LongBeep();
        ShortBeep();
        Debug.Print("r");


        ShortPause();
        ShortBeep();
        LongBeep();
        ShortBeep();
        ShortBeep();
        Debug.Print("l");

        ShortPause();
        LongBeep();
        ShortBeep();
        ShortBeep();
        Debug.Print("d");

        LongPause();
        LongPause();

      } 
    }

    private static void ShortBeep()
    {
      led.Write(true);
      Thread.Sleep(100);
      led.Write(false);
      Thread.Sleep(50);
    }

    private static void LongBeep()
    {
      led.Write(true);
      Thread.Sleep(300);
      led.Write(false);
      Thread.Sleep(50);
    }

    private static void ShortPause()
    {
      led.Write(false);
      Thread.Sleep(100);
    }

    private static void LongPause()
    {
      led.Write(false);
      Thread.Sleep(600);
    }
  }
}
