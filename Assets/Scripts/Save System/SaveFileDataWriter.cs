using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace JM
{
    public class SaveFileDataWriter : MonoBehaviour
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
    }
}