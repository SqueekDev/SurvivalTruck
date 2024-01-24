using System.Collections;
using Level;
using UnityEngine;

namespace UI
{
    public class FinishPanelEnabler : MonoBehaviour
    {
        private const float FinishLevelDelayTime = 1.5f;

        [SerializeField] private Wave _wave;
        [SerializeField] private GamePanel _finishLevelPanel;

        private Coroutine _finishCorutine;
        private WaitForSeconds _finishLevelDelay = new WaitForSeconds(FinishLevelDelayTime);

        private void OnEnable()
        {
            _wave.Ended += OnWaveEnded;
        }

        private void OnDisable()
        {
            _wave.Ended -= OnWaveEnded;
        }

        private IEnumerator FinishLevelCorutine()
        {
            yield return _finishLevelDelay;
            _finishLevelPanel.gameObject.SetActive(true);
        }

        private void OnWaveEnded()
        {
            if (_finishCorutine != null)
            {
                StopCoroutine(_finishCorutine);
            }

            _finishCorutine = StartCoroutine(FinishLevelCorutine());
        }
    }
}