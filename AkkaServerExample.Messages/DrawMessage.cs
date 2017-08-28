namespace ActorServerExample.Messages
{
    public class DrawMessage
    {
        public DrawMessage(short x, short y, char character)
        {
            X = x;
            Y = y;
            Character = character;
        }

        public short X { get; }
        public short Y { get; }
        public char Character { get; }

    }
}