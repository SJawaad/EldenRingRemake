using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JM
{
    public class PlayerInputManager : MonoBehaviour
    {
        public static PlayerInputManager instance;

        PlayerControls playerControls;

        [SerializeField] Vector2 movementInput;
        public float verticalInput;
        public float horizontalInput;
        public float moveAmount;

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

                playerControls.PlayerMovement.Movement.performed += i => movementInput = i.ReadValue<Vector2>();
            }

            playerControls.Enable();
        }

        private void OnDestroy()
        {
            // WHEN OBJECT DESTORYED, UNSUBSCRIBE FROM THE SCENE CHANGE EVENT
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void Update()
        {
            HandleMovementInput();
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

        private void HandleMovementInput()
        {
            verticalInput = movementInput.y;
            horizontalInput = movementInput.x;

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
        }
    }
}
