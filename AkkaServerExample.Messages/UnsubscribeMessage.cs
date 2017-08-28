using System;

namespace ActorServerExample.Messages
{
    public class UnsubscribeMessage
    {
        public UnsubscribeMessage(Guid subdcriberGuid, short lastX, short lastY)
        {
            SubdcriberGuid = subdcriberGuid;
            LastX = lastX;
            LastY = lastY;
        }

        public Guid SubdcriberGuid { get; }
        public short LastX { get; }
        public short LastY { get; }
    }
}