using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using ParrelSync;
using System;
using UnityEditor;
using Unity.Netcode.Transports.UTP;

namespace JM
{
	public class PlayerUIManager : MonoBehaviour
	{
        public static PlayerUIManager instance;

        [HideInInspector] public PlayerUIHudManager playerUIHudManager;

        [Header("NETWORK JOIN")]
		[SerializeField] bool startGameAsClient;

        int clientNumber;

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

            playerUIHudManager = GetComponentInChildren<PlayerUIHudManager>();
        }

        private void Start()
        {
            DontDestroyOnLoad(gameObject);
            ChangePortForCloneEditor();
        }

        private void Update()
        {
            if (startGameAsClient)
            {
                startGameAsClient = false;
                StartCoroutine(RestartAsClient());
            }
        }

        void ChangePortForCloneEditor()
        {
            if (!ClonesManager.IsClone())
                return;

            // convert the argument for this clone editor to the number of said editor; input checking exists below
            // to ensure that the argument is a number, and that it starts from index 1
            string clonesManagerArgument = ClonesManager.GetArgument();

            if (!Int32.TryParse(clonesManagerArgument, out clientNumber) || clientNumber < 1)
            {
                Debug.Log("Change the argument of the clone editor to the number of said editor; start from 1.");
                EditorApplication.isPlaying = false;
            }

            // finally, actually change the port
            UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
            transport.ConnectionData.Port += (ushort)clientNumber;
        }

        private IEnumerator RestartAsClient()
        {
            if (NetworkManager.Singleton.IsListening)
            {
                NetworkManager.Singleton.Shutdown();
                UnityTransport transport = NetworkManager.Singleton.GetComponent<UnityTransport>();
                transport.ConnectionData.Port -= (ushort)clientNumber;
            }

            yield return null;

            NetworkManager.Singleton.StartClient();
        }
    }
}
