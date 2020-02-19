using UnityEngine;
using System.Collections.Generic;


public class Pathfinding : MonoBehaviour
{
    [SerializeField]
    private float speed = 2.0f;

    //used to reset speed
    private float normalSpeed;

    [SerializeField]
    private float speedModifier = 0.65f;

    [SerializeField]
    private float powerTime = 3.0f;

    private Transform target;
    [SerializeField]
    private List<Transform> paths;
    [SerializeField]
    private int currentNode = 0;

    private List<Transform> nodes;

    private bool canWalk = true;

    private bool canPowerUp = true;

    [SerializeField]
    private Transform healthCanvas;

    private Vector3 healthLocalPosition;

    private Quaternion healthRotation;

    private void Awake()
    {
        normalSpeed = speed;
        nodes = new List<Transform>();

        Random.InitState((int)System.DateTime.Now.Ticks);
        int pathNumber = Random.Range(0, paths.Count);

        healthLocalPosition = healthCanvas.localPosition;
        healthRotation = healthCanvas.rotation;

        for (int counter = 0; counter < paths[pathNumber].childCount; counter++)
        {
            nodes.Add(paths[pathNumber].GetChild(counter));
        }
    }

    void Update()
    {
        if (canWalk)
        {
            WalkPath();
        }
    }

    private void WalkPath()
    {
        float step = speed * Time.deltaTime;

        target = nodes[currentNode];

        transform.position = Vector2.MoveTowards(transform.position, target.position, step);

        if (Vector3.Distance(transform.position, target.position) <= 0.05f)
        {
            currentNode++;
            
            if (currentNode == nodes.Count)
            {
                GameManager.instance.DamageNexus(2);
                Destroy(gameObject);
            }
        }

        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Vector3 nexusPosition = GameObject.Find("Nexus").transform.position;
            nexusPosition.z = collision.transform.position.z;

            if (!collision.GetComponent<PlayerMouseMovement>().IsMoving() && collision.transform.position != nexusPosition)
            {
                canWalk = false;
            }
        }

        if (collision.tag == "Xygo")
        {
            if (canPowerUp == true)
            {
                ChangeSpeed();
            }
        }
        return;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<PlayerMouseMovement>().IsMoving())
            {
                canWalk = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            canWalk = true;
        }

        return;
    }

    public void ChangeMovement(bool moveState)
    {
        canWalk = moveState;
    }

    private void NormalSpeed()
    {
        speed = normalSpeed;
        canPowerUp = true;
    }
    private void ChangeSpeed()
    {
        canPowerUp = false;
        speed = speed * speedModifier;
        Invoke("NormalSpeed", powerTime);
    }
}