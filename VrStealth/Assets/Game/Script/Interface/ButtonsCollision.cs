using UnityEngine;
using UnityEngine.UI;

public class ButtonsCollision : MonoBehaviour
{
    public ParticleSystem selectionParticles;
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
            if (selectionParticles.isPlaying == false)
            {
                selectionParticles.Play();
            }
        }
    }
}
