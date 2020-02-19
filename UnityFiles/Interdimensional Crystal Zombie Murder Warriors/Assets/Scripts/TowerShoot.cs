using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerShoot : MonoBehaviour
{
    // Target cannot be transform because null reference doesn't work for transforms only gameobjects.
    private GameObject target;
    [SerializeField]
    private GameObject laserPrefab;

    private Transform laserSpawn;

    public bool canShoot = true;
    public bool isInRange = false;

    [SerializeField]
    private float fireDelay = 1.5f;

    [SerializeField]
    private float newDelay = 0.75f;

    private bool canPowerUp = true;

    [SerializeField]
    private float powerTime = 6.0f;

    private float normalSpeed;

    [SerializeField]
    private float range = 7.5f;

    [SerializeField]
    private bool canDestruct = true;

    [SerializeField]
    private int selfDestructDamage = 8;

    // Use this for initialization
    void Start()
    {
        normalSpeed = fireDelay;
        laserSpawn = transform.Find("LaserSpawn");
        target = FindClosestTarget("Enemy");
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.GetComponent<PlayerScript>().GetHealth() == gameObject.GetComponent<PlayerScript>().GetMaxHealth())
        {
            canDestruct = true;
        }
        // Target cannot be transform because null reference doesn't work for transforms only gameobjects.
        Vector3 nexusPosition = GameObject.Find("Nexus").GetComponent<Transform>().position;
        nexusPosition.z = transform.position.z;

        if (!gameObject.GetComponent<PlayerMouseMovement>().IsMoving() && transform.position != nexusPosition)
        {
            target = FindClosestTarget("Enemy");
            RangeCheck();
            CheckInput();
        }
        else
        {
            isInRange = false;
        }
        if (gameObject.GetComponent<PlayerScript>().GetHealth() <= 5)
        {
            if (canDestruct == true)
            {
                SelfDestruct();
            }
        }
    }

    void CheckInput()
    {
        if (canShoot && isInRange)
        {
            Fire();
            StartCoroutine(FireDelay());
        }
    }

    void Fire()
    {
        AudioManager.instance.PlayAttack(gameObject.name);
        GameObject laser = Instantiate(laserPrefab, laserSpawn.position, laserSpawn.rotation) as GameObject;
    }

    IEnumerator FireDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireDelay);
        canShoot = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Xygo")
        {
            if (canPowerUp == true)
            {
                ChangeSpeed();
            }
        }
    }

    GameObject FindClosestTarget(string _target)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(_target);

        GameObject closest = null;

        float distance = Mathf.Infinity;

        if (targets.Length == 0)
        {
            return closest;
        }
        else
        {
            foreach (GameObject enemy in targets)
            {
                float currentDistance = Vector3.Distance(enemy.transform.position, transform.position);

                if (currentDistance < distance)
                {
                    closest = enemy;
                    distance = currentDistance;
                }
            }
        }
        return closest;
    }

    void RangeCheck()
    {
        if (target == null)
        {
            isInRange = false;
            return;
        }

        float distance = Vector2.Distance(laserSpawn.position, target.transform.position);

        if (distance <= range)
        {
            isInRange = true;
        }
        else
        {
            isInRange = false;
        }
    }

    void SelfDestruct()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in targets)
        {
            float distance = Vector2.Distance(laserSpawn.position, enemy.transform.position);
            if (distance <= range)
            {
                enemy.GetComponent<EnemyScript>().DamageHealth(selfDestructDamage);
            }
        }
        canDestruct = false;
    }

    private void NormalSpeed()
    {
        fireDelay = normalSpeed;
        canPowerUp = true;
    }
    private void ChangeSpeed()
    {
        canPowerUp = false;
        fireDelay = newDelay;
        Invoke("NormalSpeed", powerTime);
    }

}