using UnityEngine;

namespace Hints
{
    public class HintViewer : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetMouseButton(0))
            {
                gameObject.SetActive(false);
            }
        }
    }
}