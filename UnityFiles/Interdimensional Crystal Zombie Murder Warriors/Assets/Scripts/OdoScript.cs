using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OdoScript : MonoBehaviour
{

    private float normalSpeed;

    [SerializeField]
    private float speedModifier = 1.25f;

    private bool canPowerUp = true;

    [SerializeField]
    private float powerTime = 3.0f;

    [SerializeField]
    private float odoAttackDelay = 1.25f;

    [SerializeField]
    private int odoDamage = 1;

    private List<GameObject> enemies;

    private bool range = false;

    private float timeRemaining = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        normalSpeed = odoAttackDelay;
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
        if (timeRemaining >= odoAttackDelay)
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
                enemy.GetComponent<EnemyScript>().DamageHealth(odoDamage);
            }
            timeRemaining = 0.0f;
            range = false;
        }
    }
    private void NormalSpeed()
    {
        odoAttackDelay = normalSpeed;
        canPowerUp = true;
    }
    private void ChangeSpeed()
    {
        canPowerUp = false;
        odoAttackDelay = odoAttackDelay / speedModifier;
        Invoke("NormalSpeed", powerTime);
    }
}