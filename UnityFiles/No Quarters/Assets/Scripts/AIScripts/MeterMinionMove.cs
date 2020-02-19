using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterMinionMove : MonoBehaviour
{
    // Start is called before the first frame update
    GameObject Target;
    Vector3 playerVel;
    public Vector3 Direction;
    public Vector3 avoidance;
    public Vector3 Velocity;
    float moveSpeed = 1.5f;
    float detectionRadius = 100f;
    float closureDistance = 2f;
    float hitRange = 0.5f;
    float closureThreshold = 0.05f;
    float fleeThreshold = 0.25f;// a float between 0 and 1 that determines when the the peasant flees
    public bool wanderDirection;
    bool flee;
    float persueAnt = 1.0f;

    public bool moveBack;
    float distance;
    bool aggro;
    float maxHealth;
    public bool attack;
    float attackDuration = 0.5f;
    float attackCooldown = 1.0f;
    int damage = 1;
    float lastX;

    Vector3 PlayerMoveVector;

    // Make sure you set up the target and input speed, radius, and closuredistance
    void Start()
    {
        wanderDirection = false;
        //Target = GameObject.FindGameObjectWithTag("Player");
        setTarget();
        maxHealth = gameObject.GetComponent<BaseMeterMinion>().health;
        attack = true;
    }

    void Awake()
    {
        wanderDirection = false;
        //Target = GameObject.FindGameObjectWithTag("Player");
        //maxHealth = gameObject.GetComponent<BaseBossGuard>().health;
        attack = true;
    }

    // Update is called once per frame
    void Update()
    {
        setTarget();
        distance = (Target.transform.position - this.transform.position).magnitude;
        if (distance > detectionRadius)
        {
            setTarget();
            aggro = false;
        }
        PlayerMoveVector = playerVel + Target.transform.position;
        Direction = PlayerMoveVector - this.transform.position;
        Direction.Normalize();
        Velocity = Direction + avoidance;
        Velocity.Normalize();
        if (distance > closureDistance + closureThreshold) moveBack = false;

        //playerMoveDirection = playerManager.GetComponent<PlayerManager>().moveInput;
        //PlayerMoveVector = playerVel + Target.transform.position;
        playerVel = Target.GetComponent<PlayerControllerV2>().playerVelocity;
        PlayerMoveVector = playerVel + Target.transform.position;
        if (distance < detectionRadius)
            aggro = true;
        if (!aggro || distance > detectionRadius) // could make abig changes   
            wander();
        else
            pursue();

        UpdatePosition();
        if (distance <= closureDistance + closureThreshold && attack == true)
        {
            StartCoroutine(Attack());
        }
        UpdateDirection();
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
        if (distance < closureDistance + hitRange)
            Target.GetComponent<PlayerControllerV2>().TakeDamage(damage); // dealing damage
        closureDistance += 0.5f;
        moveBack = true;
        StartCoroutine(AttackCoolDown());
    }

    IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(attackCooldown);
        //moveBack = false;
        //attack = true;
        StartCoroutine(timeBeforeNextAttack(0, 2));
    }

    private void wander()
    {
        if (wanderDirection)
            Velocity = new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f);
        // transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f));
        //else
            //Velocity = new Vector3(-moveSpeed * Time.deltaTime, 0.0f, 0.0f);
        //transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0.0f, 0.0f));
    }

    void setTarget()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        GameObject temp = null;
        float minDist = Mathf.Infinity;
        //float bestDistance = 10000f;
        Vector3 currentPos = transform.position;
        foreach (GameObject player in players)
        {
            Vector3 distanceToPlayer = player.transform.position - currentPos;
            float distance = distanceToPlayer.sqrMagnitude;
            if (distance < minDist)
            {
                temp = player;
                minDist = distance;
            }
        }
        Target = temp;
    }

    void pursue()
    {
        float health = gameObject.GetComponent<BaseMeterMinion>().health;
        //if (health < (maxHealth * fleeThreshold))
            //flee = true;

        if (distance > closureDistance + closureThreshold) // keeps the enemy from closing in too much
        {
            //if (flee || moveBack)
                //Velocity = -Velocity;
            //transform.Translate(-Velocity * Time.deltaTime);
            //else
            //transform.Translate(Velocity * Time.deltaTime);
        }
        if (distance < closureDistance - closureThreshold)
        {
            //transform.Translate(-Direction * Time.deltaTime);
            //transform.Translate(new Vector3(-Velocity.x, Velocity.y, 0.0f) * Time.deltaTime);
            Velocity = new Vector3(-Velocity.x, Velocity.y, 0.0f);
        }
        if (distance - closureDistance < 0.1 && distance - closureDistance > -0.1)
        {
            if (distance - closureDistance > 0)
                Velocity = new Vector3(0.1f, 0.0f, 0.0f);
            //else if (distance - closureDistance < 0)
                //Velocity = new Vector3(-0.1f, 0.0f, 0.0f);
            //attack = true;
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

    void UpdatePosition()
    {
        transform.Translate(Velocity * Time.deltaTime);
    }

    void UpdateDirection()
    {
        if (Direction.x < 0 && lastX > 0)
            this.GetComponent<SpriteRenderer>().flipX = true;
        if (Direction.x > 0 && lastX < 0)
            this.GetComponent<SpriteRenderer>().flipX = false;
        lastX = Direction.x;
    }
}
