using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

namespace JM
{
    public class TitleScreenManager : MonoBehaviour
    {
        public static TitleScreenManager instance;

        [Header("Menus")]
        [SerializeField] GameObject titleScreenMainMenu;
        [SerializeField] GameObject titleScreenLoadCharacterMenu;

        [Header("Buttons")]
        [SerializeField] Button mainMenuNewGameButton;
        [SerializeField] Button mainMenuLoadGameButton;
        [SerializeField] Button loadMenuReturnButton;
        [SerializeField] Button noCharacterSlotsOkayButton;
        [SerializeField] Button deleteCharacterSlotConfirmButton;

        [Header("Pop Ups")]
        [SerializeField] GameObject noCharacterSlotsPopUp;
        [SerializeField] GameObject deleteCharacterSlotPopUp;


        [Header("Character Slots")]
        public CharacterSlot currentSelectedSlot = CharacterSlot.NO_SLOT;
        [SerializeField] CharacterSlot slotToBeDeleted = CharacterSlot.NO_SLOT;

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

        public void StartNetworkAsHost()
        {
            NetworkManager.Singleton.StartHost();
        }

        public void StartNewGame()
        {
            WorldSaveGameManager.instance.AttemptToCreateNewGame();
        }

        public void OpenLoadGameMenu()
        {
            // CLOSE MAIN MENU AND OPEN LOAD CHARACTER MENU
            titleScreenMainMenu.SetActive(false);
            titleScreenLoadCharacterMenu.SetActive(true);

            loadMenuReturnButton.Select();
        }

        public void CloseLoadGameMenu()
        {
            titleScreenLoadCharacterMenu.SetActive(false);
            titleScreenMainMenu.SetActive(true);

            mainMenuLoadGameButton.Select();
        }

        public void DisplayNoFreeCharacterSlotsPopUp()
        {
            noCharacterSlotsPopUp.SetActive(true);
            noCharacterSlotsOkayButton.Select();
        }

        public void CloseNoFreeCharacterSlotsPopUp()
        {
            noCharacterSlotsPopUp.SetActive(false);
            mainMenuNewGameButton.Select();
        }

        public void SelectCharacterSlot(CharacterSlot slot)
        {
            currentSelectedSlot = slot;
            slotToBeDeleted = slot;
        }

        public void SelectNoSlot()
        {
            currentSelectedSlot = CharacterSlot.NO_SLOT;
        }

        public void ResetDeleteSlot()
        {
            slotToBeDeleted = CharacterSlot.NO_SLOT;
        }

        public void AttemptToDeleteCharacterSlot()
        {
            if (currentSelectedSlot != CharacterSlot.NO_SLOT && (currentSelectedSlot == slotToBeDeleted))
            {
                deleteCharacterSlotPopUp.SetActive(true);
                deleteCharacterSlotConfirmButton.Select();
            }
        }

        public void DeleteCharacterSlot()
        {
            // CLOSE POP UP AND DELETE CHARACTER SLOT
            deleteCharacterSlotPopUp.SetActive(false);
            WorldSaveGameManager.instance.DeleteGame(slotToBeDeleted);

            // RELOAD LOAD MENU
            titleScreenLoadCharacterMenu.SetActive(false);
            titleScreenLoadCharacterMenu.SetActive(true);

            // SELECT RETURN BUTTON AND RESET SLOT TO BE DELETED
            loadMenuReturnButton.Select();
            slotToBeDeleted = CharacterSlot.NO_SLOT;
        }

        public void CloseDeleteCharacterSlotPopUp()
        {
            deleteCharacterSlotPopUp.SetActive(false);
            loadMenuReturnButton.Select();
        }
    }
}
