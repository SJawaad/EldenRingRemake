using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class WorldCharacterEffectsManager : MonoBehaviour
    {
        public static WorldCharacterEffectsManager instance;

        [SerializeField] List<InstantCharacterEffect> instantEffects = new List<InstantCharacterEffect>();

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            GenerateEffectID();
        }

        private void GenerateEffectID()
        {
            for (int i = 0; i < instantEffects.Count; i++)
            {
                instantEffects[i].instantEffectID = i;
            }
        }
    }
}