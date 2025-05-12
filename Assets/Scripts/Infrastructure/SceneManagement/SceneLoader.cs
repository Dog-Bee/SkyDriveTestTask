using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneManagement
{
    public class SceneLoader: MonoBehaviour
    {
        [SerializeField] private string sceneToLoad;

        private void Start()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad,LoadSceneMode.Additive);
            while(asyncLoad.isDone == false)
                yield return null;
            
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(sceneToLoad));
        }
    }
}