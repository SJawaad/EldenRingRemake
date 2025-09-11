using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class PlayerEffectsManager : CharacterEffectsManager
    {
        [Header("DELETE LATER")]
        [SerializeField] InstantCharacterEffect testEffect;
        [SerializeField] bool applyTestEffect = false;

        private void Update()
        {
            if (applyTestEffect)
            {
                applyTestEffect = false;
                InstantCharacterEffect effect = Instantiate(testEffect);
                ProcessInstantCharacterEffects(testEffect);
            }
        }
    }
}