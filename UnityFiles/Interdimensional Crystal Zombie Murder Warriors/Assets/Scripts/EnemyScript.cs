using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyScript : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 20;
    [SerializeField]
    private int health = 0;

    [SerializeField]
    private int attackDelay = 2;
    [SerializeField]
    private int damage = 1;

    private GameObject player = null;

    [SerializeField]
    private GameObject bloodSplat;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private float bloodLifespan = 0.75f;

    Coroutine attackRoutine;

    Vector3 originalPosition;

    void Awake()
    {
        originalPosition = transform.position;
    }

    private void Start()
    {
        health = maxHealth;
        healthBar.fillAmount = (float)health / (float)maxHealth;
    }

    private void Update()
    {
        CheckHealth();
    }

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Projectile")
        {
            health--;
        }

        if (collision.tag == "Player")
        {
            Vector3 nexusPosition = GameObject.Find("Nexus").GetComponent<Transform>().position;
            nexusPosition.z = collision.transform.position.z;
            if (!collision.GetComponent<PlayerMouseMovement>().IsMoving() && collision.transform.position != nexusPosition)
            {
                player = collision.gameObject;
                StartCoroutine("AttackPlayer");
            }
        }

        return;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !collision.GetComponent<PlayerMouseMovement>().IsMoving())
        {
            if (player == null)
            {
                player = collision.gameObject;
                StartCoroutine("AttackPlayer");
            }
        }
        else if (collision.tag == "Player")
        {
            StopCoroutine("AttackPlayer");
            player = null;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            StopCoroutine("AttackPlayer");
            player = null;
        }
    }

    private IEnumerator AttackPlayer()
    {
        Vector3 nexusPosition = GameObject.Find("Nexus").GetComponent<Transform>().position;
        nexusPosition.z = player.transform.position.z;

        if (player.GetComponent<PlayerScript>().GetHealth() > 0 && !player.GetComponent<PlayerMouseMovement>().IsMoving() && player.transform.position != nexusPosition)
        {
            player.GetComponent<PlayerScript>().DamageHealth(damage);
            yield return new WaitForSeconds(attackDelay);
            StartCoroutine("AttackPlayer");
        }
        else
        {
            player = null;
        }
    }

    public void DamageHealth(int _amount)
    {
        health -= _amount;
        health = Mathf.Max(health, 0);
        healthBar.fillAmount = (float)health / (float)maxHealth;
        Vector3 bloodPosition = transform.position;
        bloodPosition.z = -1;
        GameObject blood = Instantiate(bloodSplat, bloodPosition, Quaternion.identity);
        Destroy(blood, bloodLifespan);
        return;
    }

    public void HealHealth(int _amount)
    {
        health += _amount;
        health = Mathf.Min(health, maxHealth);
        healthBar.fillAmount = (float)health / (float)maxHealth;
        return;
    }

    private void CheckHealth()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
