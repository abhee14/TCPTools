using Topshelf;

namespace TCPListener
{
    internal class Program
    {
        static void Main()
        {
            //Thread t = new Thread(delegate ()
            //{
            //    Server myServer = new Server("192.168.0.66", 520);
            //});
            //t.Start();
            var exitCode = HostFactory.Run(x =>
            {
                x.Service<Server>(s =>
                {
                    s.ConstructUsing(server => new Server("192.168.0.66", 520));
                    s.WhenStarted(server => server.StartService());
                    s.WhenStopped(server => server.StopService());
                });
                x.RunAsLocalSystem();
                x.SetServiceName("HospitalScannersTCPServer");
                x.SetDisplayName("Hospital Scanners TCP Server");
                x.SetDescription("TCP Servers for Hospital Scanners");

            });

            int exitCodeValue = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
            Environment.ExitCode = exitCodeValue;       
        }
    }
}