using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanerWeapon : MonoBehaviour
{
    public int weaponDamage = 25;

    public int coolDown = 5;

    bool canAttack = false;
    bool attacking = false;

    bool alreadyFlip1 = true;
    bool alreadyFlip2 = false;

    GameObject target;
    [SerializeField]
    SpriteRenderer spriteRenderer;

    private void Start()
    {
        canAttack = true;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = false;
    }

    private void FixedUpdate()
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

    public void FlipBarAttack(bool dir)
    {
        //Determines when to flip the offset of the box collider
        if (dir )//&& gameObject.GetComponent<PolygonCollider2D>().offset.y > 0)
        {
            //gameObject.GetComponent<PolygonCollider2D>().offset = new Vector2(gameObject.GetComponent<PolygonCollider2D>().offset.x, gameObject.GetComponent<PolygonCollider2D>().offset.y) * -1;
            if (!alreadyFlip1)
            {
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector3(0, 0, 0);
                alreadyFlip1 = true;
                alreadyFlip2 = false;
            }
        }

        else if (!dir) //&& gameObject.GetComponent<PolygonCollider2D>().offset.y < 0)
        {
            //gameObject.GetComponent<PolygonCollider2D>().offset = new Vector2(gameObject.GetComponent<PolygonCollider2D>().offset.x, gameObject.GetComponent<PolygonCollider2D>().offset.y );
            
            if(!alreadyFlip2)
            {
                gameObject.GetComponent<BoxCollider2D>().offset = new Vector3(0, 6, 0);
                alreadyFlip1 = false;
                alreadyFlip2 = true;
            }
        }

    }

    public bool checkAttack()
    {
        if (canAttack && attacking)
        {
            return true;
        }
        return false;
    }
}
