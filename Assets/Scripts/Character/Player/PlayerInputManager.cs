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

        private void Awake()
        {
            //WHEN SCENE CHANGES, RUN THIS LOGIC
            SceneManager.activeSceneChanged += OnSceneChange;
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
            //WHEN OBJECT DESTORYED, UNSUBSCRIBE FROM THE SCENE CHANGE EVENT
            SceneManager.activeSceneChanged -= OnSceneChange;
        }

        private void OnSceneChange(Scene current, Scene next)
        {
            if (playerControls != null)
            {
                playerControls.Disable();
                playerControls = null;
            }
        }
    }
}
