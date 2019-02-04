using System;
using System.Collections.Generic;
using System.Linq;

using System.Runtime.Remoting;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading.Tasks;
using TspService;

namespace TspServer
{
    class Program
    {
        static void Main(string[] args)
        {
            var channel = new TcpChannel(8000);
            ChannelServices.RegisterChannel(channel);
            var service = new TspService.TspService();
            RemotingConfiguration.RegisterWellKnownServiceType(typeof(TspService.TspService), "tsp", WellKnownObjectMode.Singleton);
            Console.WriteLine("Server started");
            Console.ReadLine();
        }
    }
}
