using System.Collections;
using UnityEngine;

public class BushSnake : MonoBehaviour
{
    public delegate void BushSnakeEventsDelegate();
    public event BushSnakeEventsDelegate OnSnakeAttack;

    public float attackTime = 0.0f;
    public Animator snakeAnim;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Jugador")
        {
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
        }
    }

    IEnumerator attack;
    IEnumerator SnakeAttack()
    {
        yield return new WaitForSeconds(attackTime);
        if(OnSnakeAttack != null)
        {
            OnSnakeAttack();
        }
        snakeAnim.SetTrigger("Attack");
        yield return new WaitForSeconds(0.15f);
        GameManager.GameOver();
    }
}