using FishNet.Managing;
using FishNet.Transporting;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace Exercises._02_Server_Client_Sockets
{
    public class NetworkCanvas : MonoBehaviour
    {
        private enum AutomaticConnection
        {
            None,
            ServerOnly,
            ClientOnly,
            ServerAndClient,
        }

        [SerializeField] private NetworkManager networkManager;
        [SerializeField] private AutomaticConnection automaticConnection = AutomaticConnection.None;

        [SerializeField] private Color connectionStartedColor = Color.green;
        [SerializeField] private Color connectionStoppedColor = Color.gray;
        [SerializeField] private Color connectionPendingColor = Color.yellow;

        [SerializeField] private Image serverStatusIndicator;
        [SerializeField] private Image clientStatusIndicator;

        private void UpdateColor()
        {
            clientStatusIndicator.color = _clientState switch
            {
                LocalConnectionState.Started => connectionStartedColor,
                LocalConnectionState.Stopped => connectionStoppedColor,
                _ => connectionPendingColor
            };

            serverStatusIndicator.color = _serverState switch
            {
                LocalConnectionState.Started => connectionStartedColor,
                LocalConnectionState.Stopped => connectionStoppedColor,
                _ => connectionPendingColor
            };
        }

        private LocalConnectionState _clientState = LocalConnectionState.Stopped;
        private LocalConnectionState _serverState = LocalConnectionState.Stopped;

        private void Awake()
        {
            Assert.IsNotNull(networkManager);
            Assert.IsNotNull(serverStatusIndicator);
            Assert.IsNotNull(clientStatusIndicator);
        }

        private void Start()
        {
            UpdateColor();

            // MessagesCanvas.Instance.FullScreenAlert.ShowPersistentMessage("Waiting for connection...", Color.black, 0.0f);

            networkManager.ClientManager.OnClientConnectionState += ClientManager_OnClientConnectionState;
            networkManager.ServerManager.OnServerConnectionState += ServerManager_OnServerConnectionState;

            if (automaticConnection is AutomaticConnection.ServerOnly or AutomaticConnection.ServerAndClient)
            {
                HandleClickServer();
            }
            // Should not be in command-line mode.
            else if (!Application.isBatchMode && automaticConnection is AutomaticConnection.ClientOnly or AutomaticConnection.ServerAndClient)
            {
                HandleClickClient();
            }
        }

        private void OnDestroy()
        {
            networkManager.ClientManager.OnClientConnectionState -= ClientManager_OnClientConnectionState;
            networkManager.ServerManager.OnServerConnectionState -= ServerManager_OnServerConnectionState;
        }

        private void ClientManager_OnClientConnectionState(ClientConnectionStateArgs clientConnectionStateArgs)
        {
            _clientState = clientConnectionStateArgs.ConnectionState;
            UpdateColor();

            if (_clientState == LocalConnectionState.Starting)
            {
                // MessagesCanvas.Instance.FullScreenAlert.ShowPersistentMessage("Connecting...", Color.black, 0.0f);
            }
        }

        private void ServerManager_OnServerConnectionState(ServerConnectionStateArgs serverConnectionStateArgs)
        {
            _serverState = serverConnectionStateArgs.ConnectionState;
            UpdateColor();
        }

        public void HandleClickServer()
        {
            if (_serverState == LocalConnectionState.Stopped)
            {
                networkManager.ServerManager.StartConnection();
            }
            else
            {
                networkManager.ServerManager.StopConnection(true);
            }
        }

        public void HandleClickClient()
        {
            if (_clientState == LocalConnectionState.Stopped)
            {
                networkManager.ClientManager.StartConnection();
            }
            else
            {
                networkManager.ClientManager.StopConnection();
            }
        }
        
    }
}
