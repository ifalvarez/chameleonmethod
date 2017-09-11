using System.Collections;
using UnityEngine;

public class BushSnake : MonoBehaviour
{
    public delegate void BushSnakeEventsDelegate();
    public event BushSnakeEventsDelegate OnTimeOver; //game over for the player

    public float attackTime = 0.0f;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "SnakeTrap")
        {
            attack = SnakeAttack();
            StartCoroutine(attack);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StopAllCoroutines();
        Debug.Log("Saved");
    }

    IEnumerator attack;
    IEnumerator SnakeAttack()
    {
        yield return new WaitForSeconds(attackTime);
        Debug.Log("I ate the chameleon");
    }

}
