using System;
using System.Configuration;
using System.Linq;
using System.Text;
using ActorServerExample.Messages;
using Akka.Actor;

namespace AkkaServerExample.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = ConfigurationManager.AppSettings;
            var sysName = config[StringConstants.SysName];
            // Set up console
            Console.Title = $"{sysName} - client";
            Console.OutputEncoding = Encoding.UTF8;
            Console.CursorVisible = false;
            
            try
            {
                using (var actorSys = ActorSystem.Create(StringConstants.SysName))
                {
                    const short currentX = 40;
                    const short currentY = 12;

                    var subscriberGuid = Guid.NewGuid();
                    var avatar = EnterAvatar();
                    var renderActor = actorSys.ActorOf<GameRenderingActor>(StringConstants.GameRenderActor);
                    var clientActor = actorSys.ActorOf(
                        Props.Create<GameClientActor>(renderActor, new ClientState(currentX, currentY, subscriberGuid, avatar)),
                        StringConstants.GameClientActor);
                    HandleInput(clientActor);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.Read();
        }

        private static char EnterAvatar()
        {
            Console.WriteLine("Enter avatar char: ");
            var input = Console.ReadLine();
            if(string.IsNullOrEmpty(input))
                throw new ArgumentNullException();
            Console.Clear();
            return input.First();
        }

        private static void HandleInput(IActorRef clientActor)
        {
            while (true)
            {
                var key = Console.ReadKey(intercept: true);
                switch (key.Key)
                {
                    case ConsoleKey.UpArrow:
                        clientActor.Tell(new MoveUpMessage());
                        break;
                    case ConsoleKey.DownArrow:
                        clientActor.Tell(new MoveDownMessage());
                        break;
                    case ConsoleKey.LeftArrow:
                        clientActor.Tell(new MoveLeftMessage());
                        break;
                    case ConsoleKey.RightArrow:
                        clientActor.Tell(new MoveRightMessage());
                        break;
                    default: return;
                }
            }
        }
    }
}
