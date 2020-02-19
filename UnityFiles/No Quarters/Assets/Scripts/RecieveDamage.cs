using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecieveDamage : MonoBehaviour
{
    private float IFrameTime = 1.0f;
    public bool Invincible = false;

    public void DealDamage(int damage, float direction)
    {
        if(!Invincible)
            StartCoroutine(IFrames(damage, direction));
    }
    IEnumerator IFrames(int damage, float direction)
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(4.0f, 0.0f) * direction);
        gameObject.GetComponent<BaseHuman>().health -= damage;
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        Invincible = true;
        yield return new WaitForSeconds(IFrameTime);
        Invincible = false;
        gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        //attack = true;
        //StartCoroutine(timeBeforeNextAttack(0, 2));
    }
}
