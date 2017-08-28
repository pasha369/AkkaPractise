namespace ActorServerExample.Messages
{
    public class EraseMessage
    {
        public EraseMessage(short x, short y)
        {
            X = x;
            Y = y;
        }

        public short X { get; }
        public short Y { get; }
    }
}