using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class PlayerStatsManager : CharacterStatsManager
    {
        public void CalculateTotalStaminaBasedOnEnduranceLevel(int endurance)
        {
            float stamina = 0;

            // APPLY EQUATION TO CALCULATE TOTAL STAMINA BASED ON ENDURANCE LEVEL
            // USING ELDFEN RING FORMULA

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

            stamina = Mathf.Floor(stamina);
        }
    }
}