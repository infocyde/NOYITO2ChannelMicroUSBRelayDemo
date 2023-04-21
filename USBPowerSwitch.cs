using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO.Ports;

namespace SwitchDemo
{
    public class USBPowerSwitch : IDisposable
    {

        // good info here
        //https://www.amazon.com/NOYITO-2-Channel-Module-Control-Intelligent/dp/B081RM7PMY/ref=sr_1_3?crid=T0POA7LV6OVN&keywords=usb+relay&qid=1680817548&sprefix=USB+Relay%2Caps%2C141&sr=8-3


        public enum USBSwitchCommand
        {
            r1_on = 0,
            r1_off = 1,
            r2_on = 3,
            r2_off = 4
        }

        //protected string _comPortName;
        public string _comPortName;
        const int _baudRate = 9600;
        const int readTimeout = 500;
        const int writeTimeout = 500;
        private SerialPort serialPort;


        public USBPowerSwitch()
        {
            this.AutoDetectComPort();
            //this.OpenPort();  //optionally open the port.
        }

        public void AutoDetectComPort()
        {
            // Create WMI query
            string query = "SELECT * FROM Win32_PnPEntity WHERE Caption LIKE '%(COM%)'";
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher(query))
            {
                foreach (ManagementObject queryObj in searcher.Get())
                {
                    // optionally you could use the DeviceId which is USB\VID_1A86&PID_7523\5&2A8BC7CB&0&1
                    if (queryObj["Description"].ToString().IndexOf("USB-SERIAL CH340") > -1)
                    {

                        // Get the Caption property, which contains the COM port information
                        string caption = queryObj["Caption"].ToString();

                        // Extract the COM port information from the caption
                        int startIndex = caption.LastIndexOf("(COM") + 1;
                        int endIndex = caption.LastIndexOf(")");
                        if (startIndex >= 0 && endIndex > startIndex)
                        {
                            _comPortName = caption.Substring(startIndex, endIndex - startIndex);
                            //Console.WriteLine(" Port: " + comPort + "\n");
                        }
                        else
                        {
                            throw new Exception("USB Switch Not Found!");
                        }
                        break;
                    }
                }
            }
        }

        public string TalkToSwitch(USBSwitchCommand cmd)
        {

            string sResult = string.Empty;
            byte[] cmdbytes = null;

            if (cmd == USBSwitchCommand.r1_on)
                cmdbytes = new byte[] { 0xA0, 0x01, 0x01, 0xA2 };
            if (cmd == USBSwitchCommand.r1_off)
                cmdbytes = new byte[] { 0xA0, 0x01, 0x00, 0xA1 };
            if (cmd == USBSwitchCommand.r2_on)
                cmdbytes = new byte[] { 0xA0, 0x02, 0x01, 0xA3 };
            if (cmd == USBSwitchCommand.r2_off)
                cmdbytes = new byte[] { 0xA0, 0x02, 0x00, 0xA2 };


            serialPort.Write(cmdbytes, 0, cmdbytes.Length);


            // this power switch is pretty basic.  No confirmation sent back so the point of below is useless.
            //try
            //{
            //    byte[] responseBuffer = new byte[1024];
            //    int bytesRead = serialPort.Read(responseBuffer, 0, responseBuffer.Length);

            //    return System.Text.Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);
            //}
            //catch
            //{
            //    return "Good?";
            //}

            return "Good?";

        }

        public string OpenPort()
        {

            serialPort = new SerialPort(_comPortName, _baudRate);
            serialPort.DtrEnable = true; //??
            serialPort.StopBits = StopBits.One;
            serialPort.ReadTimeout = readTimeout;
            serialPort.WriteTimeout = writeTimeout;

            serialPort.Open(); //takes some time. If disconnect here, exception (access denied)
            return "Opened port " + _comPortName + " baudRate: " + serialPort.BaudRate;


        }

        public string ClosePort()
        {
            if (serialPort.IsOpen)
                serialPort.Close(); //takes some time. If disconnect here, exception (access denied)
            return "port Closed";
        }

        public void Dispose()
        {

            ClosePort();
            serialPort.Dispose();
        }
    }
}
