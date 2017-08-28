using System;
using ActorServerExample.Messages;
using Akka.Actor;

namespace AkkaServerExample.Client
{
    public class GameRenderingActor : ReceiveActor
    {
        public GameRenderingActor()
        {
            Receive<DrawMessage>(msg => Draw(msg));
            Receive<EraseMessage>(msg => Erase(msg));
        }

        private void Draw(DrawMessage msg)
        {
            Console.SetCursorPosition(msg.X, msg.Y);
            Console.Write(msg.Character);
        }

        private void Erase(EraseMessage msg)
        {
            const char emptyChar = ' ';
            var drawMessage = new DrawMessage(msg.X, msg.Y, emptyChar);
            Self.Tell(drawMessage);
        }
    }
}