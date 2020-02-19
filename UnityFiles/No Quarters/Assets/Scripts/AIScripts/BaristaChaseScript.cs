using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaristaChaseScript : MonoBehaviour
{

    GameObject Target;
    Vector3 playerVel;
    Vector3 Direction;
    public float moveSpeed;
    public float detectionRadius;
    public float closureDistance;
    public float fleeThreshold; // a float between 0 and 1 that determines when the the peasant flees
    public bool wanderDirection;
    public bool flee;
    public float persueAnt = 1.0f;

    float distance;
    bool aggro;
    float maxHealth;

    [SerializeField]
    GameObject BaristaWeapon;

    [SerializeField]
    GameObject Barista;

    bool flip = false;

    Vector3 PlayerMoveVector;// new 


    // Make sure you set up the target and input speed, radius, and closuredistance
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        maxHealth = gameObject.GetComponent<BaristaScript>().returnHealth();
        setTarget();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Barista.GetComponent<BaristaScript>().checkEnrage())
        {
            if (BaristaWeapon.GetComponent<BaristaMeleeScript>().checkAttack())
            {
                BaristaAnim.baristaAnimInstance.SetBarCurrentAnimations(1);
                StartCoroutine("afterAttack");
            }
            //setmoveAnimation
            //playerMoveDirection = playerManager.GetComponent<PlayerManager>().moveInput;
            else
            {
                
                playerVel = Target.GetComponent<PlayerControllerV2>().playerVelocity;
                PlayerMoveVector = playerVel + Target.transform.position;
                if (Direction.x <= 0)
                {
                    Barista.GetComponent<SpriteRenderer>().flipX = false;
                    flip = false;
                }
                else if (Direction.x > 0)
                {
                    Barista.GetComponent<SpriteRenderer>().flipX = true;
                    flip = true;
                }
                if (!BaristaWeapon.GetComponent<BaristaMeleeScript>().checkAttack())
                {
                    if (flip)
                    {
                        BaristaWeapon.GetComponent<BaristaMeleeScript>().FlipBarAttack(false);
                    }
                    if (!flip)
                    {
                        BaristaWeapon.GetComponent<BaristaMeleeScript>().FlipBarAttack(true);
                    }
                }
            }
            setTarget();
            distance = (Target.transform.position - this.transform.position).magnitude;
            if (distance < detectionRadius)
                aggro = true;
            if (!aggro)
                wander();
            else
                persue();
        }
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
    private void wander()
    {
        if (wanderDirection)
            transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0.0f, 0.0f));
        else
            transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0.0f, 0.0f));
    }

    void persue()
    {
        Direction = PlayerMoveVector - this.transform.position;

        float health = gameObject.GetComponent<BaristaScript>().returnHealth();
        if (health < (maxHealth * fleeThreshold))
            flee = true;

        if (distance > closureDistance) // keeps the enemy from closing in too much
        {
            if (flee)
                transform.Translate(-Direction.normalized * Time.deltaTime);
            else
                transform.Translate(Direction.normalized * Time.deltaTime);
        }
    }

    IEnumerator afterAttack()
    {
        yield return new WaitForSeconds(0.5f);
        BaristaAnim.baristaAnimInstance.SetBarCurrentAnimations(0);
    }
}
