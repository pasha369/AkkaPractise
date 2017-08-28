using System;
using System.Configuration;
using ActorServerExample.Messages;
using Akka.Actor;

namespace AkkaServerExample.Client
{
    public class GameClientActor : ReceiveActor
    {
        private readonly IActorRef _gameRenderActor;
        private readonly ActorSelection _remoteServerActor;
        private readonly ClientState _clientState;

        public GameClientActor(IActorRef gameRenderActor, ClientState state)
        {
            _gameRenderActor = gameRenderActor;
            var remoteServerAddress = ConfigurationManager.AppSettings[StringConstants.AddressKey];
            _remoteServerActor = Context.ActorSelection(remoteServerAddress);
            _clientState = state;

            Receive<UnsubscribeMessage>(msg => Unsubscribe(msg));
            Receive<UpdateLocationMessage>(msg => UpdateLocation(msg));
            Receive<MoveUpMessage>(msg => MoveUp(msg));
            Receive<MoveDownMessage>(msg => MoveDown(msg));
            Receive<MoveLeftMessage>(msg => MoveLeft(msg));
            Receive<MoveRightMessage>(msg => MoveRight(msg));

            Subscribe();
        }

        private void Unsubscribe(UnsubscribeMessage msg)
        {
            var eraseMessage = new EraseMessage(_clientState.CurrentX, _clientState.CurrentY);
            _gameRenderActor.Tell(eraseMessage);
        }

        private void Subscribe()
        {
            var subscribeMessage = new SubscribeMessage(_clientState.SubscriberGuid);
            _remoteServerActor.Tell(subscribeMessage);
        }

        private void SendLocationUpdate(short oldX, short oldY)
        {
            var updateLocationMessage = new UpdateLocationMessage(
                _clientState.Avatar, oldX, oldY, _clientState.CurrentX, _clientState.CurrentY);
            _remoteServerActor.Tell(updateLocationMessage);
        }

        private void MoveRight(MoveRightMessage msg)
        {
            MoveAndUpdateLocation(() => _clientState.CurrentX++);
        }

        private void MoveLeft(MoveLeftMessage msg)
        {
            MoveAndUpdateLocation(() => _clientState.CurrentX--);
        }

        private void MoveDown(MoveDownMessage msg)
        {
            MoveAndUpdateLocation(() => _clientState.CurrentY++);
        }

        private void MoveUp(MoveUpMessage msg)
        {
            MoveAndUpdateLocation(() => _clientState.CurrentY--);
        }

        private void UpdateLocation(UpdateLocationMessage msg)
        {
            var eraseMessage = new EraseMessage(msg.OldX, msg.OldY);
            var drawMessage = new DrawMessage(msg.NewX, msg.NewY, msg.Avatar);

            _gameRenderActor.Tell(eraseMessage);
            _gameRenderActor.Tell(drawMessage);
        }

        private void MoveAndUpdateLocation(Action move)
        {
            var oldX = _clientState.CurrentX;
            var oldY = _clientState.CurrentY;

            move();
            SendLocationUpdate(oldX, oldY);
        }
    }
}