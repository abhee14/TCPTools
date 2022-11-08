﻿using System.Net.Sockets;

namespace TCPClient
{
    internal class Program
    {
        static void Main()
        {
            try
            {
                // Create a TcpClient.
                // Note, for this client to work you need to have a TcpServer
                // connected to the same address as specified by the server, port
                // combination.
                Int32 port = 520;
                string server = "192.168.56.1";


                // Prefer using declaration to ensure the instance is Disposed later.
                using TcpClient client = new(server, port);

                while (true)
                {
                    Console.Write("Input Message: ");
                    var message = Console.ReadLine();
                    if (message.Equals("exit", StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                    else
                    {
                        // Translate the passed message into ASCII and store it as a Byte array.
                        Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);

                        // Get a client stream for reading and writing.
                        NetworkStream stream = client.GetStream();

                        // Send the message to the connected TcpServer.
                        stream.Write(data, 0, data.Length);

                        Console.WriteLine("Sent: {0}", message);

                        // Receive the server response.

                        // Buffer to store the response bytes.
                        data = new Byte[256];

                        // String to store the response ASCII representation.
                        String responseData = String.Empty;

                        // Read the first batch of the TcpServer response bytes.
                        Int32 bytes = stream.Read(data, 0, data.Length);
                        responseData = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                        Console.WriteLine("Received: {0}", responseData);

                        // Explicit close is not necessary since TcpClient.Dispose() will be
                        // called automatically.
                        // stream.Close();
                        // client.Close();
                    }
                }          
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("ArgumentNullException: {0}", e);
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }

            Console.WriteLine("\n Press Enter to continue...");
            Console.Read();
        }
    }
}