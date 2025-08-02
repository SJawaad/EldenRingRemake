using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace JM
{
    public class SaveFileDataWriter
    {
        public string saveDataDirectoryPath = "";
        public string saveFileName = "";

        //CHECK IF CHARACTER SLOT EXISTS
        public bool CheckToSeeIfFileExists()
        {
            if (File.Exists(Path.Combine(saveDataDirectoryPath, saveFileName)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // USED TO DELETE A CHARACTER SAVE FILE
        public void DeleteSaveFile()
        {
            File.Delete(Path.Combine(saveDataDirectoryPath, saveFileName));
        }

        // USED TO CREATE A SAVE FILE FOR A CHARACTER WHEN STARTING NEW GAME OR EXISTING GAME
        public void CreateNewCharacterSaveFile(CharacterSaveData saveData)
        {
            // MAKE A PATH TO SAVE FILE
            string savePath = Path.Combine(saveDataDirectoryPath, saveFileName);

            try
            {
                // CREATE THE DIRECTORY IF IT DOESN'T EXIST
                Directory.CreateDirectory(Path.GetDirectoryName(savePath));
                Debug.Log("CREATING SAVE FILE AT DIRECTORY: " + savePath);

                // SERIALIZE THE DATA TO JSON
                string dataToStore = JsonUtility.ToJson(saveData, true);

                // WRITE FILE TO SYSTEM
                using (FileStream stream = new FileStream(savePath, FileMode.Create))
                {
                    using (StreamWriter fileWriter = new StreamWriter(stream))
                    {
                        fileWriter.Write(dataToStore);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.LogError("ERROR SAVING CHARACTER DATA: " + savePath + "\n" + e.Message);
            }
        }

        // USED TO LOAD A SAVE FILE WHEN LOADING PREVIOUS SAVE

        public CharacterSaveData LoadSaveFile()
        {
            CharacterSaveData characterData = null;

            // GET PATH TO SAVE FILE
            string loadPath = Path.Combine(saveDataDirectoryPath, saveFileName);

            if (File.Exists(loadPath))
            {
                try
                {
                    string dataToLoad = "";

                    using (FileStream stream = new FileStream(loadPath, FileMode.Open))
                    {
                        using (StreamReader fileReader = new StreamReader(stream))
                        {
                            dataToLoad = fileReader.ReadToEnd();
                        }
                    }

                    // DESERIALIZE THE DATA FROM JSON
                    characterData = JsonUtility.FromJson<CharacterSaveData>(dataToLoad);
                }
                catch (Exception e)
                {
                    Debug.LogError("ERROR LOADING CHARACTER DATA: " + loadPath + "\n" + e.Message);
                }
            }

            return characterData;
        }
    }
}