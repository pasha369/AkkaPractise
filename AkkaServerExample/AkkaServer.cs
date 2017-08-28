using System;
using System.Collections.Generic;
using ActorServerExample.Messages;
using Akka.Actor;

namespace AkkaServerExample
{
    public class AkkaServer : ReceiveActor
    {
        private readonly Dictionary<Guid, IActorRef> _subscribersMap;

        public AkkaServer()
        {
            _subscribersMap = new Dictionary<Guid, IActorRef>();

            Receive<SubscribeMessage>(msg => Subscribe(msg));
            Receive<UnsubscribeMessage>(msg => Unsubscribe(msg));
            Receive<UpdateLocationMessage>(msg => UpdateLocation(msg));
        }

        private void Unsubscribe(UnsubscribeMessage msg)
        {
            if (_subscribersMap.ContainsKey(msg.SubdcriberGuid))
                _subscribersMap.Remove(msg.SubdcriberGuid);
            Broadcast(msg);
        }

        private void Broadcast<T>(T msg)
            where T : class
        {
            foreach (var subscriber in _subscribersMap.Values)
            {
                subscriber.Tell(msg);
            }
        }

        private void Subscribe(SubscribeMessage msg)
        {
            _subscribersMap[msg.SubscriberGuid] = Sender;
        }

        private void UpdateLocation(UpdateLocationMessage msg)
        {
            Broadcast(msg);
        }
    }
}