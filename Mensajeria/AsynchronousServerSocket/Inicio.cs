using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Configuration;

namespace SimuladorSwitchTransaccional
{

    public class SynchronousSocketListener
    {

        // Incoming data from the client.
        public static int vg_iPort = int.Parse(ConfigurationManager.AppSettings["Port"]);
        public static string data = null;

        public static void StartListening()
        {
            // Data buffer for incoming data.
            byte[] bytes = new Byte[1024];

            // Establish the local endpoint for the socket.
            // Dns.GetHostName returns the name of the 
            // host running the application.
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, vg_iPort);

            // Create a TCP/IP socket.
            Socket listener = new Socket(AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp);

            // Bind the socket to the local endpoint and 
            // listen for incoming connections.
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.
                    Socket handler = listener.Accept();
                    data = null;

                    // An incoming connection needs to be processed.
                    //while (true)
                    //{
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    //if (data.IndexOf("<EOF>") > -1)
                    //{
                    //break;
                    //}
                    //}

                    string vl_sPosicion1 = Encoding.ASCII.GetString(bytes, 0, 1);
                    string vl_sPosicion2 = Encoding.ASCII.GetString(bytes, 1, 1);


                    // Show the data on the console.
                    Console.WriteLine("Text received : {0}", data);

                    // Echo the data back to the client.
                    byte[] msg = Encoding.ASCII.GetBytes(data);

                    handler.Send(msg);
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("\nPress ENTER to continue...");
            Console.Read();

        }

        public static int Main(String[] args)
        {
            Console.WriteLine("Iniciando el Simulador de Switch Transaccional");
            IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
            Console.WriteLine("Dirección IP: " + ipHostInfo.AddressList[0].ToString());
            Console.WriteLine("Puerto: " + vg_iPort.ToString());
            StartListening();
            Console.WriteLine("Termina el Simulador de Tramas");
            return 0;
        }
    }

}
