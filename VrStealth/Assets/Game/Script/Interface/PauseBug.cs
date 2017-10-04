using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBug : MonoBehaviour {

    [SerializeField] private string tongueTag;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject player;

    private BoxCollider coll;
    private MeshRenderer mesh;
    //private bool canvasIsActive;
    //private bool isInteractable;
    private Vector3 bugPosition;

    void Awake()
    {
        SceneManager.sceneLoaded += LoadPlayer;
    }

    private void Start()
    {
        pauseCanvas.SetActive(false);
        coll = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshRenderer>();        
    }

    private void LoadPlayer(Scene arg0, LoadSceneMode arg1)
    {
        if(arg0.buildIndex != 0 && arg0.buildIndex != 1)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));    
            player = GameObject.Find("Player");        
            bugPosition = new Vector3(player.transform.position.x + 5, player.transform.position.y + 5, player.transform.position.z + 5);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.transform.tag == tongueTag)
        {
            print("pause");
            Pause();            
        }
    }

    void Update()
    {
        transform.position = bugPosition;
    }

    public void PauseOff()
    {
        coll.enabled = true;
        mesh.enabled = true;
        pauseCanvas.SetActive(!pauseCanvas.activeInHierarchy);
    }

    public void Pause()
    {
        coll.enabled = false;
        mesh.enabled = false;
        pauseCanvas.transform.position = transform.position;
        pauseCanvas.transform.forward = (player.transform.position - transform.position).normalized * -1;
        //isInteractable = false;
        //canvasIsActive = pauseCanvas.activeInHierarchy;
        pauseCanvas.SetActive(!pauseCanvas.activeInHierarchy);
    }

}
