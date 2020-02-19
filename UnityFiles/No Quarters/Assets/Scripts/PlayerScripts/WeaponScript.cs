using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    public int weaponDamage = 25;

    public float coolDown = 0.7f;

    [SerializeField] bool canAttack = false;//can player attack
    [SerializeField] bool attacking = false;//are they attacking

    GameObject target;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If the weapon collides with a object that is an enemy, this will run
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //If they are not currently attacking, assign the target GO with the enemy and set attacking to true
            if (attacking == false)
            {
                target = collision.gameObject;
                attacking = true;
            }
        }
        else if (collision.gameObject.CompareTag("Table"))
        {
            //If they are not currently attacking, assign the target GO with the enemy and set attacking to true
            if (attacking == false)
            {
                target = collision.gameObject;
                attacking = true;
            }
        }

        else if (collision.gameObject.CompareTag("Boss"))
        {
            //If they are not currently attacking, assign the target GO with the enemy and set attacking to true
            if (attacking == false)
            {
                target = collision.gameObject;
                attacking = true;
            }
        }

        else if (collision.gameObject.CompareTag("Boss2"))
        {
            //If they are not currently attacking, assign the target GO with the enemy and set attacking to true
            if (attacking == false)
            {
                target = collision.gameObject;
                attacking = true;
            }
        }

        else if (collision.gameObject.CompareTag("Boss3"))
        {
            //If they are not currently attacking, assign the target GO with the enemy and set attacking to true
            if (attacking == false)
            {
                target = collision.gameObject;
                attacking = true;
            }
        }

        else if (collision.gameObject.CompareTag("Machine"))
        {
            //If they are not currently attacking, assign the target GO with the enemy and set attacking to true
            if (attacking == false)
            {
                target = collision.gameObject;
                attacking = true;
            }
        }
        else if (collision.gameObject.CompareTag("BankGuard"))
        {
            //If they are not currently attacking, assign the target GO with the enemy and set attacking to true
            if (attacking == false)
            {
                target = collision.gameObject;
                attacking = true;
            }
        }

        else if (collision.gameObject.CompareTag("MeterMinion"))
        {
            if (attacking == false)
            {
                target = collision.gameObject;
                attacking = true;
            }
        }

        else if (collision.gameObject.CompareTag("Tentacle"))
        {
            if (attacking == false)
            {
                target = collision.gameObject;
                attacking = true;
            }
        }

        else if (collision.gameObject.CompareTag("MaidBoss"))
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
        //Once weapon leaves collision range o enemy, make it so they are not attacking
        if (collision.gameObject.CompareTag("Enemy"))
        {
            attacking = false;
        }

        if (collision.gameObject.CompareTag("Table"))
        {
            attacking = false;
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            attacking = false;
        }

        if (collision.gameObject.CompareTag("Washer"))
        {
            attacking = false;
        }

        if (collision.gameObject.CompareTag("Dryer"))
        {
            attacking = false;
        }

        if (collision.gameObject.CompareTag("BankGuard"))
        {
            attacking = false;
        }

        if (collision.gameObject.CompareTag("Boss2"))
        {
            attacking = false;
        }

        if (collision.gameObject.CompareTag("Boss3"))
        {
            attacking = false;
        }

        if (collision.gameObject.CompareTag("MeterMinion"))
        {
            attacking = false;
        }

        if (collision.gameObject.CompareTag("Tentacle"))
        {
            attacking = false;
        }

        if (collision.gameObject.CompareTag("MaidBoss"))
        {
            attacking = false;
        }
    }


    //Setters
    public void AttackStatus(bool state)
    {
        canAttack = state;
    }

    public void FlipSideAttack(bool dir)
    {
        //Determines when to flip the offset of the box collider
        if (dir && gameObject.GetComponent<BoxCollider2D>().offset.x > 0)
        {
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x * -1, gameObject.GetComponent<BoxCollider2D>().offset.y);
        }
   
        else if(!dir && gameObject.GetComponent<BoxCollider2D>().offset.x < 0)
        {
            gameObject.GetComponent<BoxCollider2D>().offset = new Vector2(gameObject.GetComponent<BoxCollider2D>().offset.x * -1, gameObject.GetComponent<BoxCollider2D>().offset.y);
        }

    }
    //Getter
    public bool GetAttackStatus() { return canAttack; }

    public bool GetAttacking() { return attacking; }

    private void Awake()
    {
        if (gameObject.gameObject.name == "PlayerUnitP1")
            weaponDamage = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getWeaponDamage(0);
        else if (gameObject.gameObject.name == "PlayerUnitP2")
            weaponDamage = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getWeaponDamage(1);

        coolDown = Toolbox.Instance.GetObject<PlayerData>("PlayerData").getInitTimer();

    }

    private void FixedUpdate()
    {
        //This will run only if player pressed attack key and if within range of enemy.
        if (attacking && canAttack)
        {
            if (target.tag == "Enemy")
            {
                target.GetComponent<BaseHuman>().TakeDamage(weaponDamage);
            }
            else if (target.tag == "Table")
            {
                target.GetComponent<TableScript>().TableDamage(weaponDamage);
            }
            else if (target.tag == "Boss")
            {
                target.GetComponent<BaristaScript>().TakePlayerDamage(weaponDamage);
            }
            else if (target.tag == "Boss2")
            {
                target.GetComponent<CleanerScript>().TakePlayerDamage(weaponDamage);
            }

            else if (target.tag == "Boss3")
            {
                target.GetComponent<BankerScript>().TakePlayerDamage(weaponDamage);
            }
            else if (target.tag == "Dryer")
            {
                target.GetComponent<DryerScript>().MachineDamage(weaponDamage);
            }

            else if (target.tag == "Washer")
            {
                target.GetComponent<WasherScript>().MachineDamage(weaponDamage);
            }

            else if (target.tag == "BankGuard")
            {
                target.GetComponent<BaseBossGuard>().TakeDamage(weaponDamage);
            }

            else if (target.tag == "MeterMinion")
            {
                target.GetComponent<BaseMeterMinion>().TakeDamage(weaponDamage);
            }

            else if (target.tag == "Tentacle")
            {
                target.GetComponent<Tentacles>().TakeDamage(weaponDamage);
            }

            else if (target.tag == "MaidBoss")
            {
                target.GetComponent<MeterMaidBoss>().TakePlayerDamage(weaponDamage);
            }
        }
    }
}
