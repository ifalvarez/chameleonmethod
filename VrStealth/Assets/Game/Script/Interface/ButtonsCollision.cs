using UnityEngine;
using UnityEngine.UI;

public class ButtonsCollision : MonoBehaviour
{
    Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Tongue")
        {
            btn.onClick.Invoke();
        }
    }
}
