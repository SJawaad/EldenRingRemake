using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class CharacterStatsManager : MonoBehaviour
    {
        CharacterManager character;

        [Header("Stamina Regen")]
        private float staminaRegenTimer = 0;
        private float staminaTickTimer = 0;
        [SerializeField] float regenDelay = 2f;
        [SerializeField] float staminaRegenAmount = 2f;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        protected virtual void Start()
        {

        }

        public int CalculateTotalHealthBasedOnVigorLevel(int vigor)
        {
            float health = 0;

            // APPLY EQUATION TO CALCULATE TOTAL STAMINA BASED ON ENDURANCE LEVEL
            // USING ELDEN RING FORMULA

            if (vigor <= 25)
            {
                health = 300 + 500 * Mathf.Pow(((vigor - 1) / 24f), 1.5f);
            }
            else if (vigor >= 26 && vigor <= 40)
            {
                health = 800 + 650 * Mathf.Pow(((vigor - 25) / 15f), 1.1f);
            }
            else if (vigor >= 41 && vigor <= 60)
            {
                health = 1450 + 450 * Mathf.Pow(1 - (1 - ((vigor - 40) / 20f)), 1.2f);
            }
            else
            {
                health = 1900 + 200 * Mathf.Pow(1 - (1 - ((vigor - 60) / 39f)), 1.2f);
            }

            return Mathf.RoundToInt(health);
        }

        public int CalculateTotalStaminaBasedOnEnduranceLevel(int endurance)
        {
            float stamina = 0;

            // APPLY EQUATION TO CALCULATE TOTAL STAMINA BASED ON ENDURANCE LEVEL
            // USING ELDEN RING FORMULA

            if (endurance <= 15)
            {
                stamina = 80 + 25 * ((endurance - 1) / 14f);
            }
            else if (endurance >= 16 && endurance <= 35)
            {
                stamina = 105 + 25 * ((endurance - 15) / 15f);
            }
            else if (endurance >= 36 && endurance <= 60)
            {
                stamina = 130 + 25 * ((endurance - 30) / 20f);
            }
            else
            {
                stamina = 155 + 15 * ((endurance - 50) / 49f);
            }

            return Mathf.RoundToInt(stamina);
        }

        public virtual void RegenerateStamina()
        {
            if (!character.IsOwner)
                return;

            if (character.characterNetworkManager.isSprinting.Value)
                return;

            if (character.isPerformingAction)
                return;

            staminaRegenTimer += Time.deltaTime;

            if (staminaRegenTimer >= regenDelay)
            {
                if (character.characterNetworkManager.currentStamina.Value < character.characterNetworkManager.maxStamina.Value)
                {
                    staminaTickTimer += Time.deltaTime;

                    if (staminaTickTimer >= 0.05)
                    {
                        staminaTickTimer = 0;
                        character.characterNetworkManager.currentStamina.Value += staminaRegenAmount;
                    }
                }
            }
        }

        public virtual void ResetStaminaRegenTimer(float oldValue, float newValue)
        {
            // ONLY RESET TIMER IF ACTION HAS BEEN PERFORMED
            if (newValue < oldValue)
            {
                staminaRegenTimer = 0;
            }
        }
    }
}