using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{   
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;

        //  VALUES TAKEN FROM THE INPUT MANAGER
        public float verticalMovement;
        public float horizontalMovement;
        public float moveAmount;

        private Vector3 movementDirection;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        public void HandleAllMovement()
        {

        }

        private void HandleGroundedMovement()
        {

        }
    }
}
