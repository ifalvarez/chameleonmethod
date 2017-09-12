using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsCollision : MonoBehaviour {
    

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Tongue")
        {
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
