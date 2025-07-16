using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace JM
{
    public class CharacterNetworkManager : NetworkBehaviour
    {
        CharacterManager character;

        [Header("Position")]
        public NetworkVariable<Vector3> networkPosition = new NetworkVariable<Vector3>(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<Quaternion> networkRotation = new NetworkVariable<Quaternion>(Quaternion.identity, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public Vector3 networkVelocity;
        public float networkPositionSmoothTime = 0.1f;
        public float networkRotationSmoothTime = 0.1f;

        [Header("Animation")]
        public NetworkVariable<float> horizontalMovement = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> verticalMovement = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
        public NetworkVariable<float> moveAmount = new NetworkVariable<float>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        // SERVER RPC IS A FUNCTION CALLED FROM A CLIENT TO THE SERVER
        [ServerRpc]
        public void NotifyTheServerOfActionAnimationServerRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            // IF THIS CHARACTER IS SERVER/HOST, PLAY ANIMATION FOR ALL CLIENTS
            if (IsServer)
            {
                PlayActionAnimationForAllClientsClientRpc(clientID, animationID, applyRootMotion);
            }
        }

        // CLIENT RPC IS A FUNCTION SENT TO ALL CLIENTS
        [ClientRpc]
        public void PlayActionAnimationForAllClientsClientRpc(ulong clientID, string animationID, bool applyRootMotion)
        {
            if (clientID != NetworkManager.Singleton.LocalClientId)
            {
                PerformActionAnimationFromServer(animationID, applyRootMotion);
            }
        }

        private void PerformActionAnimationFromServer(string animationID, bool applyRootMotion)
        {
            character.applyRootMotion = applyRootMotion;
            character.animator.CrossFade(animationID, 0.2f);
        }
    }
}