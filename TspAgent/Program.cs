using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using System.Text;
using System.Threading;

namespace TspAgent
{
    public class Program
    {
        //201166 scsi ide
        volatile static TspService.TspService retentivePopuationAccessor;
        volatile static int retentive = 0;
        static object locked = new object();
        static string name;
        static int threads;

        public static void Main(string[] args)
        {
            name = args.Length > 0 ? args[0] : $"agent {new Random().Next(0,100)}";
            threads = args.Length > 1 ? Int32.Parse(args[1]) : 5;

            ChannelServices.RegisterChannel(new TcpChannel());
            retentivePopuationAccessor = Activator.GetObject(typeof(TspService.TspService), 
                                          "tcp://localhost:8000/tsp")
                                           as TspService.TspService;

            foreach (var item in new byte[threads])
                ThreadPool.QueueUserWorkItem(new WaitCallback((x)=>Process(name)));
            Console.ReadLine();
        }

        private static void Process(string name) {

            while (true)
            {
                var candidate = retentivePopuationAccessor.GetBestPopulation().Evolve();
                if (candidate.maxFit > retentivePopuationAccessor.GetBestPopulation().maxFit)
                {
                    lock (locked)
                    {
                        retentivePopuationAccessor.SetBestPopuation(candidate, name);
                        Console.Clear();
                        Console.WriteLine($"{name} agent  ");
                        Console.WriteLine($"{++retentive} population of {name} surpassed the best in The Universe!");
                        Console.WriteLine($"The race continues...");
                    }
                }
            }
        }
    }
}
