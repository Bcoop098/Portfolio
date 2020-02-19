using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CroissantShot : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private int damage = 5;

    [SerializeField]
    private float timeToDeath = 6f;

    private GameObject player;

    private GameObject boss;

    Vector3 playerVel;
    Vector3 Direction;
    Vector3 PlayerMoveVector;

    Vector2 croissantDirection;

    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        initialPos = transform.position;
        boss = GameObject.FindGameObjectWithTag("Boss");
        setTarget();
        playerVel = player.GetComponent<PlayerControllerV2>().playerVelocity;
        PlayerMoveVector = playerVel + player.transform.position;
        Direction = PlayerMoveVector - boss.transform.position;
        croissantDirection = new Vector2(Direction.x, Direction.y);


        StartCoroutine(Destroy(timeToDeath));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(croissantDirection.normalized * Time.deltaTime * speed);
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
        player = temp;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerControllerV2>().TakeDamage(damage);
            gameObject.SetActive(false);
            StopAllCoroutines();
        }
    }


    private IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
        StopAllCoroutines();
    }

    public void enableShot()
    {
        gameObject.SetActive(true);
        transform.position = boss.transform.position;
        setTarget();
        StartCoroutine(Destroy(timeToDeath));
        playerVel = player.GetComponent<PlayerControllerV2>().playerVelocity;
        PlayerMoveVector = playerVel + player.transform.position;
        Direction = PlayerMoveVector - boss.transform.position;
        croissantDirection = new Vector2(Direction.x, Direction.y);
    }

    public void disableShot()
    {
        gameObject.SetActive(false);
        StopAllCoroutines();
    }
}
