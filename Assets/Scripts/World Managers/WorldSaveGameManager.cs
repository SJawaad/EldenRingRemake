using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JM
{
	public class WorldSaveGameManager : MonoBehaviour
	{
		public static WorldSaveGameManager instance;

        public PlayerManager player;

        [Header("Save/Load")]
        [SerializeField] bool saveGame;
        [SerializeField] bool loadGame;

        [Header("World Scene Index")]
        [SerializeField] int worldSceneIndex = 1;

        [Header("Save Data Writer")]
        private SaveFileDataWriter saveFileDataWriter;

        [Header("Current Character Data")]
        public CharacterSlot currentCharacterSlotBeingUsed;
        public CharacterSaveData currentCharacterData;
        private string saveFileName;

        [Header("Character Slots")]
        public CharacterSaveData[] characterSlots = new CharacterSaveData[10];

        private void Awake()
        {
            if(instance == null)
            {
                // THERE CAN ONLY BE ONE!
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

            LoadAllCharacterSlots();
        }

        private void Update()
        {
            if (saveGame)
            {
                saveGame = false;
                SaveGame();
            }

            if (loadGame)
            {
                loadGame = false;
                LoadGame();
            }
        }

        public string DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(CharacterSlot characterSlot)
        {
            return $"characterSlot_{((int)characterSlot + 1):D2}";
        }

        public void AttemptToCreateNewGame()
        {
            saveFileDataWriter = new SaveFileDataWriter
            {
                saveDataDirectoryPath = Application.persistentDataPath
            };

            foreach (var slot in Enum.GetValues(typeof(CharacterSlot)).Cast<CharacterSlot>())
            {
                // SKIP NO_SLOT
                if (slot == CharacterSlot.NO_SLOT)
                    continue;

                // SET FILE NAME
                saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(slot);

                // CHECK FILE EXISTS
                if (saveFileDataWriter.CheckToSeeIfFileExists())
                    continue;

                // SET SAVE
                currentCharacterSlotBeingUsed = slot;
                currentCharacterData = new CharacterSaveData();

                StartCoroutine(LoadWorldScene());
                return;
            }

            // NO FREE SLOTS AVAILABLE
            TitleScreenManager.instance.DisplayNoFreeCharacterSlotsPopUp();
        }

        public void LoadGame()
        {
            // LOAD FILE WITH FILE NAME BASED ON WHICH SLOT WE ARE USING
            saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

            saveFileDataWriter = new SaveFileDataWriter();

            // GENERAL DATA PATH WHICH GENERALLY WORKS ON MOST MACHINE TYPES
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;
            currentCharacterData = saveFileDataWriter.LoadSaveFile();

            StartCoroutine(LoadWorldScene());
        }

        public void SaveGame()
        {
            saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(currentCharacterSlotBeingUsed);

            saveFileDataWriter = new SaveFileDataWriter();

            // GENERAL DATA PATH WHICH GENERALLY WORKS ON MOST MACHINE TYPES
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = saveFileName;

            // PASS PLAYER INFO FROM GAME TO SAVE FILE
            player.SaveGameDataToCurrentCharacterData(ref currentCharacterData);

            // WRITE PLAYER INFO TO JSON FILE
            saveFileDataWriter.CreateNewCharacterSaveFile(currentCharacterData);
        }

        public void DeleteGame(CharacterSlot slot)
        {
            // CHOOSE FILE WITH FILE NAME BASED ON WHICH SLOT WE ARE USING
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;
            saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(slot);

            saveFileDataWriter.DeleteSaveFile();
        }

        // LOAD ALL CHARACTER PROFILES ON DEVICE WHEN STARTING GAME
        private void LoadAllCharacterSlots()
        {
            saveFileDataWriter = new SaveFileDataWriter();
            saveFileDataWriter.saveDataDirectoryPath = Application.persistentDataPath;

            for (int i = 0; i < 10; i++)
            {
                var slot = (CharacterSlot)i;
                saveFileDataWriter.saveFileName = DecideCharacterFileNameBasedOnCharacterSlotBeingUsed(slot);
                characterSlots[i] = saveFileDataWriter.LoadSaveFile();
            }
        }

        public IEnumerator LoadWorldScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(worldSceneIndex);

            player.LoadGameDataFromCurrentCharacterData(ref currentCharacterData);

            yield return null;
        }

        public int GetWorldSceneIndex()
        {
            return worldSceneIndex;
        }
    }
}
