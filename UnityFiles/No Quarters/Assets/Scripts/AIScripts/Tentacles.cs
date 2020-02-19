using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tentacles : MonoBehaviour
{
    [SerializeField]
    private int tentacleDamage = 5;

    

    [SerializeField]
    private int tentacleHealth = 100;

    [SerializeField]
    GameObject tentacle;
    GameObject myTentacle;

    GameObject boss;

    float invincibilityFrames = 1f;

    [SerializeField]
    bool canAttack = false;

    [SerializeField]
    bool attacking = false;

    GameObject target;

    bool canBeDamaged = true;

    void Start()
    {
        canAttack = true;
        myTentacle = Instantiate(tentacle, new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
        invincibilityFrames = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getInitTimer();
        boss = GameObject.FindGameObjectWithTag("MaidBoss");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (attacking && target != null && canAttack)
        {

            target.GetComponentInParent<PlayerControllerV2>().TakeDamage(tentacleDamage);
            canAttack = false;
            StartCoroutine("CoolDown");
        }

        if (tentacleHealth <= 0)
        {
            boss.GetComponent<MeterMaidBoss>().KillTentacle(gameObject);
            gameObject.SetActive(false);
            myTentacle.SetActive(false);
            //Destroy(gameObject);
            //Destroy(myTentacle);
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
        }
    }

    IEnumerator CoolDown()
    {
        if (attacking)
        {
            yield return new WaitForSecondsRealtime(3.0f);
            canAttack = true;
        }
    }

    public void SetDamagedStatus(bool canHit)
    {
        canBeDamaged = canHit;
    }

    public void TakeDamage(int incomingDamage)
    {
        if (canBeDamaged)
        {
            SetDamagedStatus(false);
            tentacleHealth -= incomingDamage;
            StartCoroutine(InvincibilityFrame());
        }
    }

    IEnumerator InvincibilityFrame()
    {
        yield return new WaitForSeconds(invincibilityFrames);
        SetDamagedStatus(true);
    }

    public bool checkTentacleAttack()
    {
        if (canAttack && attacking)
        {
            return true;
        }
        return false;
    }
}
