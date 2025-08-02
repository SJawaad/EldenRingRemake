using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{   
    public class PlayerManager : CharacterManager
    {
        [HideInInspector] public PlayerAnimatorManager playerAnimatorManager;
        [HideInInspector] public PlayerLocomotionManager playerLocomotionManager;
        [HideInInspector] public PlayerNetworkManager playerNetworkManager;
        [HideInInspector] public PlayerStatsManager playerStatsManager;

        protected override void Awake()
        {
            base.Awake();

            playerLocomotionManager = GetComponent<PlayerLocomotionManager>();
            playerAnimatorManager = GetComponent<PlayerAnimatorManager>();
            playerNetworkManager = GetComponent<PlayerNetworkManager>();
            playerStatsManager = GetComponent<PlayerStatsManager>();
        }

        protected override void Update()
        {
            base.Update();

            // DO NOT WANT TO CONTROL OR EDIT OTHER PLAYERS' CHARACTERS
            if (!IsOwner)
                return;

            // HANDLE ALL PLAYER MOVEMENT
            playerLocomotionManager.HandleAllMovement();

            // REGEN STAMINA IF PLAYER IS NOT SPRINTING
            playerStatsManager.RegenerateStamina();
        }

        protected override void LateUpdate()
        {
            if (!IsOwner)
                return;

            base.LateUpdate();

            PlayerCamera.instance.HandleAllCameraActions();
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            if (IsOwner)
            {
                PlayerCamera.instance.player = this;
                PlayerInputManager.instance.player = this;

                playerNetworkManager.currentStamina.OnValueChanged += PlayerUIManager.instance.playerUIHudManager.SetNewStaminaValue;
                playerNetworkManager.currentStamina.OnValueChanged += playerStatsManager.ResetStaminaRegenTimer;

                // THIS WILL BE MOVED WHEN SAVING AND LOADING IS ADDED
                playerNetworkManager.maxStamina.Value = playerStatsManager.CalculateTotalStaminaBasedOnEnduranceLevel(playerNetworkManager.endurance.Value);
                playerNetworkManager.currentStamina.Value = playerStatsManager.CalculateTotalStaminaBasedOnEnduranceLevel(playerNetworkManager.endurance.Value);
                PlayerUIManager.instance.playerUIHudManager.SetMaxStaminaValue(playerNetworkManager.maxStamina.Value);
            }
        }

        public void SaveGameDataToCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            currentCharacterData.characterName = playerNetworkManager.characterName.Value.ToString();
            currentCharacterData.xPosition = transform.position.x;
            currentCharacterData.yPosition = transform.position.y;
            currentCharacterData.zPosition = transform.position.z;
        }

        public void LoadGameDataFromCurrentCharacterData(ref CharacterSaveData currentCharacterData)
        {
            playerNetworkManager.characterName.Value = currentCharacterData.characterName;

            Vector3 myPosition = new Vector3(currentCharacterData.xPosition, currentCharacterData.yPosition, currentCharacterData.zPosition);
            transform.position = myPosition;
        }
    }
}
