using FishNet.Connection;
using FishNet.Object;

namespace Client
{
    public class ClientInstance : NetworkBehaviour
    {
        #region Public Statics
        public static ClientInstance Instance;
        #endregion

        #region Constants
        private const string VersionCode = "0.0.2";
        #endregion

        #region Properties
        public bool Initialized
        {
            get;
            private set;
        }
        #endregion

        #region Private Methods
        [ServerRpc]
        private void CmdVerifyVersion(string versionToCheck)
        {
            Initialized = versionToCheck == VersionCode;
            TargetVerifyVersion(conn: Owner, verified: Initialized);
        }

        [TargetRpc]
        private void TargetVerifyVersion(NetworkConnection conn, bool verified)
        {
            Initialized = verified;
            if (verified)
            {
                return;
            }

            // Show feedback to the user that the version does not match.
            // The server will not allow ClientInstances that have not .Initialized to execute.
            NetworkManager.ClientManager.StopConnection();
            MessagesCanvas.Instance.FullScreenAlert.ShowTimedMessage("Your executable is out of date. Please update to the latest version to proceed.");
        }
        #endregion

        #region FishNet Callbacks
        public override void OnOwnershipClient(NetworkConnection prevOwner)
        {
            base.OnOwnershipClient(prevOwner);
            if (IsOwner)
            {
                Instance = this;
            }
        }

        public override void OnStartClient()
        {
            base.OnStartClient();
            if (IsOwner)
            {
                CmdVerifyVersion(VersionCode);
            }
        }
        #endregion
    }
}
