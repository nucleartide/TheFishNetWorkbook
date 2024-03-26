using System;
using System.Collections.Generic;
using System.Linq;
using FishNet.Connection;
using FishNet.Managing;
using FishNet.Object;
using FishNet.Transporting;
using UnityEngine;
using UnityEngine.Assertions;

namespace Exercises._05_Auth
{
    public class LobbyNetwork : NetworkBehaviour
    {
        private Dictionary<NetworkConnection, string> _loggedInUsernames = new();

        private static LobbyNetwork _instance;

        public static LobbyNetwork Instance
        {
            get
            {
                if (_instance == null)
                {
                    throw new Exception("LobbyNetwork does not exist in the scene, make sure you have one");
                }

                return _instance;
            }
        }

        private void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
            }
        }

        private void OnEnable()
        {
            // networkManager.ClientManager.OnRemoteConnectionState += ClientManager_OnRemoteConnectionState;
        }

        private void OnDisable()
        {
            // networkManager.ClientManager.OnRemoteConnectionState -= ClientManager_OnRemoteConnectionState;
        }

        #if false
        private void ClientManager_OnRemoteConnectionState(ClientConnectionStateArgs obj)
        {
            obj.
            throw new NotImplementedException();
        }
        #endif

        public static bool ValidateUsername(string username, ref string validationMessage)
        {
            // only letters
            if (!username.All(char.IsLetter))
            {
                validationMessage = "Username may only contain letters.";
                return false;
            }

            // be at least 3 characters
            if (username.Length < 3)
            {
                validationMessage = "Username must be at least 3 characters.";
                return false;
            }

            // be a max of 15 characters
            if (username.Length > 15)
            {
                validationMessage = "Username may not exceed 15 characters.";
                return false;
            }

            return true;
        }

        [Client]
        public void SignIn(string username)
        {
            Debug.Log("[LobbyNetwork] In client, signing in...");
            CmdSignIn(username);
        }

        [ServerRpc(RequireOwnership = false)] // omitting RequireOwnership, see what happens - silent no-op
        private void CmdSignIn(string username, NetworkConnection conn = null) // omitting conn = null, see what happens - won't compile
        {
            Debug.Log("[LobbyNetwork] In server, signing in..."); 

            string validationMessage = null;
            var isValid = LobbyNetwork.ValidateUsername(username, ref validationMessage);
            if (!isValid)
            {
                TargetSignInFailed(conn, validationMessage);
                return;
            }

            if (_loggedInUsernames.Any(pair => pair.Value.ToLower() == username.ToLower()))
            {
                validationMessage = "Username is taken";
                TargetSignInFailed(conn, validationMessage);
                return;
            }

            _loggedInUsernames.Add(conn, username);
            var values = string.Join(", ", _loggedInUsernames.Values.Select(v => v));
            Debug.Log("Current logged in usernames:"+ values);

            TargetSignInSuccess(conn);
        }

        [TargetRpc]
        private void TargetSignInSuccess(NetworkConnection conn)
        {
            // TODO: Show feedback in the frontend.
            // Canvases.LoginCanvas.SignInSuccess();
            MessagesCanvas.Instance.Toast.ShowTimedMessage("success");
        }

        [TargetRpc]
        private void TargetSignInFailed(NetworkConnection conn, string validationMessage)
        {
            MessagesCanvas.Instance.Toast.ShowTimedMessage(validationMessage);
            // TODO: unlock button and text input
        }

    }
}