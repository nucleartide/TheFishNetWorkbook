using System.Collections;
using System.Linq;
using Client;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Managing.Server;
using FishNet.Object;
using FishNet.Transporting;
using UnityEngine;
using UnityEngine.Assertions;

/// <summary>
/// exposes Toast and Fullscreenalert so you can emit messages at any time in other parts of the app
/// </summary>
public class MessagesCanvas : MonoBehaviour
{
    #region Serialized Fields
    public AlertMessage Toast;
    public AlertMessage FullScreenAlert;
    public NetworkManager networkManager;
    #endregion

    #region Static Fields
    public static MessagesCanvas Instance;
    #endregion

    #region Private Fields
    private Coroutine _hideCoroutine;
    private ServerConnectionStateArgs _prevServerState;
    private ClientConnectionStateArgs _prevClientState;
    #endregion

    #region Unity Callbacks
    private void Awake()
    {
        Instance = this;
        Assert.IsNotNull(Toast);
        Assert.IsNotNull(FullScreenAlert);
    }

    private void OnEnable()
    {
        LocalPlayerAnnouncer.OnLocalPlayerUpdated += LocalPlayerAnnouncer_OnLocalPlayerUpdated;
        networkManager.ClientManager.OnClientConnectionState += ClientManager_OnClientConnectionState;
        networkManager.ServerManager.OnServerConnectionState += ServerManager_OnServerConnectionState;
        networkManager.ServerManager.OnRemoteConnectionState += ServerManager_OnRemoteConnectionState;
    }

    public void FirstInitialize()
    {
        FullScreenAlert.FirstInitialize();
        Toast.FirstInitialize();
        LocalPlayerAnnouncer_OnLocalPlayerUpdated(null);
    }

    private void Start()
    {
        FirstInitialize();
    }

    private void ClientManager_OnClientConnectionState(ClientConnectionStateArgs clientConnectionStateArgs)
    {
            // var values = string.Join(", ", _loggedInUsernames.Values.Select(v => v));
            var clients = networkManager.ServerManager.Clients.Values.Select(v => v.ClientId);
            var debugClients = string.Join(", ", clients);
            Debug.Log("[ClientManager] Current connected clients:"+ debugClients);

        if (clientConnectionStateArgs.ConnectionState == LocalConnectionState.Starting)
        {
            // FullScreenAlert.ShowPersistentMessage("Connecting...", Color.red, 0.0f);
            _prevClientState = clientConnectionStateArgs;
            return;
        }

        if (_prevClientState.ConnectionState == LocalConnectionState.Starting && clientConnectionStateArgs.ConnectionState == LocalConnectionState.Stopping)
        {
            Toast.ShowTimedMessage("Failed to start client (may need to start server first)");
            _prevClientState = clientConnectionStateArgs;
            return;
        }

        _prevClientState = clientConnectionStateArgs;
    }

    private void ServerManager_OnRemoteConnectionState(NetworkConnection conn, RemoteConnectionStateArgs remoteConnectionStateArgs)
    {
            // var values = string.Join(", ", _loggedInUsernames.Values.Select(v => v));
            var clients = networkManager.ServerManager.Clients.Values.Select(v => v.ClientId);
            var debugClients = string.Join(", ", clients);
            Debug.Log("[ServerManager] Current connected clients:"+ debugClients);
    }
    
    private void ServerManager_OnServerConnectionState(ServerConnectionStateArgs serverConnectionStateArgs)
    {
            // var values = string.Join(", ", _loggedInUsernames.Values.Select(v => v));
            var clients = networkManager.ServerManager.Clients.Values.Select(v => v.ClientId);
            var debugClients = string.Join(", ", clients);
            Debug.Log("[ServerManager] Current connected clients:"+ debugClients);
            
        if (serverConnectionStateArgs.ConnectionState == LocalConnectionState.Starting)
        {
            // FullScreenAlert.ShowPersistentMessage("Connecting...", Color.red, 0.0f);
            _prevServerState = serverConnectionStateArgs;
            return;
        }

        if (_prevServerState.ConnectionState == LocalConnectionState.Starting && serverConnectionStateArgs.ConnectionState == LocalConnectionState.Stopping)
        {
            Toast.ShowTimedMessage("Failed to start server (server may already exist)");
            _prevServerState = serverConnectionStateArgs;
            return;
        }

        _prevServerState = serverConnectionStateArgs;
    }

    private void OnDisable()
    {
        LocalPlayerAnnouncer.OnLocalPlayerUpdated -= LocalPlayerAnnouncer_OnLocalPlayerUpdated;
        networkManager.ClientManager.OnClientConnectionState -= ClientManager_OnClientConnectionState;
        networkManager.ServerManager.OnServerConnectionState -= ServerManager_OnServerConnectionState;
    }
    #endregion

    #region Private Methods
    private void LocalPlayerAnnouncer_OnLocalPlayerUpdated(NetworkObject networkObject)
    {
        if (_hideCoroutine != null)
        {
            StopCoroutine(_hideCoroutine);
            _hideCoroutine = null;
        }

        if (networkObject == null)
        {
            FullScreenAlert.ShowPersistentMessage("Waiting for player to connect", Color.red, 1.0f);
        }
        else
        {
            _hideCoroutine = StartCoroutine(__ClearWaitingMessage(networkObject));
        }
    }

    private IEnumerator __ClearWaitingMessage(NetworkObject networkObject)
    {
        var client = networkObject.GetComponent<ClientInstance>();
        while (!client.Initialized)
        {
            yield return null;
        }
        
        FullScreenAlert.HidePersistentMessage();
        _hideCoroutine = null;
    }
    #endregion
}
