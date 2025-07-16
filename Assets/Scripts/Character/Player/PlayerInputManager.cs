using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JM
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;
        public PlayerManager player;

        PlayerControls playerControls;

        [Header("Camera Movement Input")]
        [SerializeField] Vector2 cameraMovementInput;
        public float cameraVerticalInput;
        public float cameraHorizontalInput;

        [Header("Player Movement Input")]
        [SerializeField] Vector2 playerMovementInput;
        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;

        [Header("Player Action Input")]
        [SerializeField] bool dodgeInput = false;
        [SerializeField] bool sprintInput = false;

        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);

            // WHEN SCENE CHANGES, RUN THIS LOGIC
            SceneManager.activeSceneChanged += OnSceneChange;

            instance.enabled = false; // DISABLE THE INPUT MANAGER BY DEFAULT

        }

        private void OnEnable()
        {
            if(playerControls == null)
            {
                playerControls = new PlayerControls();

                // MOVEMENT
                playerControls.PlayerMovement.Movement.performed += i => playerMovementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Controller.performed += i => cameraMovementInput = i.ReadValue<Vector2>();
                playerControls.PlayerCamera.Mouse.performed += i => cameraMovementInput = i.ReadValue<Vector2>();

                // ACTIONS
                playerControls.PlayerActions.Dodge.performed += i => dodgeInput = true;
                playerControls.PlayerActions.Sprint.performed += i => sprintInput = true;
                playerControls.PlayerActions.Sprint.canceled += i => sprintInput = false;
            }

            playerControls.Enable();
        }

        private void OnDestroy()
        {
            // WHEN OBJECT DESTORYED, UNSUBSCRIBE FROM THE SCENE CHANGE EVENT
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        // PREVENTS INPUTS BEING READ WHEN GAME IS MINIMISED
        private void OnApplicationFocus(bool focus)
        {
            if (enabled)
            {
                if (focus)
                {
                    playerControls.Enable();
                }
                else
                {
                    playerControls?.Disable();
                }
            }
        }

        private void OnSceneChange(Scene current, Scene next)
        {
            // IF WE ARE LOADING INTO THE WORLD SCENE, ENABLE THE INPUT MANAGER
            if (next.buildIndex == WorldSaveGameManager.instance.GetWorldSceneIndex())
            {
                instance.enabled = true;
            }
            // IF WE ARE NOT IN THE WORLD SCENE, DISABLE THE INPUT MANAGER
            else
            {
                instance.enabled = false;
            }
        }

        private void Update()
        {
            HandleAllInput();
        }

        private void HandleAllInput()
        {
            // MOVEMENT
            HandlePlayerMovementInput();
            HandleCameraMovementInput();

            // ACTIONS
            HandleDodgeInput();
            HandleSprintInput();
        }

        //------------------------------MOVEMENT---------------------------------//

        private void HandlePlayerMovementInput()
        {
            verticalInput = playerMovementInput.y;
            horizontalInput = playerMovementInput.x;

            // RETURNS ABSOLUTE VALUE OF THE INPUTS
            moveAmount = Mathf.Clamp01(Mathf.Abs(verticalInput) + Mathf.Abs(horizontalInput));

            // ENSURE MOVE AMOUNT IS WITHIN A SPECIFIC RANGE TO PREVENT JITTERY MOVEMENT (OPTIONAL)
            if (moveAmount <= 0.5f && moveAmount > 0f)
            {
                moveAmount = 0.5f;
            }
            else if (moveAmount > 0.5f && moveAmount <= 1f)
            {
                moveAmount = 1f;
            }

            if (player == null)
                return;

            // PASS 0 IN HORIZONTAL AS WE ARE NOT LOCKED ON SO THERE IS NO STRAFING (ONLY WALK BACK/FORWARD AND RUN BACK/FORWARD)
            // ONLY USE HORIZONTAL WHEN WE ARE LOCKED ON
            player.playerAnimatorManager.UpdateAnimatorMovementParameters(0, moveAmount);
        }

        private void HandleCameraMovementInput()
        {
            cameraVerticalInput = cameraMovementInput.y;
            cameraHorizontalInput = cameraMovementInput.x;
        }

        //------------------------------ACTIONS---------------------------------//

        private void HandleDodgeInput()
        {
            if (dodgeInput)
            {
                dodgeInput = false;
                player.playerLocomotionManager.AttemptToPerformDodge();
            }
        }

        private void HandleSprintInput()
        {
            if (sprintInput)
            {
                player.playerLocomotionManager.HandleSprinting();
            }
        }
    }
}
