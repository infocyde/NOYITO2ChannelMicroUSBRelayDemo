using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace SwitchDemo
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine(" ************* USB Power Switch Demo *************\n");
            Console.WriteLine(" For use with NOYITO 2-Channel Micro USB Relay Module USB Smart Control");
            Console.WriteLine(" This approach will probably work for their 4 and 8 channel models as well with some tweaking.\n\n");
            Console.WriteLine(" This will just demo turning power on and off to each side.\n Make sure you a connected then hit any key.\n");
            Console.WriteLine(" Hint: Watch the board to see if the red light comes on for each side.\n");
            Console.Write(" Hit any key:");
            Console.ReadKey();
            Console.WriteLine("\n");

            try
            {
                using (USBPowerSwitch s = new USBPowerSwitch())
                {
                    Console.WriteLine(" Switch Detected on COM " + s._comPortName);
                    Console.WriteLine(" " + s.OpenPort());
                    Console.WriteLine(" Attempting r1 on");
                    s.TalkToSwitch(USBPowerSwitch.USBSwitchCommand.r1_on);
                    Console.WriteLine(" Pausing for 3 seconds...");
                    Thread.Sleep(3000);
                    Console.WriteLine(" Attempting r1 off");
                    s.TalkToSwitch(USBPowerSwitch.USBSwitchCommand.r1_off);
                    Console.WriteLine(" Pausing for 3 seconds...");
                    Thread.Sleep(3000);
                    Console.WriteLine(" Attempting r2 on");
                    s.TalkToSwitch(USBPowerSwitch.USBSwitchCommand.r2_on);
                    Console.WriteLine(" Pausing for 3 seconds...");
                    Thread.Sleep(3000);
                    Console.WriteLine(" Attempting r2 off");
                    s.TalkToSwitch(USBPowerSwitch.USBSwitchCommand.r2_off);
                    Thread.Sleep(3000);
                    Console.WriteLine(" \n All done.");
                    Console.Write("\n\n Hit any key to exit:");
                    Console.ReadKey();

                }
            }
            catch (System.Exception ex)
            {
                Console.WriteLine(" Ooops....something went wrong.\n\n");
                Console.WriteLine(ex.ToString());
            }

        }
    }
}
