using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KruxScript : MonoBehaviour
{

    private float normalSpeed;

    [SerializeField]
    private float speedModifier = 1.25f;

    private bool canPowerUp = true;

    [SerializeField]
    private float powerTime = 3.0f;

    [SerializeField]
    private float kruxAttackDelay = 2.5f;

    [SerializeField]
    private int kruxDamage = 5;

    private List<GameObject> enemies;

    private bool range = false;

    private float timeRemaining = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        normalSpeed = kruxAttackDelay;
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 nexusPosition = GameObject.Find("Nexus").GetComponent<Transform>().position;
        nexusPosition.z = transform.position.z;
        if (!gameObject.GetComponent<PlayerMouseMovement>().IsMoving() && transform.position != nexusPosition)
        {
            timeRemaining += Time.deltaTime;
            TimeCheck();
            MeleeAttack();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (!enemies.Contains(collision.gameObject))
            {
                enemies.Add(collision.gameObject);
            }
        }

        if (collision.tag == "Xygo")
        {
            ChangeSpeed();
        }
    }

    private void TimeCheck()
    {
        if (timeRemaining >= kruxAttackDelay)
        {
            range = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            enemies.Remove(collision.gameObject);
        }
    }

    private void MeleeAttack()
    {
        if (range == true)
        {
            foreach (GameObject enemy in enemies)
            {
                AudioManager.instance.PlayAttack(gameObject.name);
                enemy.GetComponent<EnemyScript>().DamageHealth(kruxDamage);
            }
            timeRemaining = 0.0f;
            range = false;
        }
    }
    private void NormalSpeed()
    {
        kruxAttackDelay = normalSpeed;
        canPowerUp = true;
    }
    private void ChangeSpeed()
    {
        canPowerUp = false;
        kruxAttackDelay = kruxAttackDelay / speedModifier;
        Invoke("NormalSpeed", powerTime);
    }
}

