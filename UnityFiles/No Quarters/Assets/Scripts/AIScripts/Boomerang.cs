using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    [SerializeField]
    private int boomerangDamage = 5;
    [SerializeField]
    private float speed = 2.0f;
    private Transform target;
    private List<Transform> nodes;

    [SerializeField]
    private int currentNode = 0;

    [SerializeField]
    private List<Transform> paths;

    private Vector3 initialPos;

    // Start is called before the first frame update

    private void Awake()
    {
        initialPos = transform.position;
        nodes = new List<Transform>();
        Random.InitState((int)System.DateTime.Now.Ticks);
        int pathNumber = Random.Range(0, paths.Count);

        for (int counter = 0; counter < paths[pathNumber].childCount; counter++)
        {
            nodes.Add(paths[pathNumber].GetChild(counter));
        }

    }

    // Update is called once per frame
    void Update()
    {
        float step = speed * Time.deltaTime;

        target = nodes[currentNode];

        transform.position = Vector2.MoveTowards(transform.position, target.position, step);


        if (Vector3.Distance(transform.position, target.position) <= 0.05f)
        {
            currentNode++;
            if (currentNode == nodes.Count)
            {
                gameObject.SetActive(false);
            }
        }

        return;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponentInParent<PlayerControllerV2>().TakeDamage(boomerangDamage);
        }
    }

    public void activateBoomerrang()
    {
        gameObject.SetActive(true);
        currentNode = 0;
        transform.position = initialPos;

    }

    public void disableBoomerrang()
    {
        gameObject.SetActive(false);
    }
}