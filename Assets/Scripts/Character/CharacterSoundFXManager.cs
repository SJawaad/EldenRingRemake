using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JM
{
    public class CharacterSoundFXManager : MonoBehaviour
    {
        private AudioSource AudioSource;

        protected virtual void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        public void PlayRollSoundFX()
        {
            AudioSource.PlayOneShot(WorldSoundFXManager.instance.rollFX);
        }
    }
}