using Level;
using UnityEngine;

namespace UI
{
    public class LevelViewEnabler : MonoBehaviour
    {
        [SerializeField] private LevelChanger _levelChanger;
        [SerializeField] private LevelWaveView _levelWaveView;

        private void OnEnable()
        {
            _levelChanger.NormalLevelStarted += OnNormalLevelStarted;
        }

        private void OnDisable()
        {
            _levelChanger.NormalLevelStarted -= OnNormalLevelStarted;
        }

        private void OnNormalLevelStarted()
        {
            _levelWaveView.gameObject.SetActive(true);
        }
    }
}