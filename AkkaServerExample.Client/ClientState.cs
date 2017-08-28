using System;

namespace AkkaServerExample.Client
{
    public class ClientState
    {
        public ClientState(short currentX, short currentY, Guid subscriberGuid, char avatar)
        {
            CurrentX = currentX;
            CurrentY = currentY;
            SubscriberGuid = subscriberGuid;
            Avatar = avatar;
        }

        public short CurrentX { get; set; }
        public short CurrentY { get; set; }
        public Guid SubscriberGuid { get; }
        public char Avatar { get; }
    }
}