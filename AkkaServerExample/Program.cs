using System;
using System.Configuration;
using Akka.Actor;

namespace AkkaServerExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationManager.AppSettings;
            var sysName = config[StringConstants.SysName];
            Console.Title = $"{sysName} - server";
            try
            {
                using (var actorSys = ActorSystem.Create(sysName))
                {
                    var server = actorSys.ActorOf<AkkaServer>(StringConstants.ServerActor);
                    var serverAddress = config[StringConstants.ServerActorAddress];
                    var remoteServerActor = actorSys.ActorSelection(serverAddress);
                    Console.Read();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.Read();
        }
    }
}
