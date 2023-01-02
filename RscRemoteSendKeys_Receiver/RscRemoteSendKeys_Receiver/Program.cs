using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using System.Net.Sockets; 

namespace RscRemoteSendKeys_Receiver
{
    class Program
    {
  
        // Incoming data from the client.  
        public static string data = null;

        public static void StartListening()
        {
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
                    data = null;

                    if (sckRead.Poll(-1, SelectMode.SelectRead))
                    {
                        // An incoming connection needs to be processed.  
                        while (true)
                        {
                            int bytesRec = sckRead.Receive(bytes);
                            data += Encoding.ASCII.GetString(bytes, 0, bytesRec);

                            if (bytesRec < bytes.Length) //data.IndexOf("<EOF>") > -1)
                            {
                                break;
                            }
                        }

                        // Show the data on the console.
                        /*
                        Console.Write("{0}", data);
                        */

                        int iData = 0;
                        if (Int32.TryParse(data, out iData))
                        {
                            if (iData > 0)
                            {
                                char cChr = (char)iData;
                                Console.Write(cChr);
                            }
                        }
                        else
                        {
                            Console.Write("{" + data + "}");
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
