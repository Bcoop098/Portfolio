using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeasantScriptV2 : BaseHuman
{

    // why are there 3 damage floats? player, payer manager, and weapon damage
    GameObject Target;

    Vector2 velocity;

    public float moveSpeed;
    public float detectionRadius;
    public float closureDistance;
    public float fleeThreshold; // a float between 0 and 1 that determines when the the peasant flees
    public bool moveLeft;
    bool shouldFlee;
    float lastX;
    Vector3 direction;

    float distance;
    //bool aggro;
    float maxHealth;

    // Make sure you set up the target and input speed, radius, and closuredistance
    void Start()
    {
        lastX = 0;
        WanderDirection();
        shouldFlee = false;
        Target = GameObject.FindGameObjectWithTag("Player");
        maxHealth = gameObject.GetComponent<BaseHuman>().health;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0f)
        {
            this.gameObject.GetComponent<CoinDrop>().SpawnCoin(this.gameObject);
            this.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
            Toolbox.Instance.GetObject<PixelLord>("PixelLord").EnemyKilled(iD, quadrant);
            this.gameObject.GetComponent<RecieveDamage>().Invincible = false;
            gameObject.SetActive(false);
            ResetStats();
        }
        //float health = gameObject.GetComponent<BaseHuman>().health;
        direction = Target.transform.position - this.transform.position;
        if (distance > detectionRadius)
        {
            setTarget();
        }
        if (health < (maxHealth * fleeThreshold))
        {
            shouldFlee = true;
        }
        if (Toolbox.Instance.GetObject<PixelLord>("PixelLord").GetPlayerQuadrant() == gameObject.GetComponent<BaseHuman>().GetQuadrant())
        {
            if(shouldFlee)
            {
                velocity += Flee();
            }
            else velocity += Wander();

            UpdatePosition();
            UpdateDirection();
        }
        velocity = new Vector2(0f, 0f);
    }
    void setTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        float bestDistance = 10000f;
        foreach (GameObject player in players)
        {

            distance = (player.transform.position - this.transform.position).magnitude;
            if (distance < bestDistance)
            {
                Target = player;
            }
            Debug.Log(Target.transform.position);
        }
    }
    void UpdatePosition()
    {
        transform.Translate(velocity * Time.deltaTime);
    }

    Vector2 Wander()
    {
        if (!moveLeft)
        {
            lastX = moveSpeed;
            return new Vector3(1.0f, 0.0f, 0.0f);
        }
        else
        {
            lastX = -moveSpeed;
            return new Vector3(-1.0f, 0.0f, 0.0f);
        }
    }

    Vector2 Seek()
    {
        direction.Normalize();
        return direction;
    }

    Vector2 Flee()
    {
        return -Seek();
    }

    void WanderDirection()
    {
        int decision = Random.Range(0, 2);
        if (decision == 1)
        {
            moveLeft = true;
            lastX = 1;
        }
        else
        {
            moveLeft = false;
            lastX = -1;
        }
    }

    void UpdateDirection()
    {
        if(direction.x < 0 && lastX > 0)
            this.GetComponent<SpriteRenderer>().flipX = false;
        if(direction.x > 0 && lastX < 0)
            this.GetComponent<SpriteRenderer>().flipX = true;
        lastX = direction.x;
    }

}
