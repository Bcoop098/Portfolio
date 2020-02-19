using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HollowScript : MonoBehaviour
{
    [SerializeField]
    private int maxHealth = 20;
    [SerializeField]
    private int health = 0;

    [SerializeField]
    private float odoAttackDelay = 1.25f;

    [SerializeField]
    private int attackDelay = 2;
    [SerializeField]
    private int damage = 1;

    [SerializeField]
    private int odoDamage = 1;

    private GameObject player = null;

    [SerializeField]
    private GameObject bloodSplat;

    [SerializeField]
    private Image healthBar;

    [SerializeField]
    private bool isInRange = false;

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

        if (collision.tag == "Odo")
        {
            Vector3 nexusPosition = GameObject.Find("Nexus").GetComponent<Transform>().position;
            nexusPosition.z = collision.transform.position.z;
            if (!collision.transform.parent.GetComponent<PlayerMouseMovement>().IsMoving() && collision.transform.parent.transform.position != nexusPosition)
            {
                DamageHealth(odoDamage);
                isInRange = true;
                StartCoroutine("TakeMeleeDamage");
            }
        }

        return;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Odo" && collision.transform.parent.GetComponent<PlayerMouseMovement>().IsMoving())
        {
            isInRange = false;
        }
        else if (collision.tag == "Odo")
        {
            isInRange = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Odo")
        {
            Vector3 nexusPosition = GameObject.Find("Nexus").GetComponent<Transform>().position;
            nexusPosition.z = collision.transform.parent.transform.position.z;
            if (!collision.transform.parent.GetComponent<PlayerMouseMovement>().IsMoving() && collision.transform.parent.transform.position != nexusPosition)
            {
                isInRange = false;
            }
        }
    }

    private IEnumerator TakeMeleeDamage()
    {
        yield return new WaitForSeconds(odoAttackDelay);
        if (isInRange == true)
        {
            DamageHealth(odoDamage);
            StartCoroutine("TakeMeleeDamage");
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
            isInRange = false;
        }
    }
}
