using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantScript : MonoBehaviour
{

    // why are there 3 damage floats? player, payer manager, and weapon damage
    GameObject Target;
    public float moveSpeed;
    public float detectionRadius;
    public float closureDistance;
    public float fleeThreshold; // a float between 0 and 1 that determines when the the peasant flees
    public bool wanderDirection;
    public bool flee;

    float distance;
    bool aggro;
    float maxHealth;

    // Make sure you set up the target and input speed, radius, and closuredistance
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        maxHealth = gameObject.GetComponent<BaseHuman>().health;
    }

    // Update is called once per frame
    void Update()
    {
        if (Toolbox.Instance.GetObject<PixelLord>("PixelLord").GetPlayerQuadrant() == gameObject.GetComponent<BaseHuman>().GetQuadrant())
        {
            distance = (Target.transform.position - this.transform.position).magnitude;
            if (distance < detectionRadius)
                aggro = true;
            if (!aggro)
                wander();
            else
                seek();
        }
    }

    private void wander()
    {
        if (wanderDirection)
            transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f));
        else
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0.0f, 0.0f));
    }

    void seek()
    {
        Vector3 direction = Target.transform.position - this.transform.position;

        float health = gameObject.GetComponent<BaseHuman>().health;
        if (health < (maxHealth * fleeThreshold))
            flee = true;

        if (distance > closureDistance) // keeps the enemy from closing in too much
        {
            if (flee)
                transform.Translate(-direction.normalized * Time.deltaTime);
            else
                transform.Translate(direction.normalized * Time.deltaTime);
        } 
    }
}
