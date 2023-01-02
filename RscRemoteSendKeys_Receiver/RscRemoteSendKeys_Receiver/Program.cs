using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets;

using System.Windows.Forms;

namespace RscRemoteSendKeys_Receiver
{
    class Program
    {

        public const string csAPP_TITLE = "Rsc Remote SendKeys Receiver v2.00";
        protected const string csAPP_NAME = "RscRemoteSendKeys";
  
        // Incoming data from the client.  
        public static string sKeyList = null;
        public static string sConsoleOutLast = "";

        public static void StartListening()
        {
            Console.WriteLine(csAPP_TITLE);
            Console.WriteLine();
            Console.WriteLine("ATTN: This app uses SendKeys.SendWait method to send keystrokes to the focused window and the system!");
            Console.WriteLine();
            Console.WriteLine("INFO: Use Rsc Remote SendKeys app to send keystrokes remotely.");
            Console.WriteLine();

            // Data buffer for incoming data.  
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.  
            // Dns.GetHostName returns the name of the
            // host running the application.
            string sHostName = Dns.GetHostName();
            IPHostEntry ipHostInfo = Dns.GetHostEntry(sHostName);
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            int iPort = 9000;
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, iPort);

            Console.WriteLine("Listening at:");
            Console.WriteLine("       Host: " + sHostName);
            Console.WriteLine(" IP Address: " + ipAddress.ToString());
            Console.WriteLine("       Port: " + iPort.ToString());
            Console.WriteLine();

            Console.Write("Keystroke received: ");

            // Create a TCP/IP socket.  
            Socket sckListener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and
            // listen for incoming connections.  
            try
            {
                sckListener.Bind(localEndPoint);
                sckListener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    //Console.WriteLine("Waiting for a connection...");

                    // Program is suspended while waiting for an incoming connection.  
                    Socket sckRead = sckListener.Accept();

                    if (sckRead.Poll(-1, SelectMode.SelectRead))
                    {
                        sKeyList = "";
                        // An incoming connection needs to be processed.  
                        while (true)
                        {
                            int bytesRec = sckRead.Receive(bytes);
                            sKeyList += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                            if ((sKeyList.Length) > 0 && (sKeyList[sKeyList.Length - 1] == '\n')) //(bytesRec < bytes.Length) //data.IndexOf("<EOF>") > -1)
                            {
                                sKeyList = sKeyList.Substring(0, sKeyList.Length - 1);
                                break;
                            }
                        }

                        string[] astr = sKeyList.Split('\r');
                        foreach (string sKey in astr)
                        {
                            if (sKey.Length == 0)
                                continue;

                            int iKey = 0;
                            string sSendKeysParam = "";
                            string sConsoleOut = "";
                            if (sKey[0] == '{')
                            {
                                sSendKeysParam = sKey;

                                sConsoleOut = sKey;
                            }
                            else
                            {
                                if (Int32.TryParse(sKey, out iKey))
                                {
                                    if (iKey > 0)
                                    {
                                        char cChr = (char)iKey;

                                        sSendKeysParam = cChr.ToString();

                                        sConsoleOut = cChr.ToString();
                                    }
                                }
                                else
                                {
                                    sConsoleOut = "UNKNOWN KEY: " + sKey;
                                }
                            }

                            if (sConsoleOutLast.Length > 0)
                            {
                                for (int i = 0; i < sConsoleOutLast.Length; i++)
                                {
                                    Console.Write(((char)8));
                                }
                                for (int i = sConsoleOut.Length; i < sConsoleOutLast.Length; i++)
                                {
                                    sConsoleOut += " ";
                                }
                            }
                            Console.Write(sConsoleOut);
                            sConsoleOutLast = sConsoleOut;

                            if (sSendKeysParam.Length > 0)
                            {
                                try
                                {
                                    SendKeys.SendWait(sSendKeysParam);
                                }
                                catch (Exception exc)
                                {
                                    Console.WriteLine();
                                    Console.WriteLine("SendKeys.SendWait ERROR: " + exc.Message);
                                    Console.WriteLine();

                                    Console.Write("Keystroke received: ");
                                    sConsoleOutLast = "";
                                }
                            }
                        }

                        // Echo the data back to the client.
                        /*
                        byte[] msg = Encoding.ASCII.GetBytes(data);
                        sckRead.Send(msg);
                        */

                        sckRead.Shutdown(SocketShutdown.Both);
                        sckRead.Close();
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();
        }

        static void Main(string[] args)
        {
            StartListening();
        }
    }
}
