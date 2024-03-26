using System;
using FishNet.Connection;
using FishNet.Object;

namespace Client
{
    /// <summary>
    /// Helper to alert other non-NetworkBehaviours (MonoBehaviours)
    /// that the client's GameObject has been instantiated.
    /// </summary>
    public class LocalPlayerAnnouncer : NetworkBehaviour
    {
        #region Events
        public static event Action<NetworkObject> OnLocalPlayerUpdated;
        #endregion

        #region FishNet Callbacks
        public override void OnOwnershipClient(NetworkConnection prevOwner)
        {
            base.OnOwnershipClient(prevOwner);
            if (IsOwner)
            {
                OnLocalPlayerUpdated?.Invoke(NetworkObject);
            }
        }
        #endregion

        #region Unity Callbacks
        private void OnEnable()
        {
            if (IsOwner)
            {
                OnLocalPlayerUpdated?.Invoke(NetworkObject);
            }
        }

        private void OnDisable()
        {
            if (IsOwner)
            {
                OnLocalPlayerUpdated?.Invoke(null);
            }
        }
        #endregion
    }
}
