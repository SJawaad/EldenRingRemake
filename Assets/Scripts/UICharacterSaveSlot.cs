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
                    saveFileDataWriter.saveFileName = "characterSlot_02";
                    break;
                case CharacterSlot.CharacterSlot_03:
                    saveFileDataWriter.saveFileName = "characterSlot_03";
                    break;
                case CharacterSlot.CharacterSlot_04:
                    saveFileDataWriter.saveFileName = "characterSlot_04";
                    break;
                case CharacterSlot.CharacterSlot_05:
                    saveFileDataWriter.saveFileName = "characterSlot_05";
                    break;
                case CharacterSlot.CharacterSlot_06:
                    saveFileDataWriter.saveFileName = "characterSlot_06";
                    break;
                case CharacterSlot.CharacterSlot_07:
                    saveFileDataWriter.saveFileName = "characterSlot_07";
                    break;
                case CharacterSlot.CharacterSlot_08:
                    saveFileDataWriter.saveFileName = "characterSlot_08";
                    break;
                case CharacterSlot.CharacterSlot_09:
                    saveFileDataWriter.saveFileName = "characterSlot_09";
                    break;
                case CharacterSlot.CharacterSlot_10:
                    saveFileDataWriter.saveFileName = "characterSlot_10";
                    break;
            }
        }
    }
}