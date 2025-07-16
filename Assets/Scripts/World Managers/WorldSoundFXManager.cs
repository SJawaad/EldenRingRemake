using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class WorldSoundFXManager : MonoBehaviour
    {
        public static WorldSoundFXManager instance;

        [Header("Action Sounds")]
        public AudioClip rollFX;

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
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}