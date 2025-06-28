using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace JM
{
	public class CharacterManager : MonoBehaviour
	{
        protected virtual void Awake()
        {
            DontDestroyOnLoad(this);
        }

        protected virtual void Update()
        {

        }
    }
}
