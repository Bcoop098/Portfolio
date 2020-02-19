using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterMaidMelee : MonoBehaviour
{
    public int weaponDamage = 25;

    public int coolDown = 5;

    bool canAttack = false;
    bool attacking = false;

    GameObject target;

    [SerializeField]
    SpriteRenderer spriteRenderer;

    [SerializeField]
    GameObject MaidBoss;

    private void Start()
    {
        canAttack = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void FixedUpdate()
    {
        if (MaidBoss.GetComponent<MeterMaidBoss>().checkIfPissed())
        {
            if (attacking && target != null && canAttack)
            {
                spriteRenderer.enabled = true;
                target.GetComponentInParent<PlayerControllerV2>().TakeDamage(weaponDamage);
                canAttack = false;
                StartCoroutine("CoolDown");
                StartCoroutine("DisableRenderer");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (attacking == false)
            {
                target = collision.gameObject;
                attacking = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attacking = false;
            //StopCoroutine("CoolDown");
        }
    }

    IEnumerator CoolDown()
    {
        if (attacking)
        {

            yield return new WaitForSecondsRealtime(5.0f);
            canAttack = true;
        }
    }

    IEnumerator DisableRenderer()
    {
        if (attacking)
        {
            yield return new WaitForSecondsRealtime(1.5f);
            spriteRenderer.enabled = false;
        }
    }

    /*public void FlipBarAttack(bool dir)
    {
        //Determines when to flip the offset of the box collider
        if (dir && gameObject.GetComponent<BoxCollider2D>().offset.x > 0)
        {
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x * -1, gameObject.GetComponent<BoxCollider2D>().offset.y);
        }

        else if (!dir && gameObject.GetComponent<BoxCollider2D>().offset.x < 0)
        {
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x * -1, gameObject.GetComponent<BoxCollider2D>().offset.y);
        }

    }*/

    public bool checkAttack()
    {
        if (canAttack && attacking)
        {
            return true;
        }
        return false;
    }
}
