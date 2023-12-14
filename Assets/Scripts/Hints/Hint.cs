using UnityEngine;

public abstract class Hint : MonoBehaviour
{
    [SerializeField] private HintViewer _hintViewer;

    public void Open()
    {
        if (PlayerPrefs.HasKey(PlayerPrefsKeys.LevelNumber)&& PlayerPrefs.GetInt(PlayerPrefsKeys.LevelNumber)==1)
        {
            _hintViewer.gameObject.SetActive(true);
        }
    }
}
