using System;
using Exercises._05_Auth;
using FishNet.Managing;
using FishNet.Object;
using FishNet.Transporting;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Exercises._01_Understanding_NetworkManager
{
    /// <summary>
    /// LobbyNetworkSpawner spawns a specified GameObject that implements lobby behavior.
    /// </summary>
    [Tooltip("LobbyNetworkSpawner spawns a specified GameObject that implements lobby behavior.")]
    [RequireComponent(typeof(NetworkManager))]
    public class LobbyNetworkSpawner : MonoBehaviour
    {
        [SerializeField] private LobbyNetwork lobbyNetworkPrefab;

        private NetworkManager _networkManager;

        private void Awake()
        {
            _networkManager = GetComponent<NetworkManager>();
            _networkManager.ServerManager.OnServerConnectionState += ServerManager_OnServerConnectionState;
        }

        private void ServerManager_OnServerConnectionState(ServerConnectionStateArgs serverConnectionStateArgs)
        {
            if (serverConnectionStateArgs.ConnectionState != LocalConnectionState.Started)
            {
                return;
            }

            // Spawn in scene
            // TODO: update scene name
            // We want to move into a new scene because the Lobby shouldn't be in DontDestroyOnLoad;
            // remember we are loading a new game scene
            var obj = Instantiate(lobbyNetworkPrefab);
            var scene = SceneManager.GetSceneByName("Lobby"); // done because this object is in the DontDestroyOnLoad scene
            SceneManager.MoveGameObjectToScene(obj.gameObject, scene);

            // Spawn on the network too
            _networkManager.ServerManager.Spawn(obj.gameObject);
        }
    }
}
