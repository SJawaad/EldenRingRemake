using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

namespace JM
{
    public class TitleScreenManager : MonoBehaviour
    {
        [SerializeField] GameObject titleScreenMainMenu;
        [SerializeField] GameObject titleScreenLoadCharacterMenu;

        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StartNewGame()
        {
            WorldSaveGameManager.instance.CreateNewGame();
            StartCoroutine(WorldSaveGameManager.instance.LoadWorldScene());
        }

        public void OpenLoadGameMenu()
        {
            // CLOSE MAIN MENU AND OPEN LOAD CHARACTER MENU
            titleScreenMainMenu.SetActive(false);
            titleScreenLoadCharacterMenu.SetActive(true);


        }
    }
}
