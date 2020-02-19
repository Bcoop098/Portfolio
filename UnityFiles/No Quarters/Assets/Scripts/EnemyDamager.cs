using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamager : MonoBehaviour
{
    // Start is called before the first frame update
    public float direction = 1.0f;
    public int damage = 25;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<RecieveDamage>().DealDamage(damage, direction);
        }

        else if (collision.gameObject.CompareTag("Table"))
        {
            collision.gameObject.GetComponent<TableScript>().TableDamage(damage);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BaristaScript>().TakePlayerDamage(damage);
        }
        else if (collision.gameObject.CompareTag("Boss2"))
        {
            collision.gameObject.GetComponent<CleanerScript>().TakePlayerDamage(damage);
        }

        else if (collision.gameObject.CompareTag("Boss3"))
        {
            collision.gameObject.GetComponent<BankerScript>().TakePlayerDamage(damage);
        }
        else if (collision.gameObject.CompareTag("Dryer"))
        {
            collision.gameObject.GetComponent<DryerScript>().MachineDamage(damage);
        }

        else if (collision.gameObject.CompareTag("Washer"))
        {
            collision.gameObject.GetComponent<WasherScript>().MachineDamage(damage);
        }
        else if (collision.gameObject.CompareTag("BankGuard"))
        {
            collision.gameObject.GetComponent<BaseBossGuard>().TakeDamage(damage);
        }

        else if (collision.gameObject.CompareTag("MeterMinion"))
        {
            collision.gameObject.GetComponent<BaseMeterMinion>().TakeDamage(damage);
        }

        else if (collision.gameObject.CompareTag("Tentacle"))
        {
            collision.gameObject.GetComponent<Tentacles>().TakeDamage(damage);
        }

        else if (collision.gameObject.CompareTag("MaidBoss"))
        {
            collision.gameObject.GetComponent<MeterMaidBoss>().TakePlayerDamage(damage);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("RERE");
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<RecieveDamage>().DealDamage(damage, direction);
        }

        else if (collision.gameObject.CompareTag("Table"))
        {
            collision.gameObject.GetComponent<TableScript>().TableDamage(damage);
        }
        else if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BaristaScript>().TakePlayerDamage(damage);
        }
        else if (collision.gameObject.CompareTag("Boss2"))
        {
            collision.gameObject.GetComponent<CleanerScript>().TakePlayerDamage(damage);
        }

        else if (collision.gameObject.CompareTag("Boss3"))
        {
            collision.gameObject.GetComponent<BankerScript>().TakePlayerDamage(damage);
        }
        else if (collision.gameObject.CompareTag("Dryer"))
        {
            collision.gameObject.GetComponent<DryerScript>().MachineDamage(damage);
        }

        else if (collision.gameObject.CompareTag("Washer"))
        {
            collision.gameObject.GetComponent<WasherScript>().MachineDamage(damage);
        }
        else if (collision.gameObject.CompareTag("BankGuard"))
        {
            collision.gameObject.GetComponent<BaseBossGuard>().TakeDamage(damage);
        }

        else if (collision.gameObject.CompareTag("MeterMinion"))
        {
            collision.gameObject.GetComponent<BaseMeterMinion>().TakeDamage(damage);
        }

        else if (collision.gameObject.CompareTag("Tentacle"))
        {
            collision.gameObject.GetComponent<Tentacles>().TakeDamage(damage);
        }

        else if (collision.gameObject.CompareTag("MaidBoss"))
        {
            collision.gameObject.GetComponent<MeterMaidBoss>().TakePlayerDamage(damage);
        }
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector2(3.0f, 0.0f) * direction * Time.deltaTime);
        Color newColor = this.GetComponentInChildren<SpriteRenderer>().color;
        newColor.a *= 0.9f;
        this.GetComponentInChildren<SpriteRenderer>().color = newColor;
    }
}
