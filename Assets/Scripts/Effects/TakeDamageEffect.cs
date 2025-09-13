using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace JM
{
    public class TakeDamageEffect : InstantCharacterEffect
    {
        [Header("Character Causing Damage")]
        public CharacterManager characterCausingDamage; // THIS WILL STORE THE CHARACTER THAT CAUSED THE DAMAGE

        [Header("Damage")]
        public float physicalDamage = 0; // THIS WILL BE SLIT INTO SUB TYPES "STANDARD", "STRIKE", "SLASH", "PIERCE"
        public float magicDamage = 0;
        public float fireDamage = 0;
        public float lightningDamage = 0;
        public float holyDamage = 0;

        [Header("Final Damage")]
        private float finalDamageDealt = 0;

        [Header("Poise")]
        public float poiseDamage = 0;
        public bool isPoiseBroken = false;

        [Header("Animation")]
        public bool plsyDamageAnimation = true;
        public bool manuallySelectDamageAnimation = false;
        public string damageAnimation;

        [Header("Sound FX")]
        public bool willPlayDamageFX = true;
        public AudioClip elementalDamageSoundFX;

        [Header("Direction Damage Taken")]
        public float angleHitFrom = 0;
        public Vector3 contactPoint;

        public override void ProcessEffect(CharacterManager character)
        {
            base.ProcessEffect(character);

            if (character.isDead.Value)
                return;
        }

        private void CalculateDamage(CharacterManager character)
        {
            if (characterCausingDamage != null)
            {

            }

            finalDamageDealt = physicalDamage + magicDamage + fireDamage + lightningDamage + holyDamage;
        }
    }
}