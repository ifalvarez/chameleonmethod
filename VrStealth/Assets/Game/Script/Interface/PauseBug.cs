using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBug : MonoBehaviour {

    [SerializeField] private string tongeTag;
    [SerializeField] private GameObject pauseCanvas;
    [SerializeField] private GameObject player;

    private BoxCollider coll;
    private MeshRenderer mesh;
    private bool canvasIsActive;
    private bool isInteractable;
    private Vector3 bugPosition;

    private void Start()
    {
        coll = GetComponent<BoxCollider>();
        mesh = GetComponent<MeshRenderer>();
        player = GameObject.Find("Player");
        bugPosition = new Vector3(player.transform.position.x + 5, player.transform.position.y + 5, player.transform.position.z + 5);
    }


    void OnTriggerEnter(Collider other)
    {
        Pause();
        if(other.transform.tag == tongeTag)
        {
            
        }
    }

    void Update()
    {
        transform.position = bugPosition;
        if(!isInteractable)
        {
            if(canvasIsActive == pauseCanvas.activeInHierarchy)
            {
                PauseOff();
                isInteractable = true;

            }
        }
    }

 /*   void OnMouseDown()
    {
        Pause();
    }*/

    private void PauseOff()
    {
        coll.enabled = true;
        mesh.enabled = true;
    }

    public void Pause()
    {
        coll.enabled = false;
        mesh.enabled = false;
        pauseCanvas.transform.position = transform.position;
        pauseCanvas.transform.forward = (player.transform.position - transform.position).normalized * -1;
        isInteractable = false;
        canvasIsActive = pauseCanvas.activeInHierarchy;
        pauseCanvas.SetActive(!pauseCanvas.activeInHierarchy);
    }

}
