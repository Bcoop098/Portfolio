using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    private Transform target;
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float rotateSpeed = 200f;
    [SerializeField]
    private int damage = 1;
    // Start is called before the first frame update
    void Start()
    {
        target = FindClosestTarget("Player").transform;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        IsTargetAlive();
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            Vector3 vectorToTarget = target.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotateSpeed);
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    GameObject FindClosestTarget(string _target)
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag(_target);
        Vector3 position = transform.position;
        float distance = Mathf.Infinity;
        GameObject closest = null;
        foreach (GameObject player in targets)
        {
            float currentDistance = Vector3.Distance(player.transform.position, position);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = player;
            }
        }
        return closest;
    }
    private void IsTargetAlive()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in targets)
        {
            if (player.transform == target)
            {
                return;
            }
        }
        target = null;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<PlayerScript>().DamageHealth(damage);
            Destroy(gameObject);
        }
    }
}
