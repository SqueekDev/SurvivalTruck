using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private GamePanel _startPanel;
    [SerializeField] private GameButton _startButton;

    private void OnEnable()
    {
        _startButton.Clicked += StartLevel;
    }

    private void Start()
    {
        _startPanel.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _startButton.Clicked -= StartLevel;
    }

    private void StartLevel()
    {
        SceneManager.LoadScene(SceneNumbers.GameSceneNumber);
    }
}
