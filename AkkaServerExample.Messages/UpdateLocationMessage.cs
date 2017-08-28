namespace ActorServerExample.Messages
{
    public class UpdateLocationMessage
    {
        public UpdateLocationMessage(
            char avatar, short oldX, short oldY, short newX, short newY)
        {
            Avatar = avatar;
            OldX = oldX;
            OldY = oldY;
            NewX = newX;
            NewY = newY;
        }

        public char Avatar { get; }
        public short OldX { get; }
        public short OldY { get; }
        public short NewX { get; }
        public short NewY { get; }
    }
}