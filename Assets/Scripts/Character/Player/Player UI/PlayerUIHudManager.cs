using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class PlayerUIHudManager : MonoBehaviour
    {
        [SerializeField] UIStatBar healthBar;
        [SerializeField] UIStatBar staminaBar;

        public void RefreshHUD()
        {
            healthBar.gameObject.SetActive(false);
            healthBar.gameObject.SetActive(true);

            staminaBar.gameObject.SetActive(false);
            staminaBar.gameObject.SetActive(true);
        }

        public void SetNewHealthValue(float oldValue, float newValue)
        {
            healthBar.SetStat(newValue);
        }

        public void SetMaxHealthValue(float maxValue)
        {
            healthBar.SetMaxStat(maxValue);
        }

        public void SetNewStaminaValue(float oldValue, float newValue)
        {
            staminaBar.SetStat(newValue);
        }

        public void SetMaxStaminaValue(float maxValue)
        {
            staminaBar.SetMaxStat(maxValue);
        }
    }
}