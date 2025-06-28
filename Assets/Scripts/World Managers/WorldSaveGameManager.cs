using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace JM
{
	public class WorldSaveGameManager : MonoBehaviour
	{
		public static WorldSaveGameManager instance;

        [SerializeField]
        int worldSceneIndex = 1;

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
        }

        public IEnumerator LoadNewGame()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(worldSceneIndex);

            yield return null;
        }

        public int GetWorldSceneIndex()
        {
            return worldSceneIndex;
        }
    }
}
