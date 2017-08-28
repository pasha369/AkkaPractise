using System;

namespace ActorServerExample.Messages
{
    public class SubscribeMessage
    {
        public SubscribeMessage(Guid subscriberGuid)
        {
            SubscriberGuid = subscriberGuid;
        }

        public Guid SubscriberGuid { get; }
    }
}