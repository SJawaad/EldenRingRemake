using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class PlayerUIHudManager : MonoBehaviour
    {
        [SerializeField] UIStatBar healthBar;
        [SerializeField] UIStatBar staminaBar;

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