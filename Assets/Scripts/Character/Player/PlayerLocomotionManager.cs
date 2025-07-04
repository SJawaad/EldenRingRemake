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

        [SerializeField] float walkingSpeed = 2f;
        [SerializeField] float runningSpeed = 5f;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        public void HandleAllMovement()
        {
            HandleGroundedMovement();
        }

        private void GetVerticalAndHorizontalInputs()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;
        }

        private void HandleGroundedMovement()
        {
            GetVerticalAndHorizontalInputs();

            // MOVEMENT DIRECTION IS BASED ON THE CAMERA'S PERSPECTIVE & OUR INPUT
            movementDirection = PlayerCamera.instance.transform.forward * verticalMovement;
            movementDirection += PlayerCamera.instance.transform.right * horizontalMovement;
            movementDirection.Normalize();
            movementDirection.y = 0;

            if (PlayerInputManager.instance.moveAmount > 0.5f)
            {
                // MOVE AT RUNNING SPEED
                player.characterController.Move(movementDirection * runningSpeed * Time.deltaTime);
            }
            else if(PlayerInputManager.instance.moveAmount <= 0.5f)
            {
                // MOVE AT WALKING SPEED
                player.characterController.Move(movementDirection * walkingSpeed * Time.deltaTime);
            }
        }
    }
}
