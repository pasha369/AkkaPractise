using System;
using Akka.Actor;

namespace HelloAkkaExample
{
    class Program
    {
        private const string SysName = "MySys";

        public class Greet
        {
            public string Message { get; }

            public Greet(string msg)
            {
                Message = msg;
            }
        }

        public class GreetActor: ReceiveActor
        {
            public static string Name = "Greeter";

            public GreetActor()
            {
                Receive(PrintGreet());
            }

            private static Action<Greet> PrintGreet()
            {
                return greet => Console.WriteLine(greet.Message);
            }
        }

        static void Main(string[] args)
        {
            var sys = ActorSystem.Create(SysName);
            var actor = sys.ActorOf<GreetActor>(GreetActor.Name);
            actor.Tell(new Greet("Hello akka"));
            Console.Read();
        }
    }
}
