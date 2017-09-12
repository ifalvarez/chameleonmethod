using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsCollision : MonoBehaviour {

   [SerializeField]private GameObject pauseObject;

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Tongue")
        {
            print("continuar");
            GetComponent<Button>().onClick.Invoke();
        }
    }
}
