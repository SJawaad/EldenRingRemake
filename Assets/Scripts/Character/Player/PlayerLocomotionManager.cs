using UnityEngine;

namespace JM
{   
    public class PlayerLocomotionManager : CharacterLocomotionManager
    {
        PlayerManager player;

        //  VALUES TAKEN FROM THE INPUT MANAGER
        [HideInInspector] public float verticalMovement;
        [HideInInspector] public float horizontalMovement;
        [HideInInspector] public float moveAmount;

        [Header("Movement Settings")]
        private Vector3 movementDirection;
        private Vector3 targetRotationDirection;
        [SerializeField] float walkingSpeed = 2f;
        [SerializeField] float runningSpeed = 5f;
        [SerializeField] float rotationSpeed = 15f;

        [Header("Dodge Settings")]
        private Vector3 rollDirection;

        protected override void Awake()
        {
            base.Awake();

            player = GetComponent<PlayerManager>();
        }

        protected override void Update()
        {
            base.Update();

            if (player.IsOwner)
            {
                player.characterNetworkManager.verticalMovement.Value = verticalMovement;
                player.characterNetworkManager.horizontalMovement.Value = horizontalMovement;
                player.characterNetworkManager.moveAmount.Value = moveAmount;
            }
            else
            {
                verticalMovement = player.characterNetworkManager.verticalMovement.Value;
                horizontalMovement = player.characterNetworkManager.horizontalMovement.Value;
                moveAmount = player.characterNetworkManager.moveAmount.Value;

                player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);
            }
        }

        public void HandleAllMovement()
        {
            HandleGroundedMovement();
            HandleRotation();
        }

        private void GetMovementValues()
        {
            verticalMovement = PlayerInputManager.instance.verticalInput;
            horizontalMovement = PlayerInputManager.instance.horizontalInput;
            moveAmount = PlayerInputManager.instance.moveAmount;
        }

        private void HandleGroundedMovement()
        {
            GetMovementValues();

            if (!player.canMove)
                return;

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

        private void HandleRotation()
        {
            if (!player.canRotate)
                return;

            targetRotationDirection = Vector3.zero;
            targetRotationDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
            targetRotationDirection += PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;
            targetRotationDirection.Normalize();
            targetRotationDirection.y = 0;

            if (targetRotationDirection == Vector3.zero)
            {
                targetRotationDirection = transform.forward;
            }

            Quaternion newRotation = Quaternion.LookRotation(targetRotationDirection);
            Quaternion targetRotation = Quaternion.Slerp(transform.rotation, newRotation, rotationSpeed * Time.deltaTime);
            transform.rotation = targetRotation;
        }

        public void AttemptToPerformDodge()
        {
            if (player.isPerformingAction)
                return;

            // IF PLAYER IS MOVING, PERFORM ROLL IN CAMERA DIRECION
            if (moveAmount > 0)
            {
                rollDirection = PlayerCamera.instance.cameraObject.transform.forward * verticalMovement;
                rollDirection += PlayerCamera.instance.cameraObject.transform.right * horizontalMovement;

                rollDirection.y = 0;
                rollDirection.Normalize();

                Quaternion playerRotation = Quaternion.LookRotation(rollDirection);
                player.transform.rotation = playerRotation;

                player.playerAnimatorManager.PlayTargetActionAnimation("Roll_Forward_01", true);
            }
            // IF PLAYER IS NOT MOVING, PERFORM A BACKSTEP
            else
            {
                player.playerAnimatorManager.PlayTargetActionAnimation("Back_Step_01", true);
            }
        }
    }
}
