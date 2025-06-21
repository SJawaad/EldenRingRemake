using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace JM
{
	public class PlayerUIManager : MonoBehaviour
	{
        public static PlayerUIManager instance;

        [Header("NETWORK JOIN")]
		[SerializeField] bool startGameAsClient;

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

        private void Update()
        {
            if(startGameAsClient)
            {
                startGameAsClient = false;
                // MUST FIRST SHUT DOWN NETWORK AS WE HAVE STARTED AS HOST AND WANT TO START AS CLIENT
                NetworkManager.Singleton.Shutdown();
                // START AS CLIENT
                NetworkManager.Singleton.StartClient();
            }
        }
    }
}
