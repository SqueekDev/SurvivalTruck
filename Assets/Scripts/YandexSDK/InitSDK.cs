using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Agava.YandexGames;
using Base;

namespace YandexSDK
{
    public class InitSDK : MonoBehaviour
    {
        private void Awake()
        {
            YandexGamesSdk.CallbackLogging = true;
        }

        private IEnumerator Start()
        {
            yield return YandexGamesSdk.Initialize(OnInitialized);
        }

        private void OnInitialized()
        {
            SceneManager.LoadScene(GlobalValues.MainMenuSceneNumber);
        }
    }
}