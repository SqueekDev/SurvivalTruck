using Base;
using UnityEngine;

namespace Hints
{
    public abstract class Hint : MonoBehaviour
    {
        private const int TutorialLevelNumber = 1;

        [SerializeField] private HintViewer _hintViewer;

        public void Open()
        {
            if (PlayerPrefs.HasKey(PlayerPrefsKeys.LevelNumber) && PlayerPrefs.GetInt(PlayerPrefsKeys.LevelNumber) == TutorialLevelNumber)
            {
                _hintViewer.gameObject.SetActive(true);
            }
        }
    }
}