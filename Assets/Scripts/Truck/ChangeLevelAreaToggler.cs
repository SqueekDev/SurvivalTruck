using Level;
using UnityEngine;

namespace Truck
{
    public class ChangeLevelAreaToggler : MonoBehaviour
    {
        [SerializeField] private ChangeLevelArea _changeLevelArea;
        [SerializeField] private LevelChanger _levelChanger;

        private void OnEnable()
        {
            _levelChanger.Changed += OnLevelChanged;
            _levelChanger.Finished += OnLevelFinished;
        }

        private void Start()
        {
            Toggle(true);
        }

        private void OnDisable()
        {
            _levelChanger.Changed -= OnLevelChanged;
            _levelChanger.Finished -= OnLevelFinished;
        }

        private void Toggle(bool enabled)
        {
            _changeLevelArea.gameObject.SetActive(enabled);
        }

        private void OnLevelChanged(int levelNumber)
        {
            Toggle(false);
        }

        private void OnLevelFinished()
        {
            Toggle(true);
        }
    }
}