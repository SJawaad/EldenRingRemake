using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class CharacterEffectsManager : MonoBehaviour
    {
        CharacterManager character;

        protected virtual void Awake()
        {
            character = GetComponent<CharacterManager>();
        }

        public void ProcessInstantCharacterEffects(InstantCharacterEffect effect)
        {
            effect.ProcessEffect(character);
        }
    }
}