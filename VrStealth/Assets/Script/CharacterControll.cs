using UnityEngine;

public class CharacterControll : MonoBehaviour
{
    public float characterSpeed = 0.0f;    
    
    private void Update()
    {        
        if(Input.GetKey(KeyCode.W))
        {
            transform.position += transform.forward * characterSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= transform.forward * characterSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * characterSpeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * characterSpeed * Time.deltaTime;
        }        
    }
}