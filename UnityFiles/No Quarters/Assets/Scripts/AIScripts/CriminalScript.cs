using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriminalScript : BaseHuman
{
    GameObject Target;
    Vector3 playerVel;
    public Vector3 Direction;
    public Vector3 avoidance;
    public Vector3 Velocity;
    float moveSpeed = 0.75f;
    float detectionRadius = 6f;
    float closureDistance = 2f;
    float fleeThreshold = 0.25f;// a float between 0 and 1 that determines when the the peasant flees
    public bool wanderDirection;
    bool flee;
    float persueAnt = 1.0f;

    public bool moveBack;
    float distance;
    bool aggro;
    float maxHealth;
    bool attack;
    float attackDuration = 0.5f;
    float attackCooldown = 1.0f;
    int damage = 1;

    Vector3 PlayerMoveVector;

    // Make sure you set up the target and input speed, radius, and closuredistance
    void Start()
    {
        wanderDirection = false;
        Target = GameObject.FindGameObjectWithTag("Player");
        maxHealth = gameObject.GetComponent<BaseHuman>().health;
        attack = true;
    }

    // Update is called once per frame
    void Update()
    {
        distance = (Target.transform.position - this.transform.position).magnitude;
        PlayerMoveVector = playerVel + Target.transform.position;
        Direction = PlayerMoveVector - this.transform.position;
        Direction.Normalize();
        Velocity = Direction + avoidance;
        Velocity.Normalize();
        if (distance > closureDistance /*+ closureThreshold*/) moveBack = false;

        if (Toolbox.Instance.GetObject<PixelLord>("PixelLord").GetPlayerQuadrant() == gameObject.GetComponent<BaseHuman>().GetQuadrant())
        {
            //playerMoveDirection = playerManager.GetComponent<PlayerManager>().moveInput;
            playerVel = Target.GetComponent<PlayerControllerV2>().playerVelocity;
            PlayerMoveVector = playerVel + Target.transform.position;
            if (distance < detectionRadius)
                aggro = true;
            if (!aggro)
                wander();
            else
                pursue();
        }
        if (distance <= closureDistance/* + closureThreshold */&& attack == true)
        {
            StartCoroutine(Attack());
        }
    }



    IEnumerator timeBeforeNextAttack(float low, float high)
    {
        float time = Random.Range(low, high);
        yield return new WaitForSeconds(time);
        attack = true;
    }
    IEnumerator Attack()
    {
        attack = false;
        closureDistance -= 0.5f;
        yield return new WaitForSeconds(attackDuration);
        Target.GetComponent<PlayerControllerV2>().TakeDamage(damage); // dealing damage
        closureDistance += 0.5f;
        moveBack = true;
        StartCoroutine(AttackCoolDown());
    }

    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCooldown);
        moveBack = false;
        //attack = true;
        StartCoroutine(timeBeforeNextAttack(0, 2));
    }

    private void wander()
    {
        if (wanderDirection)
            transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f));
        else
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0.0f, 0.0f));
    }

    void pursue()
    {
        float health = gameObject.GetComponent<BaseHuman>().health;
        if (health < (maxHealth * fleeThreshold))
            flee = true;

        if (distance > closureDistance/* + closureThreshold*/) // keeps the enemy from closing in too much
        {
            if (flee || moveBack)
                transform.Translate(-Velocity * Time.deltaTime);
            else
                transform.Translate(Velocity * Time.deltaTime);
        }
        if (distance < closureDistance /* - closureThreshold*/)
        {
            transform.Translate(-Direction * Time.deltaTime);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Vector2 otherRange = other.transform.position - this.transform.position;
            float dot = Vector3.Dot(Direction.normalized, otherRange.normalized);
            if (dot != 0)
            {
                Vector3 newDirection = Vector3.Cross(Direction, new Vector3(0.0f, 0.0f, 1.0f));
                if ((other.transform.position - (this.transform.position + newDirection)).magnitude < (other.transform.position - (this.transform.position - newDirection)).magnitude)
                    avoidance = -newDirection;
                else avoidance = newDirection;
            }
            float radius = GetComponent<CircleCollider2D>().radius;
            //avoidance = (other.transform.position - this.transform.position);
            avoidance = (2 * radius - otherRange.magnitude) * avoidance;
            //avoidance.Normalize();
            //Direction -= avoidance;
            if (otherRange.magnitude < radius)
                transform.Translate(avoidance * Time.deltaTime * 0.05f);
        }
    }
}
