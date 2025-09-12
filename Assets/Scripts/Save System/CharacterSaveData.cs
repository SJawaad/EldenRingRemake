using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    [System.Serializable]
    public class CharacterSaveData
    {
        [Header("Character Name")]
        public string characterName = "Character";

        [Header("Attributes")]
        public int vigor;
        public int endurance;

        [Header("Stats")]
        public float currentHealth;
        public float currentStamina;

        [Header("Time Played")]
        public float secondsPlayed;

        // WHEN SERIALIZING DATA, CAN ONLU USE PRIMITIVE TYPES SO CANT STORE A VECTOR3
        [Header("World Position")]
        public float xPosition;
        public float yPosition;
        public float zPosition;
    }
}