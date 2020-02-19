using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCoin2 : MonoBehaviour
{
    // Start is called before the first frame update
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

    Vector2 coinDirection;

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss3");
        setTarget();
        playerVel = player.GetComponent<PlayerControllerV2>().playerVelocity;
        PlayerMoveVector = playerVel + player.transform.position;
        Direction = PlayerMoveVector - boss.transform.position;
        coinDirection = new Vector2(Direction.x, Direction.y);


        StartCoroutine(Destroy(timeToDeath));
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

    // Update is called once per frame
    void Update()
    {
        transform.Translate(coinDirection.normalized * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
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

    public void enableCoin2()
    {
        gameObject.SetActive(true);
        transform.position = boss.transform.position;
        setTarget();
        playerVel = player.GetComponent<PlayerControllerV2>().playerVelocity;
        PlayerMoveVector = playerVel + player.transform.position;
        Direction = PlayerMoveVector - boss.transform.position;
        coinDirection = new Vector2(Direction.x, Direction.y);
        StartCoroutine(Destroy(timeToDeath));
    }

    public void disableCoin2()
    {
        gameObject.SetActive(false);
        StopAllCoroutines();
    }
}
