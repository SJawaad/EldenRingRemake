using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{   
    public class PlayerManager : CharacterManager
    {
        PlayerLocomotionManager playerLocomotionManager;

        protected override void Awake()
        {
            base.Awake();

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
        }

        protected override void Update()
        {
            base.Update();

            // DO NOT WANT TO CONTROL OR EDIT OTHER PLAYERS' CHARACTERS
            if (!IsOwner)
                return;

            // HANDLE ALL PLAYER MOVEMENT
            playerLocomotionManager.HandleAllMovement();
        }
    }
}
