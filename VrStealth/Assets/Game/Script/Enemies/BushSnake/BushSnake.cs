using System.Collections;
using UnityEngine;

public class BushSnake : MonoBehaviour
{
    public delegate void BushSnakeEventsDelegate();
    public event BushSnakeEventsDelegate OnTimeOver; //game over for the player

    public float attackTime = 0.0f;
    public Animator snakeAnim;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Jugador")
        {
            print("Looking At Player");
            snakeAnim.SetTrigger("ComeOut");
            attack = SnakeAttack();
            StartCoroutine(attack);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Jugador")
        {
            snakeAnim.SetTrigger("GoBack");
            StopAllCoroutines();
            Debug.Log("Player Gone");
        }
    }

    IEnumerator attack;
    IEnumerator SnakeAttack()
    {
        yield return new WaitForSeconds(attackTime);
        if(OnTimeOver != null)
        {
            OnTimeOver();
        }
        snakeAnim.SetTrigger("Attack");
        GameManager.GameOver();
        Debug.Log("Game Over");
    }
}