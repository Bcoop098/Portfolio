using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HollowShoot : MonoBehaviour
{
    // Target cannot be transform because null reference doesn't work for transforms only gameobjects.
    private GameObject target;
    [SerializeField]
    private GameObject laserPrefab;

    private Transform laserSpawn;

    private bool canShoot = true;
    private bool isInRange = false;

    [SerializeField]
    private float fireDelay = 1.5f;

    private GameObject nexus;

    private Vector2 nexusPosition;

    [SerializeField]
    private float range = 5.0f;

    // Use this for initialization
    void Start()
    {
        nexus = GameObject.Find("Nexus");
        nexusPosition = nexus.transform.position;

        laserSpawn = transform.Find("LaserSpawn");
        target = FindClosestTarget("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Target cannot be transform because null reference doesn't work for transforms only gameobjects.      
        target = FindClosestTarget("Player");
        RangeCheck();
        CheckInput();
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
        GameObject laser = Instantiate(laserPrefab, laserSpawn.position, laserSpawn.rotation) as GameObject;
    }

    IEnumerator FireDelay()
    {
        canShoot = false;
        yield return new WaitForSeconds(fireDelay);
        canShoot = true;
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
            foreach (GameObject player in targets)
            {
                float currentDistance = Vector3.Distance(player.transform.position, transform.position);

                Vector2 playerPosition = player.transform.position;

                if (currentDistance < distance && playerPosition != nexusPosition)
                {
                    closest = player;
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
            gameObject.GetComponent<Pathfinding>().ChangeMovement(true);
            return;
        }

        float distance = Vector2.Distance(laserSpawn.position, target.transform.position);

        if (distance <= range)
        {
            isInRange = true;
            gameObject.GetComponent<Pathfinding>().ChangeMovement(false);
        }
        else
        {
            isInRange = false;
            gameObject.GetComponent<Pathfinding>().ChangeMovement(true);
        }
    }
}
