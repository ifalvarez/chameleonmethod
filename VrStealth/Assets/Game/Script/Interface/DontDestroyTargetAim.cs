using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyTargetAim : MonoBehaviour {

    private DontDestroyTargetAim instance;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
            DestroyImmediate(gameObject);

        DontDestroyOnLoad(this.gameObject);            
    }
}
