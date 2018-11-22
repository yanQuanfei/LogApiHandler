using LogApiHandler;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using MessagePack;
using MessagePack.Resolvers;

namespace ConsoleTestApp
{
    class Program
    {
        static Logger logger = LogApiHandler.Logger.GetLogger("logSource");
        static void Main(string[] args)
        {
            logger.Debug("text");
            logger.Info("text");

            Console.ReadLine();
        }        
    }    
}
