using System.Net.Sockets;
using System.Net;
using System.Text;

namespace TCPListener
{
    internal class Server
    {
        readonly TcpListener? server = null;
        public Server(string ip, int port)
        {
            IPAddress localAddr = IPAddress.Parse(ip);
            server = new TcpListener(localAddr, port);
            server.Start();
            //StartListener();
        }
        public bool StartService()
        {
            var myThread = new Thread(new ThreadStart(StartListener));
            myThread.IsBackground = true;  // This line will prevent thread from working after service stop.
            myThread.Start();

            return true;
        }

        public void StartListener()
        {
            try
            {
                while (true)
                {
                    // Console.WriteLine("Waiting for a connection...");
                    TcpClient client = server!.AcceptTcpClient();
                    //Console.WriteLine("Connected!");
                    Thread t = new(new ParameterizedThreadStart(HandleDeivce!));
                    t.Start(client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
                server!.Stop();
            }
        }

        public void StopService()
        {
            server!.Stop();
        }
        public void HandleDeivce(Object obj)
        {
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            Byte[] bytes = new Byte[256];
            int i;
            try
            {
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    string hex = BitConverter.ToString(bytes);
                    string data = Encoding.ASCII.GetString(bytes, 0, i);
                    //Console.WriteLine("{1}: Received: {0}", data, Environment.CurrentManagedThreadId);
                    string str = "Hello Hello Hello!";
                    Byte[] reply = Encoding.ASCII.GetBytes(str);
                    stream.Write(reply, 0, reply.Length);
                    //Console.WriteLine("{1}: Sent: {0}", str, Environment.CurrentManagedThreadId);
                }
                client.Close();
            }
            catch (Exception e)
            {
                //Console.WriteLine("Exception: {0}", e.ToString());
                client.Close();
            }
        }
    }
}
