using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace JM
{
    public class UICharacterSaveSlot : MonoBehaviour
    {
        SaveFileDataWriter saveFileDataWriter;

        [Header("Game Slot")]
        public CharacterSlot characterSlot;

        [Header("Character Data")]
        public TextMeshProUGUI characterName;
        public TextMeshProUGUI timePlayed;

        private void OnEnable()
        {
            LoadSaveSlots();
        }

        public void LoadSaveSlots()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

            switch (characterSlot)
            {
                case CharacterSlot.CharacterSlot_01:

                    saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    if (saveFileDataWriter.CheckToSeeIfFileExists())
                    {
                        characterName.text = WorldSaveGameManager.instance.characterSlots[0].characterName;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;

                case CharacterSlot.CharacterSlot_02:

                    saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    if (saveFileDataWriter.CheckToSeeIfFileExists())
                    {
                        characterName.text = WorldSaveGameManager.instance.characterSlots[1].characterName;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;

                case CharacterSlot.CharacterSlot_03:

                    saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    if (saveFileDataWriter.CheckToSeeIfFileExists())
                    {
                        characterName.text = WorldSaveGameManager.instance.characterSlots[2].characterName;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;

                case CharacterSlot.CharacterSlot_04:

                    saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    if (saveFileDataWriter.CheckToSeeIfFileExists())
                    {
                        characterName.text = WorldSaveGameManager.instance.characterSlots[3].characterName;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;

                case CharacterSlot.CharacterSlot_05:

                    saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    if (saveFileDataWriter.CheckToSeeIfFileExists())
                    {
                        characterName.text = WorldSaveGameManager.instance.characterSlots[4].characterName;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;

                case CharacterSlot.CharacterSlot_06:

                    saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    if (saveFileDataWriter.CheckToSeeIfFileExists())
                    {
                        characterName.text = WorldSaveGameManager.instance.characterSlots[5].characterName;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;

                case CharacterSlot.CharacterSlot_07:

                    saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    if (saveFileDataWriter.CheckToSeeIfFileExists())
                    {
                        characterName.text = WorldSaveGameManager.instance.characterSlots[6].characterName;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;

                case CharacterSlot.CharacterSlot_08:

                    saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    if (saveFileDataWriter.CheckToSeeIfFileExists())
                    {
                        characterName.text = WorldSaveGameManager.instance.characterSlots[7].characterName;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;

                case CharacterSlot.CharacterSlot_09:

                    saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    if (saveFileDataWriter.CheckToSeeIfFileExists())
                    {
                        characterName.text = WorldSaveGameManager.instance.characterSlots[8].characterName;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;

                case CharacterSlot.CharacterSlot_10:

                    saveFileDataWriter.saveFileName = WorldSaveGameManager.instance.DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(characterSlot);

                    if (saveFileDataWriter.CheckToSeeIfFileExists())
                    {
                        characterName.text = WorldSaveGameManager.instance.characterSlots[9].characterName;
                    }
                    else
                    {
                        gameObject.SetActive(false);
                    }
                    break;
            }
        }

        public void LoadGameFromCharacterSlot()
        {
            WorldSaveGameManager.instance.currentCharacterSlotBeingUsed = characterSlot;
            WorldSaveGameManager.instance.LoadGame();
        }

        public void SelectCurrentSlot()
        {
            TitleScreenManager.instance.SelectCharacterSlot(characterSlot);
        }
    }
}