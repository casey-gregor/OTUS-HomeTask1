using UnityEngine;

namespace RealTime
{
    public sealed class ServerConnectPresenter
    {
        private readonly ServerConnectView _view;
        private readonly string _connectingMessage = "Connecting to server...";
        private readonly string _connectedMessage = "Connected to server.";

        public ServerConnectPresenter(ServerConnectView view)
        {
            _view = view;
        }

        public void ShowConnectingMessage()
        {
            Color color = Color.red;
            _view.ShowMessage(_connectingMessage, color);
        }

        public void ShowConnectedMessage()
        {
            Color color = Color.blue;
            _view.ShowMessage(_connectedMessage, color);
        }
    }
}