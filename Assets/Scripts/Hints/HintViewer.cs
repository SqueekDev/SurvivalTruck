using UnityEngine;

public class HintViewer : MonoBehaviour
{
    private void Update()
    {
        if (UnityEngine.Input.GetMouseButton(0))
        {
            gameObject.SetActive(false);
        }
    }
}
