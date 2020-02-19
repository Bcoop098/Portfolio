using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody2D))]
public class HomingShot : MonoBehaviour
{

    private GameObject target;

    private List<GameObject> hitEnemies;

    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float rotateSpeed = 200f;
    [SerializeField]
    private int damage = 1;

    [SerializeField]
    private int maxChainCount = 3;

    private int initialChainCount = 0;

    private void Awake()
    {
        hitEnemies = new List<GameObject>();
    }

    [SerializeField]
    private float timeToDeath = 1.0f;

    private float timeRemaining = 100.0f;

    // Start is called before the first frame update
    void Start()
    {
        target = FindClosestTarget("Enemy");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timeRemaining -= Time.deltaTime;
        if (timeRemaining <= 0.0f)
        {
            Destroy(gameObject);
        }
        IsTargetAlive();
        if (target != null)
        {
            float step = speed * Time.deltaTime;

            Vector3 vectorToTarget = target.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);

            transform.position = Vector2.MoveTowards(transform.position, target.transform.position, step);
        }
        else
        {
            Destroy(gameObject);
        }

    }
    GameObject FindClosestTarget(string _target)
    {
        // Get the all enemies currently in the scene.
        GameObject[] targets = GameObject.FindGameObjectsWithTag(_target);


        Vector3 position = transform.position;
        float distance = Mathf.Infinity;
        GameObject closest = null;

        // Loop through each enemy currently in the scene.
        foreach (GameObject enemy in targets)
        {
            //this is work in progress for chain lightning
            if (hitEnemies.Count > 0)
            {
                if (hitEnemies.Contains(enemy)) //change to see if closest is in there
                {
                    continue;
                }
            }

            // Get the distance to the current enemy in the list.
            float currentDistance = Vector3.Distance(enemy.transform.position, position);

            // The current enemy is closer than the previous closest enemy.
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = enemy;
                
            }
        }

        return closest;
    }

    private void IsTargetAlive()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in targets)
        {
            bool enemyHit = false;

            // Check if the current enemy had been hit already.
            foreach (GameObject hitEnemy in hitEnemies)
            {
                if (enemy == hitEnemy)
                {
                    enemyHit = true;
                    continue;
                }
            }

            // If the enemy has been hit, go to the next enemy.
            if (enemyHit)
            {
                continue;
            }

            if (enemy != null && target != null)
            {
                if (enemy.transform == target.transform)
                {
                    return;
                }
            }
        }

        target = null;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<EnemyScript>().DamageHealth(damage);

            if (initialChainCount == maxChainCount)
            {
                Destroy(gameObject);
                return;
            }

            initialChainCount++;

            hitEnemies.Add(target);

            target = FindClosestTarget("Enemy");
            timeRemaining = timeToDeath;
        }


    }
}
